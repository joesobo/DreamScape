using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public GameObject destroyEffect;
    private RoundHandler roundHandler;

    private CameraShake shake;

    private void Awake()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
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
            shake.CamShake();

            Instantiate(destroyEffect, transform.position, Quaternion.identity);

            //update roundHandler
            roundHandler.enemiesLeft--;
            roundHandler.enemiesRemaining();

            Destroy(gameObject);
        }
    }
}
