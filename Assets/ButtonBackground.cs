using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackground : MonoBehaviour
{
    public bool isEnter;
    public Animator animator;
    public float rotateSpeed;
    public float scaleSpeed;
    public GameObject content;

    private void OnMouseEnter()
    {
        isEnter = true;
        //animator.Play("ButtonBackground_Rotate");
    }

    private void OnMouseExit()
    {
        isEnter = false;
        //animator.Play("ButtonBackground_RotateBack");

    }

    private void Update()
    {
        Quaternion targetRotation;
        Vector3 targetScale;
        if (isEnter)
        {
            targetRotation = Quaternion.Euler(0, 0, 45);
            targetScale = new Vector3(2, 2, 1);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0,0);
            targetScale = new Vector3(1, 1, 1);
        }

        content.transform.localScale = Vector3.Lerp(content.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.RotateTowards(transform.rotation,targetRotation,rotateSpeed * Time.deltaTime);

    }
}
