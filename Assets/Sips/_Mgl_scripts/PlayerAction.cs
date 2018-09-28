using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    [SerializeField] Collider2D attackCollider;
    [SerializeField] KeyCode actionKey;

    [SerializeField] float attackTimer, attackCooldown = .3f;
    bool attacking;

    void Awake()
    {
        attackCollider.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(actionKey) && !attacking)
        {
            Attack();
        }

        if(attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackCollider.enabled = false;
            }
        }
    }

    void Attack()
    {
        attacking = true;
        attackTimer = attackCooldown;
        attackCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != null)
        {
            if(other.gameObject.tag == "Enemy")
            {
                // do dmg
                Debug.Log("hit enemy");
            }
            else
            {
                Debug.Log("Interact with" + other.gameObject.name);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                // do dmg
                Debug.Log("hit enemy");
            }
            else
            {
                Debug.Log("Interact with" + other.gameObject.name);
            }
        }
    }

}
