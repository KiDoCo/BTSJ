using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrootLogic : MonoBehaviour
{

    Transform playerPos;
    bool left;

    [SerializeField] float maxDistance, speed = 3;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerPos = GameManager.Instance.Player.transform;
    }

    void FixedUpdate()
    {
        if((transform.position.x - playerPos.position.x) < 0)
        {
            left = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            left = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        float distance = Mathf.Abs(transform.position.x - playerPos.position.x);

        if(distance > maxDistance)
        {
            if(left)
            {
                transform.position += -Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

    }
}
