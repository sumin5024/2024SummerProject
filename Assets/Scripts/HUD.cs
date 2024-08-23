using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
   public enum InfoType { Exp, Level, Kill, Time, Health }
   public InfoType type;

   Text myText;
   Slider mySlider;

   void Awake()
   {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
   }

   void LateUpdate()
   {
        switch (type) {
            case InfoType.Exp:
                break;
            case InfoType.Health:
                float curHealth = gm1.instance.health1;
                float maxHealth = gm1.instance.maxHealth1;
                mySlider.value = curHealth / maxHealth;
                break;
            case InfoType.Kill:
                break;
            case InfoType.Level:
                myText.text = string.Format("Wave {0:F0}",gm1.instance.level);    
                break;
            case InfoType.Time:
                break;
        }
   }
}
