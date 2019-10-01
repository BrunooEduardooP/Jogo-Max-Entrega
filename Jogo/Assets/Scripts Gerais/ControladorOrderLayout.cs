using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorOrderLayout : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer[] decoracoesSR;
    Transform[] decoracoesT;
    
    Transform personagem;
    GameObject decoracoes;
    void Start()
    {
        decoracoesSR = gameObject.GetComponentsInChildren<SpriteRenderer>();
        decoracoesT = gameObject.GetComponentsInChildren<Transform>();
            
        personagem = GameObject.Find("Hero").GetComponent<Transform>();
        

    }

    // Update is called once per frame
    void Update()
    {
        float posicaoPersonagem = personagem.position.y;
      
        for (int i = 0; i < decoracoesSR.Length ; i++)        {
            switch (decoracoesSR[i].tag) {

                case "Arvore":
                    if (posicaoPersonagem >= decoracoesT[i + 1].position.y - 1){ // O "-1" é usado porque as folhas das arvores tem 1 unidade de abaixo de seu centro.
                        decoracoesSR[i].sortingOrder = 3;

                    }else{
                        decoracoesSR[i].sortingOrder = 1;
                    }
                    break;
                
                default:
                    if (posicaoPersonagem >= decoracoesT[i + 1].position.y) {
                        decoracoesSR[i].sortingOrder = 3;

                    }else{
                        decoracoesSR[i].sortingOrder = 1;
                    }

                    break;

                    
            }


            

        }

    }
}
