using UnityEngine;

public class RingItem : UpgradeItem
{
    [Header("Ring Recover")]
    public bool isGoldRing;
    [Range(0, 100)]
    public int amountRecover;

    [Header("Animation")]
    public AnimationClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IUpgradeItem item))
                item.HealRing(amountRecover, isGoldRing);

            TriggerEnter(other.transform);
            _animation.Play(clip.name);
        }
    }
}
