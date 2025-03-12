using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{

    public GameObject zombie; // El objeto zombie (que tiene el Animator)
    public HumanBodyBones handBone = HumanBodyBones.RightHand; // Hueso de la mano
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private Animator animator;
    private Transform handTransform;

    void Start()
    {
        // Obtiene el Animator desde el zombie general
        if (zombie != null)
        {
            animator = zombie.GetComponent<Animator>();
            if (animator != null)
            {
                handTransform = animator.GetBoneTransform(handBone);
            }
        }
    }

    void LateUpdate()
    {
        if (handTransform != null)
        {
            transform.position = handTransform.position + handTransform.TransformDirection(positionOffset);
            transform.rotation = handTransform.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
