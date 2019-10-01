using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    class StaminaController : MonoBehaviour{

        private RectTransform staminaBarSize;
        public float healthMultiplier = 1.5f;
        Image staminaBar;
        Image staminaHud;
        public StaminaController(string staminaBarGameObjectName, string staminaHudGameObjectName) {
            staminaBar = GameObject.Find(staminaBarGameObjectName).GetComponent<Image>();
            staminaHud = GameObject.Find(staminaHudGameObjectName).GetComponent<Image>();
            staminaBarSize = GameObject.Find(staminaBarGameObjectName).GetComponent<RectTransform>();
        
        }

        public void disable(){
            staminaBar.enabled = false;
            staminaHud.enabled = false;

        }

        public void enable(){
            staminaBar.enabled = true;
            staminaHud.enabled = true;
            
        }

        public void UpdateStamina(float newHealth) {
            float imageHeight = staminaBarSize.rect.height;
            float imageWidth = newHealth * healthMultiplier;
            staminaBarSize.sizeDelta = new Vector2(imageWidth, imageHeight);
        }


    }

