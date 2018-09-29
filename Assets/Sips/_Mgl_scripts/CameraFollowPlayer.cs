using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    Transform playerPos;

    [SerializeField] Vector3 offset;
    [SerializeField] float moveSpeed;

    public Vector3 currentVelocity;

    void Start()
    {
        playerPos = GameManager.Instance.Player.transform;
    }

    void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");

        Vector3 wantedPosition = new Vector3(playerPos.position.x + offset.x * hor, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, moveSpeed * Time.deltaTime);


        //transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref currentVelocity, moveSpeed * Time.deltaTime, moveSpeed);
    }
}
