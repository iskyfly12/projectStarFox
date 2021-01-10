using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool destroyAfter;
    [SerializeField] bool hasDelay;

    [Header("Events")]
    [SerializeField] UnityEvent events;

    [Header("Delay")]
    [SerializeField] float delayTime;

    void Start()
    {
        Execute();
    }

    void Execute()
    {
        if (hasDelay)
            StartCoroutine(DelayEvent());
        else
            events.Invoke();

        if (destroyAfter)
            Destroy(gameObject);
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(delayTime);
        events.Invoke();
    }
}
