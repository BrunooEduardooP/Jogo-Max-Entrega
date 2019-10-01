using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class UIController {

    CoordsController coordsController;
    StaminaController staminaController;
    DialogController dialogController;

    bool gamePaused = false;
    HealthController healthController;
    personagem personagem;

    public UIController(personagem personagem) {
        staminaController = new StaminaController("StaminaBar", "StaminaHUD");
        coordsController = new CoordsController("CoordinatesText", "CoordinatesHUD");
        healthController = new HealthController("HealthBar", "HealthHUD");
        dialogController = new DialogController("TextDialog", "TitleDialog", "DialogHUD");
        
        this.personagem = personagem;
    }

    public bool isGamePaused(){
        return this.gamePaused;
    }
    public void Update() {

        staminaController.UpdateStamina(personagem.actualStamina);
        coordsController.UpdateCoords(personagem.transform);
        if (dialogController.isSomeDialogHappening()){
            dialogBehavior();
            disableMainUiElements();
            gamePaused = true;
        }else{
            gamePaused = false;
            enableMainUiElements();

        }
    }

    void disableMainUiElements(){
        staminaController.disable();
        healthController.disable();
        coordsController.disable();
    }

    void enableMainUiElements(){
        Debug.Log("Habilitando");
        staminaController.enable();
        healthController.enable();
        coordsController.enable();
    }

    void dialogBehavior(){

        if (Input.GetButtonDown("Interact")){
            if (dialogController.hasNextMessage()){
                Debug.Log("Nova mensagem");
                dialogController.showNextMessage();
            }
            else{
                dialogController.close();
                enableMainUiElements();
                Debug.Log("FECHANDO A CONVERSA");
            }
        }
    }

    public void startMessageWithNpcByName(string name){
        dialogController.startMessageWithNpcByName(name);

    }

    public bool isAnConverasbleNpc(string npcName){
        return dialogController.isAnConverasbleNpc(npcName);
    }

    public bool isHeroDialoguing() { return dialogController.isSomeDialogHappening(); }
}

