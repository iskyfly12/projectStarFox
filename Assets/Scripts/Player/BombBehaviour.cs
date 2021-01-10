using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BombBehaviour : MonoBehaviour
{
    [HideInInspector] public bool active;
    [HideInInspector] public bool hasEnemyTarget;
    [HideInInspector] public Transform enemyTransform;
    [HideInInspector] public Vector3 direction;

    [Header("Settings")]
    [SerializeField] LayerMask layerCollision;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float followSpeed;
    [SerializeField] private float timeToDisable;
    [SerializeField] private float timeBeforeExplosion;
    [Range(0, 100)]
    [SerializeField] private int damage;
    [Range(0, 10)]
    [SerializeField] private float collisionRangeExplosion;

    [Header("References")]
    [SerializeField] VisualEffect explosionVFX;
    [SerializeField] GameObject model;

    SphereCollider sphereCollider;

    float t;

    void Awake() => sphereCollider = GetComponent<SphereCollider>();

    void FixedUpdate()
    {
        if (!active)
            return;

        if (hasEnemyTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyTransform.position, followSpeed);
        }
        else
        {
            transform.position += direction * forwardSpeed * Time.deltaTime;
            t += Time.deltaTime;
            if (t > timeToDisable)
            {
                t = 0;
                StartCoroutine(TriggerExplosion());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerCollision) != 0)
        {
            StartCoroutine(TriggerExplosion());

            if (other.transform.TryGetComponent(out IDamagable dmg))
                dmg.SetDamage(damage);
        }
    }

    IEnumerator TriggerExplosion()
    {
        if (enemyTransform != null) { hasEnemyTarget = false; enemyTransform = null; }

        active = false;
        model.SetActive(false);

        sphereCollider.radius = collisionRangeExplosion;

        explosionVFX.Play();

        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.ChargeLaserExplode));

        yield return new WaitForSeconds(timeBeforeExplosion);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        active = true;
        sphereCollider.radius = 1f;
        model.SetActive(true);
    }
}
