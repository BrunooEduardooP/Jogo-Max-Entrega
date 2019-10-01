using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class personagem : MonoBehaviour
{
    
    public float velocidadeAndando = 3f;
    public float velocidadeCorrendo = 6f;
    public float velocidade;
     Transform attackChecker;
     Transform interactChecker;
    public float interactRadius = 1.3f;
    public LayerMask enemyLayer;
    public LayerMask interactableLayer;
    public float attackRadius = 0.5f;
    public Transform heroTransform;
    Rigidbody2D corpo;
    Animator animator;
    Animation anim;

    public string lastDirection = "top";
    public string actualDirection = "top";
    public bool heroLookingRight = false;
    public bool heroLookingLeft = false;
    public bool heroLookingTop = true;
    public bool heroLookingDown = false;
    public bool heroIsRunning = false;
    public float attackFatigueDuration = 0.3f;
    public float maxStamina = 100;
    public float maxHealth = 100;

    public float actualStamina = 100;
    public float actualHealth = 100;

    public float normalAttackRadius = 0.5f;
    public float normalAttackDistanceReach = 0.6f;

    public float interactDistanceReach = 0.7f;

    private UIController uiController;

    public float actualFatigue = 0;
    void Start() {
        uiController = new UIController(this);
        corpo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackChecker = transform.Find("attackChecker").GetComponent<Transform>();
        interactChecker = transform.Find("interactChecker").GetComponent<Transform>();
    }
    // Update is cled once per frame
    void Update(){
        uiController.Update();
    }
   
    void FixedUpdate(){

        corpo.velocity = new Vector2(0, 0);
        if (uiController.isGamePaused()){
            stopHero();
        }else{
            heroMovementBehavior();
            heroInteractionBehavior();
            heroAttackBehavior();
            heroRestBehavior();
        }
        
        

    }

    void stopHero(){
        corpo.velocity = new Vector2(0, 0);
        animator.SetInteger("Vertical", 0);
        animator.SetInteger("Horizontal", 0);
    }

    void heroRestBehavior(){
        if (!heroIsRunning){
            rest();
        }
    }

    void heroMovementBehavior(){
        verifyMovementDirection();
        verifyHeroIsRunning();
        doMovement();
        
    }


    void verifyMovementDirection(){

        string direction = "";
        if (Input.GetAxisRaw("Vertical") == 1)
        { // Apertou 
            direction += "top";
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        { // Apertou 
            direction += "down";
        }


        if (Input.GetAxisRaw("Horizontal") == 1)
        { // Apertou 
            direction += "right";
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        { // Apertou A
            direction += "left";
        }

        actualDirection = direction;

    }

    void verifyHeroIsRunning()
    {

        if (isFatigued() || Input.GetAxisRaw("Fire3") == 0 || actualStamina <= 5)
        {
            heroIsRunning = false;
        }
        else
        {

            heroIsRunning = true;
        }
    }

    void doMovement(){

        velocidade = heroIsRunning ? velocidadeCorrendo : velocidadeAndando;

        if (isFatigued()){
            animator.SetInteger("Vertical", 0);
            animator.SetInteger("Horizontal", 0);
            return;
        }

        if (actualDirection.Contains("top")){
            corpo.velocity = new Vector2(corpo.velocity.x, velocidade);
            animator.SetInteger("Vertical", 1);
            heroLookingTop = true;
            heroLookingDown = false;
            lastDirection = "top";
        }
        else if (actualDirection.Contains("down")){
            corpo.velocity = new Vector2(corpo.velocity.x, -velocidade);
            animator.SetInteger("Vertical", -1);
            heroLookingTop = false;
            heroLookingDown = true;
            lastDirection = "down";
        }else{
            corpo.velocity = new Vector2(corpo.velocity.x, 0);
            animator.SetInteger("Vertical", 0);
        }

        if (actualDirection.Contains("right")){
            corpo.velocity = new Vector2(velocidade, corpo.velocity.y);
            animator.SetInteger("Horizontal", 1);
            heroLookingLeft = false;
            heroLookingRight = true;
            lastDirection = "right";
        }else if (actualDirection.Contains("left")){
            corpo.velocity = new Vector2(-velocidade, corpo.velocity.y);
            animator.SetInteger("Horizontal", -1);
            heroLookingRight = false;
            heroLookingLeft = true;
            lastDirection = "left";
        }else{
            corpo.velocity = new Vector2(0, corpo.velocity.y);
            animator.SetInteger("Horizontal", 0);
        }

        if(actualDirection != ""){
            if (heroIsRunning) actualStamina -= 0.5f;

        }

    }

    void heroInteractionBehavior(){
        updateInteractionCheckerPosition();
        interactIfTrigged();
    }

    void interactIfTrigged(){
        if (Input.GetButtonDown("Interact")){
            heroInteract();
        }
    }

    void heroInteract(){
        Collider2D [] objectsReached = Physics2D.OverlapBoxAll(interactChecker.position, new Vector2(interactRadius, interactRadius), interactableLayer);
        Debug.Log("Interagiu");
        for(int i = 0; i < objectsReached.Length; i++){
            if (objectsReached[i].name == "Hero") continue;
            Debug.Log("ACHOU");
            if (uiController.isAnConverasbleNpc(objectsReached[i].name)){
                uiController.startMessageWithNpcByName(objectsReached[i].name);
            }
        }
    }

    

    void updateInteractionCheckerPosition(){
        float newInteractionCheckerLocalXPosition = 0;
        float newInteractionCheckerLocalYPosition = 0;

        if (lastDirection == "right")
        {
            newInteractionCheckerLocalXPosition = interactDistanceReach;
            newInteractionCheckerLocalYPosition = 0;
        }
        else if (lastDirection == "left")
        {
            newInteractionCheckerLocalXPosition = -interactDistanceReach;
            newInteractionCheckerLocalYPosition = 0;
        }
        else if (lastDirection == "top")
        {
            newInteractionCheckerLocalXPosition = 0;
            newInteractionCheckerLocalYPosition = interactDistanceReach;
        }
        else if (lastDirection == "down")
        {
            newInteractionCheckerLocalXPosition = 0;
            newInteractionCheckerLocalYPosition = -interactDistanceReach;
        }

        interactChecker.localPosition = new Vector2(newInteractionCheckerLocalXPosition, newInteractionCheckerLocalYPosition);
    }

    void heroAttackBehavior()
    {
        updateAttackCheckerPosition();
        punchIfAttacked();
    }


    void updateAttackCheckerPosition(){
        float newAttackCheckerLocalXPosition = 0;
        float newAttackCheckerLocalYPosition = 0;

        if (lastDirection == "right") {
            newAttackCheckerLocalXPosition = normalAttackDistanceReach;
            newAttackCheckerLocalYPosition = 0;
        }else if (lastDirection == "left") {
            newAttackCheckerLocalXPosition = -normalAttackDistanceReach;
            newAttackCheckerLocalYPosition = 0;
        }else if (lastDirection == "top") {
            newAttackCheckerLocalXPosition = 0;
            newAttackCheckerLocalYPosition = normalAttackDistanceReach;
        }else if (lastDirection == "down") {
            newAttackCheckerLocalXPosition = 0;
            newAttackCheckerLocalYPosition = -normalAttackDistanceReach;
        }
        
        attackChecker.localPosition = new Vector2(newAttackCheckerLocalXPosition, newAttackCheckerLocalYPosition);
    }

    void punchIfAttacked() {
        if(actualFatigue <= 0 && actualStamina > 20) {
            if (Input.GetButtonDown("Fire1")) {
                animator.SetTrigger("Soco");
                playerAttack();
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(interactChecker.position, new Vector3(interactRadius, interactRadius)) ;
        Gizmos.color = Color.cyan;
    }

    void playerAttack(){
        corpo.velocity = new Vector2(0, 0);

        actualStamina -= 20;
        actualFatigue = attackFatigueDuration;
        Debug.Log("Socou");
        Collider2D [] enemiesReached = Physics2D.OverlapBoxAll(attackChecker.position, new Vector2(interactRadius, interactRadius), enemyLayer);
    }

    bool isFatigued() {
        return actualFatigue > 0;
    }

    void rest() {
        if (isFatigued() ) {
            actualFatigue -= Time.deltaTime;
        }
        if(actualStamina < maxStamina && Input.GetAxisRaw("Fire3") == 0) actualStamina += 0.5f;
    }


}
