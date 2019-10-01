using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class Sensei : Conversable{

    string[] dialogs = { "Bem vindo aventureiro, sua jornada começa aqui.", 
        "Eu sou o Sensei, sou o mais sábio desse mundo...",
        "Você está no Puzzle World, o mundo de enigmas e desafios.",
        "Nele você deverá contar com sua sagacidade, inteligência e intuição para sobreviver.",
        "Também será necessários conhecimentos matemáticos, a matemática é a essência desse mundo..",
        "Tome cuidado pois sua primeira missão é encontrar um misterioso calabouço.", 
        "Onde ele está? Bom, disso não tenho certeza...",
        "Boatos dizem que ele está presente, falando da abcissa, 191 vezes negativas o mágico número dos círculos.",
        "E ordenalmente falando, está na segunda centena numérica.",
        "Não entendeu? Problema seu!",
        "Agora vá atrás do calabouço! O que você ainda está fazendo aí?",
        "Pensamento do Sensei: *Não se fazem mais heróis como antigamente...*"
        

    };
    string title = "Sensei - O Mestre";

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

