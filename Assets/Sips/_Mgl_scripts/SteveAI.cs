using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum AnimStates { Struck, Running, Stunned, Dead }
public class SteveAI : MonoBehaviour
{

    public AnimStates state;
    public bool distance_debug = true;
    private Animator animController;
    public Bounds Stevebounds;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject goal;
    public bool steveAlive;
    public float timer;
    public bool running;

    private void Awake()
    {
        animController = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        steveAlive = true;
        EventManager.ActionAddHandler(EVENT.Reset, AReset);
        timer = 3.5f;
    }

    void FixedUpdate()
    {

        if (!steveAlive)
        {
            state = AnimStates.Dead;
            return;
        }

        if (timer <= 0)
        {
            running = true;
        }
        else
            running = false;

        Stevebounds.center = transform.position;
        float distance = Vector3.Distance(transform.position, goal.transform.position);
        if (distance <= 1f && steveAlive)
        {
            Win();
            running = false;
            return;
        }



        if (!running)
        {
            timer -= Time.deltaTime;
            return;
        }
        else
        {
            state = AnimStates.Running;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (distance_debug)
        {
            Debug.Log(distance);
        }

    }

    private void LateUpdate()
    {
        AnimChanger();
    }

    private void AnimChanger()
    {


        if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveRuns"))
        {
            animController.SetBool("Running", state == AnimStates.Running);
        }

        if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveStunned"))
        {
            if (state == AnimStates.Stunned)
            {
                EventManager.SoundBroadcast(EVENT.PlaySFX, AudioManager.SFXSource, 2);
                animController.SetTrigger("Stunned");
            }
        }

        if (!animController.GetCurrentAnimatorStateInfo(0).IsName("SteveDies"))
        {
            if (state == AnimStates.Dead)
            {
                animController.SetTrigger("Died");
            }

        }
    }

    void Win()
    {
        Debug.Log("Steve saved!");
        Destroy(gameObject);
    }

    private void AReset()
    {
        transform.position = new Vector3(-6.0f, 0, 0);
        steveAlive = true;
        state = AnimStates.Struck;
        animController.SetTrigger("Struck");
        timer = 3.5f;
    }
}

