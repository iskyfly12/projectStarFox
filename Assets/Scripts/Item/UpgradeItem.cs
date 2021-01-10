using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animation))]
public class UpgradeItem : MonoBehaviour
{
    [Header("Sound")]
    public EffectsSounds effectsSounds;

    [Header("Settings")]
    public float speedMove = 2;
    public float speedScale = 1;
    public float speedRotation = 1;
    public float speedAnimation = 5;

    [HideInInspector] public BoxCollider _boxCollider;
    [HideInInspector] public Animation _animation;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _animation = GetComponent<Animation>();
    }

    public void TriggerEnter(Transform objTriggered)
    {
        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(effectsSounds));

        transform.parent = objTriggered.transform;

        transform.DOScale(Vector3.zero, speedScale);
        transform.DOLocalMove(Vector3.zero, speedMove);
        transform.DOLocalRotate(Vector3.zero, speedRotation);

        _boxCollider.enabled = false;
        _animation[_animation.clip.name].speed = speedAnimation;

        Destroy(gameObject, 2);
    }
}
