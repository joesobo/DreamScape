using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public GameObject destroyEffect;
    private RoundHandler roundHandler;

    private void Awake()
    {
        roundHandler = FindObjectOfType<RoundHandler>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);

            //update roundHandler
            roundHandler.enemiesLeft--;
            roundHandler.enemiesRemaining();

            Destroy(gameObject);
        }
    }
}
