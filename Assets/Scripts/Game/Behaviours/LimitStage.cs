using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IStageLimit limit))
            limit.DOUTurn(transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.forward * (-1), transform.forward * (-10));
        Gizmos.DrawSphere(transform.position + transform.forward * (-1), 1f);
    }
}
