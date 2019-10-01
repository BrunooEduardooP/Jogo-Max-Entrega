using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordsController : MonoBehaviour
{
    // Start is called before the first frame update

    Text coordinatesText;
    public float coordinatesMultiplier = 5;
    public CoordsController(string coordsTextGameObjectName) {
        coordinatesText = GameObject.Find(coordsTextGameObjectName).GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateCoords(Transform objectTransform){
        float objectCoordsX = objectTransform.position.x;
        float objectCoordsY = objectTransform.position.y;
        string formattedXPosition = (coordinatesMultiplier * objectCoordsX).ToString("0");
        string formattedYPosition = (coordinatesMultiplier * objectCoordsY).ToString("0");

        coordinatesText.text = "[" + formattedXPosition + "] [" + formattedYPosition+"]";
     
    }
}
