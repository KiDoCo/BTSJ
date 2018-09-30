using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpEnemy : MonoBehaviour, IEnemy
{
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

    public string id
    {
        get
        {
            return "GroundE";
        }
    }

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void OnBecameVisible()
    {
        Debug.Log("Visible");
        canMove = true;
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

        if (!canStun)
            return;

        Jump();
        transform.position += new Vector3(0, m_velocity.y, 0) * Time.deltaTime;
        JumpDate();
        if (boxCollider.bounds.Intersects(GameManager.Instance.Player.PLcollider))
        {
            GameManager.Instance.Player.stunned = true;
            GameManager.Instance.Player.Recover();
            stunTimer = 5.0f;
            canStun = false;
            transform.position = new Vector3(transform.position.x, groundLevelDead, transform.position.z);

        }

        if (boxCollider.bounds.Intersects(GameManager.Instance.Steve.Stevebounds))
        {
            Debug.Log("Stun steve");

            GameManager.Instance.Steve.running = false;
            GameManager.Instance.Steve.timer = 2.0f;
            GameManager.Instance.Steve.state = AnimStates.Stunned;
            stunTimer = 5.0f;
            canStun = false;
            transform.position = new Vector3(transform.position.x, groundLevelDead, transform.position.z);
        }

        if((GameManager.Instance.Player.transform.position - gameObject.transform.position).magnitude >= 150.0f)
        {


            Destroy(gameObject);
        }
    }

    private void JumpDate()
    {

        if (transform.position.y < groundLevelDead)
        {
            m_jumping = false;
            return;
        }

        if (transform.position.y > groundLevelAlive)
        {
            m_jumping = true;
        }

    }

    private void Jump()
    {
        if (m_jumping) return;

        m_velocity.y = jumpSpeed;
        transform.position += Vector3.up * 0.025f;
    }

    public void TakeDamage()
    {

        Destroy(gameObject);
    }
}

public interface IEnemy
    {
    void TakeDamage();
    string id { get; }
    }


