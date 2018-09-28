using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpEnemy : MonoBehaviour {

    private Collider2D boxCollider;
    [SerializeField] LayerMask ground;
    private float distance = 1.0f;
    private bool canMove = false;
    private bool canStun = true;
    private float stunTimer;
    private float moveValue = 5.0f;
    private Vector2 m_velocity;

    [Space(10)]
    public float groundLevelAlive;
    public float groundLevelDead;
    public float jumpSpeed;
    internal bool m_jumping;
    private bool descend;

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {

    }

    private void OnBecameVisible()
    {
        Debug.Log("Visible");
        canMove = true;
    }

    private void StunEntity(object o)
    {
        //insert logic for stun here plz plox
    }

    private void Timer()
    {
        if (stunTimer < 0)
        {
            canStun = true;
        }
        else
        {
            stunTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        m_velocity.y = m_jumping ? m_velocity.y - 9.81f * Time.deltaTime : 0;
        Timer();
       // m_jumping = Physics2D.Raycast(transform.position, -Vector3.up, distance, ground) ? true : false;
        if (!canStun)
            return;
        else
        {
            transform.position += new Vector3(0, m_velocity.y, 0) * Time.deltaTime;
            Jump();
        }


        JumpDate();


        if (boxCollider.bounds.Intersects(GameManager.Instance.Player.PLcollider))
        {
            Debug.Log("Getting here");
            StartCoroutine(GameManager.Instance.Player.StunRecover(1.5f));
            GameManager.Instance.Player.stunned = true;
            Debug.Log("stunning");
            stunTimer = 5.0f;
            canStun = false;

        }

    }

    private void JumpDate()
    {
        if (!m_jumping) return;

        if (transform.position.y < groundLevelDead || transform.position.y > groundLevelAlive)
        {
            return;
        }

        m_jumping = false;
        transform.position = new Vector3(transform.position.x, groundLevelAlive, transform.position.z);
    }

    private void Jump()
    {
        if (m_jumping) return;

        m_velocity.y = jumpSpeed;
        m_jumping = true;
        transform.position += Vector3.up * 0.025f;
    }

    private void TakeDamage()
    {
        Destroy(gameObject);
    }
}
