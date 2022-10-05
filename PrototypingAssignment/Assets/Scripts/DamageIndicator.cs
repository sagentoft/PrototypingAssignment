using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float expiryTimer = 1f;
    private bool isActive;
    // Start is called before the first frame update
    public void Initialize()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //expiryTimer =- Time.deltaTime;
        //if (expiryTimer < 0)
        //    Destroy(this.gameObject);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            //1. Enemy should have script that has a function for taking damage
            //2. If enemy got hit by projectile call that function with 10 damage as input
            TakeDamage(10);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //1. Enemy should have script that has a function for taking damage
            //2. If enemy got hit by projectile call that function with 10 damage as input
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
    }
}
