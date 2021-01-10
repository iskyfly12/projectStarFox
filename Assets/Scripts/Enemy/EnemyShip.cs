using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class EnemyShip : MonoBehaviour, IDamagable
{
    [Header("Enemy Data")]
    public EnemyData data;

    [Header("Sounds")]
    [SerializeField] EffectsSounds _hitSound;
    [SerializeField] EffectsSounds _destroySound;

    [Header("References")]
    [SerializeField] private Transform _model;

    [Header("Drop")]
    [SerializeField] private bool hasDrop;
    [SerializeField] private EnemyDrop[] drops;

    private Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        data.lifeAmount = data.maxLife;
    }

    void OnDrop()
    {
        if (!hasDrop)
            return;

        int total = 0;
        for (int i = 0; i < drops.Length; i++)
            total = drops[i].probability;

        int randomValue = UnityEngine.Random.Range(0, total);
        int currentValue = 0;
        for (int i = 0; i < drops.Length; i++)
        {
            if (currentValue <= randomValue && randomValue < currentValue + drops[i].probability)
                Instantiate(drops[i].drop, transform.position, Quaternion.identity);
            
            currentValue += drops[i].probability;
        }
    }


    public void SetDamage(int damage)
    {
        data.lifeAmount -= damage;

        if (data.lifeAmount <= 0) { Destroy(); return; }

        float timeShake = .25f;
        _model.DOShakePosition(timeShake);
        _model.DOLocalMove(Vector3.zero, timeShake).SetDelay(timeShake);

        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(_hitSound));
    }

    public void Destroy()
    {
        PlayerScore.OnAddScore(data.scoreValue);

        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(_destroySound));
        ObjectPoolerSystem.SpawFromPool("ExplosionEnemy", transform.position, Quaternion.identity);

        _model.gameObject.SetActive(false);
        _collider.enabled = false;

        OnDrop();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<IDamagable>().SetDamage(data.damageToPlayer);
    }
}
