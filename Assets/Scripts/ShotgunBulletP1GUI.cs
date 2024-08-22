using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotgunBulletP1GUI : MonoBehaviour
{
    public TextMeshProUGUI P1shotgunBullet;
    public int P1shotgunBulletCount;

    // Start is called before the first frame update
    void Start()
    {
        P1shotgunBulletCount = 10 - Player1Controller.instance.shotgunFireCount;
        UpdateShotgunBulletText(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (P1shotgunBulletCount + Player1Controller.instance.shotgunFireCount != 10)
        {
            P1shotgunBulletCount = 10 - Player1Controller.instance.shotgunFireCount;
            UpdateShotgunBulletText(); 
        }
    }

    void UpdateShotgunBulletText()
    {
        P1shotgunBullet.text = "Shotgun Bullet: " + P1shotgunBulletCount;
    }
}
