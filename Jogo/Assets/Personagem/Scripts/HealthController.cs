using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    class HealthController : MonoBehaviour{

        private RectTransform healthBarSize;
        public float healthMultiplier = 3f;
        Image healthBar;
        Image healthHud;
        public HealthController(string healthBarGameObjectName, string healthHudGameObjectName) {
            healthBar = GameObject.Find(healthBarGameObjectName).GetComponent<Image>();
            healthHud = GameObject.Find(healthHudGameObjectName).GetComponent<Image>();
            healthBarSize = GameObject.Find(healthBarGameObjectName).GetComponent<RectTransform>();
        
        }

        public void disable(){
            healthBar.enabled = false;
            healthHud.enabled = false;

        }

        public void enable(){
            healthBar.enabled = true;
            healthHud.enabled = true;
            
        }

        public void UpdateHealth(float newHealth) {
            float imageHeight = healthBarSize.rect.height;
            float imageWidth = newHealth * healthMultiplier;
            healthBarSize.sizeDelta = new Vector2(imageWidth, imageHeight);
        }


    }

