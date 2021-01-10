using UnityEngine;

public class SmartBombItem : UpgradeItem
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IUpgradeItem item))
                item.UpgradeSmartBomb();

            TriggerEnter(other.transform);
        }
    }
}
