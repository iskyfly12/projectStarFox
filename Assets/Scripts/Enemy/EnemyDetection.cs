using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour, IDetectable
{
    [Header("References")]
    [SerializeField] private GameObject _arrowDetected;

    public Transform GetTransform() => this.transform;

    public void ShowArrow(bool state) => _arrowDetected.SetActive(state);
}
