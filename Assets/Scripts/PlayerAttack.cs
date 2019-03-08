using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockBackForce;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform RAttackPos;
    public Transform LAttackPos;
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

    private Vector3 RAttPos;
    private Vector3 LAttos;
    private Vector3 UAttPos;
    private Vector3 DAttPos;

    public int offset;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RAttPos = new Vector3(RAttackPos.transform.position.x, RAttackPos.transform.position.y, offset);
        LAttos = new Vector3(LAttackPos.transform.position.x, LAttackPos.transform.position.y, offset);
        UAttPos = new Vector3(UAttackPos.transform.position.x, UAttackPos.transform.position.y, offset);
        DAttPos = new Vector3(DAttackPos.transform.position.x, DAttackPos.transform.position.y, offset);

    }

    private void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //left arrow key
            if (Input.GetKeyDown(KeyCode.LeftArrow) /*&& transform.localScale.x == -1*/)
            {
                if(transform.localScale.x == 1)
                {
                    LAttos = new Vector3(LAttackPos.transform.position.x, LAttackPos.transform.position.y, offset);
                    enemiesToDamage = Physics2D.OverlapCircleAll(LAttackPos.position, attackRange, whatIsEnemy);
                    Instantiate(LattackParticle, LAttos, Quaternion.identity);
                }
                else
                {
                    RAttPos = new Vector3(RAttackPos.transform.position.x, RAttackPos.transform.position.y, offset);
                    enemiesToDamage = Physics2D.OverlapCircleAll(RAttackPos.position, attackRange, whatIsEnemy);
                    Instantiate(RattackParticle, RAttPos, Quaternion.identity);
                }
                timeBtwAttack = startTimeBtwAttack;
                rb.AddForce(new Vector2(knockBackForce, 0));
            }

            //right arrow key 
            if (Input.GetKeyDown(KeyCode.RightArrow) /*&& transform.localScale.x == 1*/)
            {
                if (transform.localScale.x == 1)
                {
                    RAttPos = new Vector3(RAttackPos.transform.position.x, RAttackPos.transform.position.y, offset);
                    enemiesToDamage = Physics2D.OverlapCircleAll(RAttackPos.position, attackRange, whatIsEnemy);
                    Instantiate(RattackParticle, RAttPos, Quaternion.identity);
                }
                else
                {
                    LAttos = new Vector3(LAttackPos.transform.position.x, LAttackPos.transform.position.y, offset);
                    enemiesToDamage = Physics2D.OverlapCircleAll(LAttackPos.position, attackRange, whatIsEnemy);
                    Instantiate(LattackParticle, LAttos, Quaternion.identity);
                }
                timeBtwAttack = startTimeBtwAttack;
                rb.AddForce(new Vector2(-knockBackForce, 0));
            }

            //up arrow key
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                UAttPos = new Vector3(UAttackPos.transform.position.x, UAttackPos.transform.position.y, offset);
                enemiesToDamage = Physics2D.OverlapCircleAll(UAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(UattackParticle, UAttPos, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                //rb.AddForce(new Vector2(0, -knockBackForce));
            }

            //down arrow key
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                DAttPos = new Vector3(DAttackPos.transform.position.x, DAttackPos.transform.position.y, offset);
                enemiesToDamage = Physics2D.OverlapCircleAll(DAttackPos.position, attackRange, whatIsEnemy);
                Instantiate(DattackParticle, DAttPos, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
                //rb.AddForce(new Vector2(0, knockBackForce));
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
        Gizmos.DrawWireSphere(LAttackPos.position, attackRange);
        Gizmos.DrawWireSphere(RAttackPos.position, attackRange);
        Gizmos.DrawWireSphere(UAttackPos.position, attackRange);
        Gizmos.DrawWireSphere(DAttackPos.position, attackRange);
    }
}
