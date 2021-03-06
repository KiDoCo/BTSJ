﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5f, distance = 0.75f, jumpForce = 5f;
    [SerializeField] LayerMask ground;
    public Bounds PLcollider;
    public bool stunned;
    private bool End;

    Rigidbody2D rb;

    [HideInInspector]
    public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EventManager.ActionAddHandler(EVENT.endGame, EndGame);
        EventManager.ActionAddHandler(EVENT.Reset, AReset);
    }

    void FixedUpdate()
    {
        if (End) return;

        float animHor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Running", (animHor != 0 && !stunned ?  true : false));

        PLcollider.center = transform.position;
        if (stunned)
            return;

        float hor = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * hor * movementSpeed * Time.deltaTime;

        if (hor < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (hor > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            Jump();

    }

    void Jump()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, distance, ground) ? true : false;

        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("not grounded");
        }
    }

    public IEnumerator StunRecover(float duration)
    {

        while(duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

		stunned = false;
		yield return null;
    }

    public void Recover()
    {
        EventManager.SoundBroadcast(EVENT.PlaySFX, AudioManager.SFXSource, 0);
		StartCoroutine(StunRecover(1.0f));
    }

    public void EndGame()
    {
        End = true;
    }

    private void AReset()
    {
        transform.position = new Vector3(0.0f, 0.5f, 0);
        End = false;
    }

}
