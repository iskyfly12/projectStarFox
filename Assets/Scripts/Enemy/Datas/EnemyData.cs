using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/ShipData/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Parameters")]
    public EnemyState state;
    public bool canFire;
    public float trackSpeed;

    [Header("Status")]
    public int maxLife;
    public int scoreValue;
    public int damageToPlayer;

    [ReadOnly] public EnemyBehaviour behaviour;
    [ReadOnly] public int lifeAmount;
}

public enum EnemyBehaviour { None, InPath, Chasing, ReturningPath }
public enum EnemyState { TrackStage, FreeStage }
