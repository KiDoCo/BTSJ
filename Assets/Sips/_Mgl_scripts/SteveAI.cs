﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAI : MonoBehaviour
{

    private enum AnimStates { Struck, Running, Stunned, Dead }
    private AnimStates state;
    public bool distance_debug = true;
    private Animator animController;
    public Bounds Stevebounds;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject goal;
    public bool steveAlive;
    public float timer;
    public bool running;

    private void Awake()
    {
        animController = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        steveAlive = true;
        timer = 1.5f;
    }

    void Update()
    {

        if (timer <= 0)
        {
            running = true;
        }
        Stevebounds.center = transform.position;
        float distance = Vector3.Distance(transform.position, goal.transform.position);
        if (distance <= 1f && steveAlive)
        {
            Win();
            running = false;
            return;
        }

        if (!steveAlive)
        {
            Debug.Log("Dead AF");
            state = AnimStates.Dead;
            return;
        }

        if (!running)
        {
            timer -= Time.deltaTime;
            state = AnimStates.Stunned;
            return;
        }
        else
        {
            state = AnimStates.Running;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (distance_debug)
        {
            Debug.Log(distance);
        }

    }

    private void FixedUpdate()
    {
        AnimChanger();
    }

    private void AnimChanger()
    {
        if (animController.GetCurrentAnimatorStateInfo(0).IsName("SteveStruck")) return;


            if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveRuns"))
        {
            animController.SetBool("Running", state == AnimStates.Running);
        }

        if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveStunned"))
        {
            if (state == AnimStates.Stunned)
            {
                animController.SetTrigger("Stunned");
            }
        }

        if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveDies"))
        {
            if (state == AnimStates.Dead)
            {
                animController.SetTrigger("Died");
            }

        }
    }

    void Win()
    {
        Debug.Log("Steve saved!");
        Destroy(gameObject);
    }
}
