using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZombie : MonoBehaviour
{
    public float damage = 10f;
    public float attackDelay = 1.0f;

    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var healthSlider = other.transform.GetComponentInChildren<HealthSlider>();

            if (lastAttackTime + attackDelay < Time.time)
            {
                lastAttackTime = Time.time;
                healthSlider.OnDamage(damage);
            }
        }
    }
}
