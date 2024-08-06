using System.Collections;
using UnityEngine;

public class Attract_Item : MonoBehaviour
{
    public Transform player1Transform; // 플레이어 1의 Transform
    public Transform player2Transform; // 플레이어 2의 Transform

    private Vector3 targetPositionPlayer1;
    private Vector3 targetPositionPlayer2;

    public float attractDuration = 10f;
    public float cooldownDuration = 20f;

    private bool isAttractActivePlayer1 = false;
    private bool isCooldownPlayer1 = false;

    private bool isAttractActivePlayer2 = false;
    private bool isCooldownPlayer2 = false;

    public GameObject attractItemImagePlayer1; 
    public GameObject attractItemImagePlayer2;

    void Start()
    {
        if (attractItemImagePlayer1 != null)
        {
            attractItemImagePlayer1.SetActive(false); 
        }

        if (attractItemImagePlayer2 != null)
        {
            attractItemImagePlayer2.SetActive(false); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldownPlayer1)
        {
            Debug.Log("플레이어 1 키가 눌려졌습니다. 플레이어 위치 저장 중...");
            StorePlayer1Position(player1Transform);
            UseAttractItemPlayer1();
        }
        if (Input.GetKeyDown(KeyCode.O) && !isCooldownPlayer2)
        {
            Debug.Log("플레이어 2 키가 눌려졌습니다. 플레이어 위치 저장 중...");
            StorePlayer2Position(player2Transform);
            UseAttractItemPlayer2();
        }
    }

    void StorePlayer1Position(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            targetPositionPlayer1 = playerTransform.position;
            targetPositionPlayer1.z = 0; 
            Debug.Log("플레이어 1 목표 위치 설정: " + targetPositionPlayer1);
        }
        else
        {
            Debug.LogError("플레이어 1 Transform이 지정되지 않았습니다.");
        }
    }

    void StorePlayer2Position(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            targetPositionPlayer2 = playerTransform.position;
            targetPositionPlayer2.z = 0; 
            Debug.Log("플레이어 2 목표 위치 설정: " + targetPositionPlayer2);
        }
        else
        {
            Debug.LogError("플레이어 2 Transform이 지정되지 않았습니다.");
        }
    }

    void UseAttractItemPlayer1()
    {
        if (attractItemImagePlayer1 != null)
        {
            attractItemImagePlayer1.transform.position = targetPositionPlayer1; 
            attractItemImagePlayer1.SetActive(true); 
        }
        StartCoroutine(ActivateAttractPlayer1());
    }

    void UseAttractItemPlayer2()
    {
        if (attractItemImagePlayer2 != null)
        {
            attractItemImagePlayer2.transform.position = targetPositionPlayer2; 
            attractItemImagePlayer2.SetActive(true); 
        }
        StartCoroutine(ActivateAttractPlayer2());
    }

    IEnumerator ActivateAttractPlayer1()
    {
        Debug.Log("플레이어 1 아이템 활성화됨");
        isAttractActivePlayer1 = true;
        isCooldownPlayer1 = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActivePlayer1 = false;
        if (attractItemImagePlayer1 != null)
        {
            attractItemImagePlayer1.SetActive(false); 
        }
        Debug.Log("플레이어 1 아이템 비활성화됨");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownPlayer1 = false;
        Debug.Log("플레이어 1 쿨다운 완료");
    }

    IEnumerator ActivateAttractPlayer2()
    {
        Debug.Log("플레이어 2 아이템 활성화됨");
        isAttractActivePlayer2 = true;
        isCooldownPlayer2 = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActivePlayer2 = false;
        if (attractItemImagePlayer2 != null)
        {
            attractItemImagePlayer2.SetActive(false); 
        }
        Debug.Log("플레이어 2 아이템 비활성화됨");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownPlayer2 = false;
        Debug.Log("플레이어 2 쿨다운 완료");
    }

    public bool IsAttractActive(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return isAttractActivePlayer1;
        }
        else if (playerNumber == 2)
        {
            return isAttractActivePlayer2;
        }
        return false;
    }

    public Vector3 GetTargetPosition(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return targetPositionPlayer1;
        }
        else if (playerNumber == 2)
        {
            return targetPositionPlayer2;
        }
        return Vector3.zero;
    }
}
