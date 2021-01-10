using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyPath : MonoBehaviour
{
    [Header("Data")]
    public EnemyData data;

    CinemachineDollyCart _dollyCart;
    CinemachineSmoothPath _path;

    void Awake()
    {
        _dollyCart = GetComponent<CinemachineDollyCart>();
        _dollyCart.m_Speed = data.trackSpeed;
        _path = (CinemachineSmoothPath) _dollyCart.m_Path;

        if (data.state == EnemyState.TrackStage)
            _dollyCart.enabled = true;
        else
            _dollyCart.enabled = false;

    }

}
