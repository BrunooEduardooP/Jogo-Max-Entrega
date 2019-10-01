using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamera : MonoBehaviour
{

    Transform cameraT;
    Transform personagem;
    Vector3 diferenca;

    // Start is called before the first frame update
    void Start()
    {
        cameraT = GetComponent<Transform>();
        personagem = GameObject.Find("Hero").GetComponent<Transform>();
        diferenca = cameraT.position - personagem.position; // Cria a diferença inicial da Câmera e o jogador.
        DontDestroyOnLoad(GameObject.Find("Hero"));
        DontDestroyOnLoad(GameObject.Find("Canvas"));
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraT.position = personagem.transform.position + diferenca; // Atualiza a posição da câmera.
        
        string actualSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if(actualSceneName == "SampleScene")
        {
            if (personagem.position.y > 40)
            {
                personagem.position = new Vector2(0, 0);
                cameraT.position = personagem.transform.position + diferenca;
                UnityEngine.SceneManagement.SceneManager.LoadScene("SalaDePortais");
                

            }

        }
        else if (actualSceneName == "SalaDePortais")
        {
            Debug.Log("TA NA SALA DE PORTAIS");
            if (personagem.position.y > 0.35)
            {
                Debug.Log("AEEE");
                personagem.position = new Vector2(0, 0);
                cameraT.position = personagem.transform.position + diferenca;
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
