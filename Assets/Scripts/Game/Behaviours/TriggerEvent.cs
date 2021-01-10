using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool destroyAfter;
    [SerializeField] bool hasDelay;
    [SerializeField] bool interactWithTag;

    [Header("Events")]
    [SerializeField] UnityEvent events;

    [Header("Delay")]
    [SerializeField] float delayTime;

    [Header("Tag")]
    [SerializeField] string interactTag;

    void Execute()
    {
        if (hasDelay)
        {
            StartCoroutine(DelayEvent()); 
            return;
        }
        else
        {
            events.Invoke();
        }

        if (destroyAfter)
            Destroy(gameObject);
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(delayTime);
        events.Invoke();

        if (destroyAfter)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactWithTag && other.transform.CompareTag(interactTag))
            Execute();
        else if (!interactWithTag)
            Execute();
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactWithTag && other.transform.CompareTag(interactTag))
            Execute();
        else if (!interactWithTag)
            Execute();
    }
}
