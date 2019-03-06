using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public GameObject hitEffect;
    public GameObject destroyEffect;
    private RoundHandler roundHandler;

    private CameraShake shake;

    private EnemyAI enemyAI;

    private void Awake()
    {
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
