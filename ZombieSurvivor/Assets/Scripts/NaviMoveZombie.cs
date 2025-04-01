using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NaviMoveZombie : MonoBehaviour
{
    public float findDelay = 1.0f;
    public float seekRange = 50.0f;
    public LayerMask targetLayerMask;

    private float lastFindTime;
    private GameObject naviTarget;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public List<NavMeshLink> navMeshLinkList;
    public float jumpTime = 1.0f;
    public float jumpHeight = 2.0f;
    private float jumpStartTime;
    private bool isJump;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFind();
        UpdateMove();
    }

    private void UpdateFind()
    {
        if (lastFindTime + findDelay < Time.time)
        {
            FindPath();
        }
    }

    private void UpdateMove()
    {
        if (isJump)
        {
            return;
        }

        if (navMeshAgent.isOnOffMeshLink)
        {
            StartCoroutine(JumpRoutine());
            return;
        }

        if (naviTarget != null)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(naviTarget.transform.position);

            animator.SetFloat("Move", navMeshAgent.speed);
        }
        else
        {
            navMeshAgent.isStopped = true;

            animator.SetFloat("Move", 0);
        }
    }

    private IEnumerator JumpRoutine()
    {
        var startPos = navMeshAgent.currentOffMeshLinkData.startPos;
        var endPos = navMeshAgent.currentOffMeshLinkData.endPos;

        while (true)
        {
            if (navMeshAgent.isStopped == false)
            {
                isJump = true;
                animator.SetBool("Jump", isJump);
                animator.SetFloat("Move", 0);
                navMeshAgent.isStopped = true;
                jumpStartTime = Time.time;
            }
            else
            {
                if (jumpStartTime + jumpTime < Time.time)
                {
                    navMeshAgent.CompleteOffMeshLink();
                    isJump = false;
                    animator.SetBool("Jump", isJump);
                    yield break;
                }

                var timeDelta = (Time.time - jumpStartTime) / jumpTime;
                var newPostion = Vector3.Lerp(startPos, endPos, timeDelta);
                newPostion.y += Mathf.Sin(timeDelta * Mathf.PI) * jumpHeight;
                transform.position = newPostion;
            }
            yield return null;
        }
    }

    private void FindPath()
    {
        naviTarget = null;

        var colliders = Physics.OverlapSphere(transform.position, seekRange, targetLayerMask);
        foreach(var collider in colliders)
        {
            naviTarget = collider.gameObject;
            break;
        }
    }
}
