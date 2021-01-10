using UnityEngine;

public class LifeItem : UpgradeItem
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IUpgradeItem item))
                item.AddLife();

            TriggerEnter(other.transform);
        }
    }
}
