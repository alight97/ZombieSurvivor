using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedWoman : MonoBehaviour
{
    public Transform tGunPivot;
    public Transform tGunRightHandMount;
    public Transform tGunLeftHandMount;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        tGunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        if (tGunRightHandMount != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, tGunRightHandMount.position);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, tGunRightHandMount.rotation);
        }

        if (tGunLeftHandMount != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, tGunLeftHandMount.position);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, tGunLeftHandMount.rotation);
        }
    }
}
