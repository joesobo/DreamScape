using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    public int health;
    public int maxHealth;

    public GameObject healthCanvas;
    public GameObject playerHit;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health == maxHealth - 1)
        {
            healthCanvas.transform.GetChild(5).GetComponent<Image>().enabled = false;
        }
        else if(health == maxHealth - 2)
        {
            healthCanvas.transform.GetChild(4).GetComponent<Image>().enabled = false;
        }
        else if(health <= 0)
        {
            healthCanvas.transform.GetChild(3).GetComponent<Image>().enabled = false;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(playerHit, transform.position, Quaternion.identity);
    }
}
