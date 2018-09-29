using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    Transform playerPos;

    [SerializeField] Vector3 offset;
    [SerializeField] float moveSpeed;

    void Start()
    {
        playerPos = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");

        Vector3 wantedPosition = new Vector3(playerPos.position.x + offset.x * hor, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, moveSpeed * Time.deltaTime);
    }
}
