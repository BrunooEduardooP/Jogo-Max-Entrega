using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class personagem : MonoBehaviour
{
    
    public float velocidadeAndando = 3f;
    public float velocidadeCorrendo = 6f;
    public float velocidade;
    public Transform attackChecker;
    public LayerMask enemyLayer;
    public float attackRadius;
    public Transform heroTransform;
    Rigidbody2D corpo;
    Animator animator;
    Animation anim;

    public string lastDirection = "top";
    public bool heroLookingRight = false;
    public bool heroLookingLeft = false;
    public bool heroLookingTop = true;
    public bool heroLookingDown = false;
    public bool correndo = false;
    public float attackFatigueDuration = 0.3f;
    public float maxStamina = 100;
    public float maxHealth = 100;

    public float actualStamina = 100;
    public float actualHealth = 100;

    private UIController uiController;

    public float actualFatigue = 0;
    void Start() {
        uiController = new UIController(this);
        corpo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackChecker = transform.Find("attackChecker").GetComponent<Transform>();

    }
    // Update is called once per frame
    void Update(){
        uiController.Update();
    }
    

    void FixedUpdate(){

        corpo.velocity = new Vector2(0, 0);
        
        verifyIfHeroIsRunning();
        verifyMovimentDirection();
        updateAttackCheckerPosition();
        punchIfAttacked();

    }

    void verifyIfHeroIsRunning(){
        if (!isFatigued() && Input.GetAxisRaw("Fire3") == 1 && actualStamina >= 5) { // Apertou Shift
            velocidade = velocidadeCorrendo;
            actualStamina -= 0.5f;
        }else{
            velocidade = velocidadeAndando;
            if(actualStamina <= maxStamina) actualStamina += 0.5f;
        }
    }

    void verifyMovimentDirection(){
        if (isFatigued()) {
            corpo.velocity = new Vector2(0, 0);
            animator.SetInteger("Vertical", 0);
            animator.SetInteger("Horizontal", 0);
            rest();
            return;
        }
        if (Input.GetAxisRaw("Vertical") == 1) { // Apertou Cima
            Debug.Log("CIMA");  
            corpo.velocity = new Vector2(corpo.velocity.x, velocidade);
            animator.SetInteger("Vertical", 1);
            heroLookingTop = true;
            heroLookingDown = false;
            lastDirection = "top";
        } else if (Input.GetAxisRaw("Vertical") == -1) { // Apertou Baixo

            corpo.velocity = new Vector2(corpo.velocity.x, -velocidade);
            animator.SetInteger("Vertical", -1);
            heroLookingTop = false;
            heroLookingDown = true;
            lastDirection = "down";
        } else { // Não apertou nada
            corpo.velocity = new Vector2(corpo.velocity.x, 0);
            animator.SetInteger("Vertical", 0);
        }



        if (Input.GetAxisRaw("Horizontal") == 1){ // Apertou D
            corpo.velocity = new Vector2(velocidade, corpo.velocity.y);
            animator.SetInteger("Horizontal", 1);
            heroLookingLeft = false;
            heroLookingRight = true;
            lastDirection = "right";
        }else if (Input.GetAxisRaw("Horizontal") == -1) { // Apertou A
            corpo.velocity = new Vector2(-velocidade, corpo.velocity.y);
            animator.SetInteger("Horizontal", -1);
            heroLookingRight = false;
            heroLookingLeft = true;
            lastDirection = "left";
        }else {  // Nem a nem D 
            corpo.velocity = new Vector2(0, corpo.velocity.y);
            animator.SetInteger("Horizontal", 0);
        }



        
        


        
    }

    void updateAttackCheckerPosition(){
        int newAttackCheckerLocalXPosition = 0;
        int newAttackCheckerLocalYPosition = 0;

        if (lastDirection == "right") {
            newAttackCheckerLocalXPosition = 1;
            newAttackCheckerLocalYPosition = 0;
        }else if (lastDirection == "left") {
            newAttackCheckerLocalXPosition = -1;
            newAttackCheckerLocalYPosition = 0;
        }else if (lastDirection == "top") {
            newAttackCheckerLocalXPosition = 0;
            newAttackCheckerLocalYPosition = 1;
        }else if (lastDirection == "down") {
            newAttackCheckerLocalXPosition = 0;
            newAttackCheckerLocalYPosition = -1;
        }
        
        attackChecker.localPosition = new Vector2(newAttackCheckerLocalXPosition, newAttackCheckerLocalYPosition);
    }

    void punchIfAttacked() {
        if(actualFatigue <= 0 && actualStamina > 20) {
            if (Input.GetButtonDown("Fire1")) {
                animator.SetTrigger("Soco");
                corpo.velocity = new Vector2(0, 0);

                actualStamina -= 20;
                actualFatigue = attackFatigueDuration;
            }
        }


    }

    bool isFatigued() {
        return actualFatigue >= 0;
    }

    void rest() {
        if (isFatigued()) {
            actualFatigue -= Time.deltaTime;
        }
    }


}
