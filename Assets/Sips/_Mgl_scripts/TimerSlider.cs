using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{

    [SerializeField] Slider timer;
    [SerializeField] float timeLosingSpeed = 0.1f, timeChangeSpeed = 1f;
    float maxValue;

    void OnEnable()
    {
        timer.value = timer.maxValue; // reset
        maxValue = timer.maxValue;
    }

    void Update()
    {
        timer.value -= timeLosingSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.G))
        {
            AddTime(.5f);
        }
        
        if(Input.GetKeyDown(KeyCode.H))
        {
            LoseTime(.2f);
        }

        if(timer.value <= 0f)
        {
            Debug.Log("Steve died here");
            //RIP STEVE JOBBARNA
            GameManager.Instance.Steve.steveAlive = false;
        }
    }

    public void AddTime(float amount)
    {
        float currentValue = timer.value;
        float wantedValue = currentValue + amount;

        StartCoroutine(ChangeTime(currentValue, wantedValue, timeChangeSpeed));
        Debug.Log("Gain time");
    }

    public void LoseTime(float amount)
    {
        float currentValue = timer.value;
        float wantedValue = currentValue - amount;

        StartCoroutine(ChangeTime(currentValue, wantedValue, timeChangeSpeed));
        Debug.Log("Lost time");
    }

    IEnumerator ChangeTime(float current, float wanted, float duration)
    {
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            timer.value = Mathf.Lerp(current, wanted, percent);
            yield return null;
        }
    }

}
