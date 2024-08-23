using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract_Item : MonoBehaviour
{
    public Transform player1Transform; // �÷��̾� 1�� Transform
    public Transform player2Transform; // �÷��̾� 2�� Transform

    private Vector3 targetPosition;
    public float attractDuration = 5f;
    public float cooldownDuration = 8f;
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
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldown && gm1.instance.coin >=5)
        {
            Debug.Log("�÷��̾� 1 Ű�� ���������ϴ�. �÷��̾� ��ġ ���� ��...");
            StorePlayerPosition(player1Transform);
            UseAttractItem();
            gm1.instance.coin -= 10;
        }
        if (Input.GetKeyDown(KeyCode.K) && !isCooldown && gm1.instance.coin >= 5)
        {
            Debug.Log("�÷��̾� 2 Ű�� ���������ϴ�. �÷��̾� ��ġ ���� ��...");
            StorePlayerPosition(player2Transform);
            UseAttractItem();
            gm1.instance.coin -= 10;
        }
    }

    void StorePlayerPosition(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            targetPosition = playerTransform.position;
            targetPosition.z = 0;
            Debug.Log("��ǥ ��ġ ����: " + targetPosition);
        }
        else
        {
            Debug.LogError("�÷��̾� Transform�� �������� �ʾҽ��ϴ�.");
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
        Debug.Log("������ Ȱ��ȭ��");
        isAttractActive = true;
        isCooldown = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActive = false;
        if (attractItemImage != null)
        {
            attractItemImage.SetActive(false);
        }
        Debug.Log("������ ��Ȱ��ȭ��");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        Debug.Log("��ٿ� �Ϸ�");
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
