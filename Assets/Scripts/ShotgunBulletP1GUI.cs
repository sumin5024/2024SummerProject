using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunBulletP1GUI : MonoBehaviour
{
    public Text P1shotgunBullet; // UI Text component to display bullet count
    public int P1shotgunBulletCount;
    private bool isReloading = false; // Flag to check if reloading is in progress

    void Start()
    {
        P1shotgunBulletCount = 10 - Player1Controller.instance.shotgunFireCount;
        UpdateShotgunBulletText(); 
    }

    void Update()
    {
        if (!isReloading && P1shotgunBulletCount + Player1Controller.instance.shotgunFireCount != 10)
        {
            P1shotgunBulletCount = 10 - Player1Controller.instance.shotgunFireCount;

            if (P1shotgunBulletCount <= 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                UpdateShotgunBulletText();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        for (int i = 5; i > 0; i--)
        {
            P1shotgunBullet.text = "Reloading: " + i + "s";
            yield return new WaitForSeconds(1f);
        }

        Player1Controller.instance.shotgunFireCount = 0; // Reset the fire count after reloading
        P1shotgunBulletCount = 10;
        UpdateShotgunBulletText();
        isReloading = false;
    }

    void UpdateShotgunBulletText()
    {
        P1shotgunBullet.text = "Shotgun Bullet: " + P1shotgunBulletCount;
    }
}
