using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract_Item : MonoBehaviour
{
    public float attractDuration = 10f;
    public float cooldownDuration = 20f;
    private bool isAttractActive = false;
    private bool isCooldown = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isCooldown)
        {
            UseAttractItem();
        }

        if (isAttractActive)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
        }
    }

    void UseAttractItem()
    {
        StartCoroutine(ActivateAttract());
    }

    IEnumerator ActivateAttract()
    {
        isAttractActive = true;
        isCooldown = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActive = false;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
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
