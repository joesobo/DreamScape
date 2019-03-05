using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagerOnCollision : MonoBehaviour {

    public GameObject playerBlood;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthController>().health--;
            Instantiate(playerBlood, collision.transform.position, Quaternion.identity);
        }
    }
}
