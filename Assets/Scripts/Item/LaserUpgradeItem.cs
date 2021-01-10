using UnityEngine;

public class LaserUpgradeItem : UpgradeItem
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IUpgradeWeapon item))
                item.UpgradeWeapon();

            TriggerEnter(other.transform);
        }
    }
}
