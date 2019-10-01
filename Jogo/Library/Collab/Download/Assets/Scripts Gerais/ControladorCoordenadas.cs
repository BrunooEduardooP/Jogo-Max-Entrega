using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCoordenadas : MonoBehaviour
{
    // Start is called before the first frame update

    Text coordinatesText;
    Transform heroTransform;
    public float coordinatesMultiplier = 5;
    void Start(){
        coordinatesText = GameObject.Find("CoordinatesText").GetComponent<Text>();
        heroTransform = GameObject.Find("Hero").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        float heroCoordinatesX = heroTransform.position.x;
        float heroCoordinatesY = heroTransform.position.y;
        string formattedXPosition = (coordinatesMultiplier * heroCoordinatesX).ToString("0");
        string formattedYPosition = (coordinatesMultiplier * heroCoordinatesY).ToString("0");

        coordinatesText.text = "[" + formattedXPosition + "] [" + formattedYPosition+"]";
    }
}
