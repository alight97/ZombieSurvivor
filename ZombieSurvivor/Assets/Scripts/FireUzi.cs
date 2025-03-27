using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUzi : MonoBehaviour
{
    public Transform firePoint;

    public float fireCoolTime = 0.1f;
    public float fireRange = 10f;
    private float lastFireTime = 0f;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (lastFireTime + fireCoolTime < Time.time)
            {
                lastFireTime = Time.time;
                StartCoroutine(Fire());
            }
        }
    }

    private IEnumerator Fire()
    {
        Debug.Log("Fire");

        var startPos = firePoint.position;
        var endPos = startPos + firePoint.forward * fireRange;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
        {
            endPos = hit.point;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPos);

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(fireCoolTime - 0.01f);

        lineRenderer.enabled = false;
    }
}
