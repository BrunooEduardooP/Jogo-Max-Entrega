using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class PlacaCasa : Conversable{

    string[] dialogs = { "Casa do herói!\nNela está escrita: \"Bem Vindo a Casa do Herói!\""};
    string title = "Placa de sua casa";

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

