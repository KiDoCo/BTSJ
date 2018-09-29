﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] KeyCode actionKey;

    [SerializeField] float attackTimer, attackCooldown = .3f;
    bool attacking;

    Animator anim;

    private void Start()
    {
        anim = GameManager.Instance.Player.anim;
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey) && !attacking)
        {
            Attack();
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
            }
        }

        //if(GetComponent<Collider2D>().bounds.Intersects())
    }

    void Attack()
    {
        attacking = true;
        attackTimer = attackCooldown;

        anim.SetTrigger("Attack");

        if (enemies.Count != 0)
        {
            enemies[0].GetComponent<IEnemy>().TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != null)
        {
            if (other.gameObject.tag == "Enemy")
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
