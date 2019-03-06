using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public GameObject hitEffect;
    public GameObject destroyEffect;
    private RoundHandler roundHandler;

    private Rigidbody2D rb;

    private CameraShake shake;

    private EnemyAI enemyAI;

    public int knockBackForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        roundHandler = FindObjectOfType<RoundHandler>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        shake.CamShake();
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        enemyAI.dazedTime = enemyAI.startDazedTime;
        rb.AddForce(-enemyAI.dir.normalized * knockBackForce);
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
