using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator animator;
    private bool isOpen;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isOpen )
        {
            animator.SetBool("doorOpen", true);
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") &&isOpen)
        {
            animator.SetBool("doorOpen", false);
            isOpen = false;
        }
    }
}
