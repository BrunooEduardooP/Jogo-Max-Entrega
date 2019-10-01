using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class Casa : Conversable{

    string[] dialogs = {"Essa é sua casa...", 
        "Você perdeu a chave de sua casa! Vá procurar ela por aí!"};
    string title = "Lar, doce lar";

    public Sprite GetObjectImageSprite(){
        return null;
    }
    public string[] GetDialogMessages(){
        return dialogs;
    }

    public string GetDialogTitle(){
        return title;
    }
}

