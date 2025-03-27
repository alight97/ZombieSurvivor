using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMoveZombie : MonoBehaviour
{
    public float findDelay = 1.0f;
    public float seekRange = 5.0f;
    public LayerMask targetLayerMask;

    private float lastFindTime;
    private GameObject naviTarget;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

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
