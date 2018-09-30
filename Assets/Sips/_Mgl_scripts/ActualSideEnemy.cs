using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualSideEnemy : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    bool canStun;
    float stunTimer;

    void FixedUpdate()
    {
        transform.position += -Vector3.right * speed * Time.deltaTime;

        if (stunTimer < 0)
        {
            canStun = true;
        }
        else
        {
            stunTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Stun player");
            GameManager.Instance.Player.stunned = true;
            GameManager.Instance.Player.Recover();
            stunTimer = 5.0f;
            canStun = false;

        }

        if (other.gameObject.tag == "Steve")
        {
            Debug.Log("Stun steve");
            GameManager.Instance.Steve.running = false;
            GameManager.Instance.Steve.timer = 2.0f;
            GameManager.Instance.Steve.state = AnimStates.Stunned;
            stunTimer = 5.0f;
            canStun = false;
        }
    }

}
