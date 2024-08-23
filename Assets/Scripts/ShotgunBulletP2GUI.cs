using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunBulletP2GUI : MonoBehaviour
{
    public Text P2shotgunBullet;
    public int P2shotgunBulletCount;

    // Start is called before the first frame update
    void Start()
    {
        P2shotgunBulletCount = 10 - Player2Controller.instance.shotgunFireCount;
        UpdateShotgunBulletText(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (P2shotgunBulletCount + Player2Controller.instance.shotgunFireCount != 10)
        {
            P2shotgunBulletCount = 10 - Player2Controller.instance.shotgunFireCount;
            UpdateShotgunBulletText(); 
        }
    }

    void UpdateShotgunBulletText()
    {
        P2shotgunBullet.text = "Shotgun Bullet: " + P2shotgunBulletCount;
    }
}
