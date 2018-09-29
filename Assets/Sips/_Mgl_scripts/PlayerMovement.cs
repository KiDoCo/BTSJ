using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5f, distance = 0.75f, jumpForce = 5f;
    [SerializeField] LayerMask ground;
    public Bounds PLcollider;
    public bool stunned;

    Rigidbody2D rb;

    [HideInInspector]
    public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    void FixedUpdate()
    {
        PLcollider.center = transform.position;
        float animHor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Running", (animHor != 0 && !stunned ? true : false)); 
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

    public void Recover()
    {
        StartCoroutine(StunRecover(2.0f));
    }

    public IEnumerator StunRecover(float duration)
    {

        yield return new WaitForSeconds(duration);
        Debug.Log("Stunned change");
            stunned = false;
        yield return null;
    }


}
