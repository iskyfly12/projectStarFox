using UnityEngine;

[System.Serializable]
public class EnemyDrop
{
    [Range(0, 100)]
    public int probability;
    public GameObject drop;
}
