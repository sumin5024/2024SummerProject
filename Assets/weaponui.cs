using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public Image weaponImage; // UI Image 컴포넌트 참조
    public Sprite rifleSprite; // 라이플 이미지 스프라이트
    public Sprite shotgunSprite; // 샷건 이미지 스프라이트

    private bool isRifleActive = true; // 현재 라이플이 표시되고 있는지 여부

    void Update()
    {
        // 2번 키를 누르면 이미지 전환
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isRifleActive)
            {
                weaponImage.sprite = shotgunSprite;
            }
            else
            {
                weaponImage.sprite = rifleSprite;
            }

            // 상태를 반전시킴
            isRifleActive = !isRifleActive;
        }
    }
}
