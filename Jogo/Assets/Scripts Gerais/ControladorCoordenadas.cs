using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordsController : MonoBehaviour
{
    // Start is called before the first frame update

    Text coordinatesText;
    Image coordinatesHud;
    public float coordinatesMultiplier = 5;
    public CoordsController(string coordsTextElementName, string coordsHudElementName) {
        coordinatesText = GameObject.Find(coordsTextElementName).GetComponent<Text>();
        coordinatesHud = GameObject.Find(coordsHudElementName).GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdateCoords(Transform objectTransform){
        float objectCoordsX = objectTransform.position.x;
        float objectCoordsY = objectTransform.position.y;
        string formattedXPosition = (coordinatesMultiplier * objectCoordsX).ToString("0");
        string formattedYPosition = (coordinatesMultiplier * objectCoordsY).ToString("0");

        coordinatesText.text = "[" + formattedXPosition + "] [" + formattedYPosition+"]";
     
    }

    public void disable(){
        coordinatesHud.enabled = false;
        coordinatesText.enabled = false;
    }

    public void enable(){
        coordinatesHud.enabled = true;
        coordinatesText.enabled = true;
    }
}
