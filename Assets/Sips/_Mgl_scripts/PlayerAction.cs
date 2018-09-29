using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] KeyCode actionKey;

    [SerializeField] float attackTimer, attackCooldown = .3f;
    bool attacking;

    private void Start()
    {
        
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
            }
        }
    }

    void Attack()
    {
        attacking = true;
        attackTimer = attackCooldown;

        if(enemies.Count != 0)
        {
            //enemies[0].GetComponent<Health>().TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != null)
        {
            if(other.gameObject.tag == "Enemy")
            {
                enemies.Add(other.gameObject);
            }
            else
            {
                Debug.Log("Interact with" + other.gameObject.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemies.Remove(other.gameObject);
            }
            else
            {
                Debug.Log("Interact with" + other.gameObject.name);
            }
        }
    }

}
