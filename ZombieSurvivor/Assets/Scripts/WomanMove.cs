using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWoman : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 200.0f;

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        UpdateRotate(dir);
        UpdateMove(dir);
    }

    private void UpdateRotate(Vector3 dir)
    {
        float turn = dir.x * rotateSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, turn, 0);
    }

    private void UpdateMove(Vector3 dir)
    {
        var movement = transform.forward * dir.z * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        animator.SetFloat("Move", dir.z);
    }
}
