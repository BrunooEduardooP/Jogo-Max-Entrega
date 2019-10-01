using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class DialogController{

    //Sprite imageSprite;
    string dialogTitle;
    string [] dialogs;
    int actualDialogIndex;

    Text dialogsElement;
    Text titleElement;
    Image hudElement;

    bool dialoguing = false;
    

    public DialogController(string textDialogElementName, string titleDialogElementName, string textHudElementName){
     dialogsElement = GameObject.Find(textDialogElementName).GetComponent<Text>();
     titleElement = GameObject.Find(titleDialogElementName).GetComponent<Text>();
     hudElement = GameObject.Find(textHudElementName).GetComponent<Image>();
        //imageElement = GameObject.Find(imageElementName).GetComponent<Image>();

    }

    public void startDialog(Conversable conversableEntity) {
        getEntityElements(conversableEntity);
        enableElements();
        titleElement.text = dialogTitle; 
        actualDialogIndex = 0;
        dialoguing = true;
    }

    void getEntityElements(Conversable conversableEntity)
    {
        //imageSprite = conversableEntity.GetObjectImageSprite();
        dialogTitle = conversableEntity.GetDialogTitle();
        //imageElement.sprite = imageSprite;
        dialogs = conversableEntity.GetDialogMessages();
    }

    void enableElements(){
        titleElement.enabled = true;
        dialogsElement.enabled = true;
        hudElement.enabled = true;
        //imageElement.enabled = true;
    }


    public void showNextMessage() {
        if (hasNextMessage()){
            dialogsElement.text = dialogs[actualDialogIndex];
            actualDialogIndex++;
        }
    }

    public void close(){
        titleElement.enabled = false;
        dialogsElement.enabled = false;
        hudElement.enabled = false;
        dialoguing = false;
    }

    public bool hasNextMessage(){
        return actualDialogIndex < dialogs.Length;
    }

    public bool isSomeDialogHappening(){
        return this.dialoguing;
    }


    private string[] conversableNpcsNames = { "sensei" , "Casa", "Placa Casa"};
    public void startMessageWithNpcByName(string npcName){
        Debug.Log(npcName);
        if(npcName == "sensei") {
            Sensei sensei = new Sensei();
            startDialog(sensei);
        }else if(npcName == "Casa"){
            Casa casa = new Casa();
            startDialog(casa);
        }else if(npcName == "Placa Casa"){
            PlacaCasa placaCasa = new PlacaCasa();
            startDialog(placaCasa);
        }
    }

    public bool isAnConverasbleNpc(string npcName)
    {
        int index = System.Array.IndexOf(conversableNpcsNames, npcName);

        return index > -1;
    }



}

