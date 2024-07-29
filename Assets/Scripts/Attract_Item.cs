using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract_Item : MonoBehaviour
{
    public Transform player1Transform; // 플레이어 1의 Transform
    public Transform player2Transform; // 플레이어 2의 Transform

    private Vector3 targetPosition;
    public float attractDuration = 10f;
    public float cooldownDuration = 10f;
    private bool isAttractActive = false;
    private bool isCooldown = false;

    public GameObject attractItemImage; 

    void Start()
    {
        if (attractItemImage != null)
        {
            attractItemImage.SetActive(false); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldown)
        {
            Debug.Log("플레이어 1 키가 눌려졌습니다. 플레이어 위치 저장 중...");
            StorePlayerPosition(player1Transform);
            UseAttractItem();
        }
        if (Input.GetKeyDown(KeyCode.I) && !isCooldown)
        {
            Debug.Log("플레이어 2 키가 눌려졌습니다. 플레이어 위치 저장 중...");
            StorePlayerPosition(player2Transform);
            UseAttractItem();
        }
    }

    void StorePlayerPosition(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            targetPosition = playerTransform.position;
            targetPosition.z = 0; 
            Debug.Log("목표 위치 설정: " + targetPosition);
        }
        else
        {
            Debug.LogError("플레이어 Transform이 지정되지 않았습니다.");
        }
    }

    void UseAttractItem()
    {
        if (attractItemImage != null)
        {
            attractItemImage.transform.position = targetPosition; 
            attractItemImage.SetActive(true); 
        }
        StartCoroutine(ActivateAttract());
    }

    IEnumerator ActivateAttract()
    {
        Debug.Log("아이템 활성화됨");
        isAttractActive = true;
        isCooldown = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActive = false;
        if (attractItemImage != null)
        {
            attractItemImage.SetActive(false); 
        }
        Debug.Log("아이템 비활성화됨");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        Debug.Log("쿨다운 완료");
    }

    public bool IsAttractActive()
    {
        return isAttractActive;
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }
}
