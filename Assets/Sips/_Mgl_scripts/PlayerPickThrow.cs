using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickThrow : MonoBehaviour
{
    List<GameObject> nearObjects = new List<GameObject>();
    GameObject holdObject;

    float gravity = -18;

    [SerializeField] Transform itemPos;
    [SerializeField] KeyCode action;

    public bool holding;
    float timer = 0.3f;

    [SerializeField] Transform target;
    [SerializeField] float height = 25;

    void Update()
    {

        if(Input.GetKeyDown(action) && nearObjects.Count != 0 && !holding)
        {
            PickUp(nearObjects[0]);
        }

        if(holding)
        {
            timer -= Time.deltaTime;
            DrawRay();
        }

        if (Input.GetKeyDown(action) && holding && timer <= 0)
        {
            Throw();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pickup")
            nearObjects.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickup")
            nearObjects.Remove(other.gameObject);
    }

    void PickUp(GameObject pickup)
    {
        holding = true;
        holdObject = pickup;
        nearObjects.Remove(nearObjects[0]);

        pickup.transform.position = itemPos.position;
        pickup.GetComponent<Rigidbody2D>().isKinematic = true;
        pickup.GetComponent<Rigidbody2D>().gravityScale = 0;
        holdObject.transform.parent = transform;
    }

    ThrowData CalculateThrowData()
    {
        float displacementY = target.position.y - holdObject.transform.position.y;
        Vector2 displacementX = new Vector2(target.position.x - holdObject.transform.position.x, 0);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector2 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector2 velocityX = displacementX / time;

        return new ThrowData(velocityX + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawRay()
    {
        ThrowData throwData = CalculateThrowData();
        Vector3 previousDrawPoint = holdObject.transform.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * throwData.timeToTarget;
            Vector3 displacement = throwData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 4;
            Vector3 drawPoint = holdObject.transform.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }

    void Throw()
    {
        Physics.gravity = Vector3.up * gravity;
        holdObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        timer = 0.3f;
        holding = false;
        holdObject.transform.parent = null;
        holdObject.GetComponent<Rigidbody2D>().isKinematic = false;
        holdObject.GetComponent<Rigidbody2D>().velocity = CalculateThrowData().initialVelocity;


        holdObject = null;
    }

    struct ThrowData
    {
        public Vector3 initialVelocity;
        public float timeToTarget;

        public ThrowData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

}
