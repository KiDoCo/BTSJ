using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5f, distance = 0.75f, jumpForce = 5f;
    [SerializeField] LayerMask ground;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * hor * movementSpeed * Time.deltaTime;

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
            Debug.Log("not grounded");
    }
}
