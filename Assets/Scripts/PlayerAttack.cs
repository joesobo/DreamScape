using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockBackForce;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform LRAttackPos;
    public Transform UAttackPos;
    public Transform DAttackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage;

    public GameObject RattackParticle;
    public GameObject LattackParticle;
    public GameObject UattackParticle;
    public GameObject DattackParticle;

    private Collider2D[] enemiesToDamage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //left arrow key and looking to left
            if (Input.GetKeyDown(KeyCode.LeftArrow) /*&& transform.localScale.x == -1*/)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(LRAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(LattackParticle, LRAttackPos.position, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                rb.velocity = Vector2.right * knockBackForce;
            }

            //right arrow key and looking to left
            if (Input.GetKeyDown(KeyCode.RightArrow) /*&& transform.localScale.x == 1*/)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(LRAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(RattackParticle, LRAttackPos.position, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                rb.velocity = Vector2.left * knockBackForce;
            }

            //up arrow key
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(UAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(UattackParticle, UAttackPos.position, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                rb.velocity = Vector2.down * knockBackForce;
            }

            //down arrow key
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(DAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(DattackParticle, DAttackPos.position, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                rb.velocity = Vector2.up * knockBackForce;
            }

            if (enemiesToDamage != null)
            {
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                enemiesToDamage = null;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LRAttackPos.position, attackRange);
        Gizmos.DrawWireSphere(UAttackPos.position, attackRange);
        Gizmos.DrawWireSphere(DAttackPos.position, attackRange);
    }
}
