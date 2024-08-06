using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract_Item : MonoBehaviour
{
    public Transform playerTransform; 
    private Vector3 targetPosition;
    public float attractDuration = 10f;
    public float cooldownDuration = 20f;
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
        if (Input.GetKeyDown(KeyCode.P) && !isCooldown)
        {
            Debug.Log("P key pressed, storing player position");
            StorePlayerPosition();
            UseAttractItem();
        }
    }

    void StorePlayerPosition()
    {
        if (playerTransform != null)
        {
            targetPosition = playerTransform.position;
            targetPosition.z = 0; 
            Debug.Log("Target position set to: " + targetPosition);
        }
        else
        {
            Debug.LogError("Player Transform is not assigned.");
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
        Debug.Log("Attract item activated");
        isAttractActive = true;
        isCooldown = true;
        yield return new WaitForSeconds(attractDuration);
        isAttractActive = false;
        if (attractItemImage != null)
        {
            attractItemImage.SetActive(false); 
        }
        Debug.Log("Attract item deactivated");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        Debug.Log("Attract item cooldown finished");
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
