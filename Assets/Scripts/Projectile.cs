using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public int damage;

    public GameObject destroyEffect;

    private Rigidbody2D body2D;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        body2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body2D.velocity = transform.right * speed;
    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            DestroyProjectile();
        }

        if(collision.gameObject.tag == "Ground")
        {
            DestroyProjectile();
        }
    }
}
