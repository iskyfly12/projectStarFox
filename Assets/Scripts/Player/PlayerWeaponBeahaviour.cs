using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerWeaponBeahaviour : MonoBehaviour, IUpgradeWeapon
{
    [Header("Player Settings")]
    public PlayerData data;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("VFX")]
    [SerializeField] VisualEffect chargeLaserVFX;
    [SerializeField] VisualEffect arrowVFX;

    [Header("Lasers")]
    [SerializeField] ParticleSystem[] laserLevels;

    [Header("References")]
    [SerializeField] Transform pointChargeLaser;
    [SerializeField] Transform pointDetect;

    [Header("Layers")]
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _laserColliderLayer;

    private CustomFixedUpdate FU_LogicInstance;
    private Transform detectedEnemy;
    private RaycastHit hit;
    private Vector3 fwd;
    private float holdTimer;
    private bool canFireBomb = true;

    private readonly string string_chargeLaser = "ChargedLaser";
    private readonly string string_arrowCharge = "Arrow Charge";
    private readonly string string_arrowFade = "Arrow Fade";
    private readonly string string_chargeSize = "Size Main";
    private readonly string string_smartBomb = "SmartBomb";

    void Awake()
    {
        FU_LogicInstance = new CustomFixedUpdate(OnFixedUpdate, fps);

        chargeLaserVFX.SetFloat("Time Charge", data.timeCharged);
        arrowVFX.SetFloat("Time Charge", data.timeCharged);

        for (int i = 0; i < laserLevels.Length; i++)
        {
            ParticleSystem.CollisionModule collision = laserLevels[i].collision;
            collision.collidesWith = _laserColliderLayer;
        }

        data.onReleaseLaser += () => ReleaseChargeLaser();
        data.onFireLaser += () => FireWeapon();
        data.onFireSmartBomb += () => StartCoroutine(ReleaseSmartBomb());
    }

    void Update()
    {
        FU_LogicInstance.Update();
    }

    void OnFixedUpdate(float aDeltaTime)
    {
        if (data.chargeWeaponState == ChargeWeaponState.Charging)
            ChargeLaser();

        if (data.enemyDetectionState == EnemyDetectionState.CanDetect)
            DetectEnemy(pointDetect);
    }

    #region LaserWeapon
    public void UpdateLaser()
    {
        if ((int)data.weaponState + 1 >= System.Enum.GetValues(typeof(WeaponState)).Length)
            return;

        data.weaponState = (WeaponState)((int)data.weaponState + 1);

        if ((int)data.weaponState + 1 == System.Enum.GetValues(typeof(WeaponState)).Length)
            data.OnOpenHyperLaser(true);
    }

    //Fire basics lasers
    public void FireWeapon()
    {
        laserLevels[(int)data.weaponState].Play();

        if (data.weaponState == WeaponState.LvOne)
            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.PlayerLaser_Lv1));
        else if (data.weaponState == WeaponState.LvTwo)
            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.PlayerLaser_Lv2));
        else
            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.PlayerLaser_Lv3));
    }
    #endregion

    #region Charge laser
    //Charge laser over time
    void ChargeLaser()
    {
        if (chargeLaserVFX.aliveParticleCount != 0)
            chargeLaserVFX.Play();

        if (holdTimer < data.timeCharged)
        {
            chargeLaserVFX.SetFloat(string_chargeSize, holdTimer);
            arrowVFX.SetFloat(string_arrowCharge, holdTimer);

            holdTimer += Time.fixedDeltaTime;
        }
        else
        {
            holdTimer = 0;
            data.chargeWeaponState = ChargeWeaponState.Charged;
            data.enemyDetectionState = EnemyDetectionState.CanDetect;

            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.ChargeLaser));
        }
    }

    //Release charge laser
    public void ReleaseChargeLaser()
    {
        if (data.chargeWeaponState == ChargeWeaponState.Charged)
        {
            FireBomb(string_chargeLaser);

            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.ChargeLaserRelease));
        }
    }
    #endregion

    #region Smart Bomb
    public IEnumerator ReleaseSmartBomb()
    {
        if (!canFireBomb || data.numSmartBombs <= 0)
            yield break;

        canFireBomb = false;

        FireBomb(string_smartBomb);
        data.OnUpdateSmartBomb(-1);
        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.SmartBombRelease));

        yield return new WaitForSeconds(2f);

        canFireBomb = true;
    }
    #endregion

    #region Logic
    //Detect enemy when laser is charged
    void DetectEnemy(Transform pointLaser)
    {
        fwd = pointLaser.TransformDirection(Vector3.forward);

        if (Physics.BoxCast(pointLaser.position, Vector3.one, fwd, out hit, pointLaser.rotation, 800))
        {
            if (((1 << hit.transform.gameObject.layer) & _enemyLayer) != 0)
            {
                if (hit.transform.TryGetComponent(out IDetectable dtc))
                {
                    detectedEnemy = dtc.GetTransform();
                    dtc.ShowArrow(true);

                    arrowVFX.SetFloat(string_arrowFade, 0);

                    data.enemyDetectionState = EnemyDetectionState.Detected;

                    AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.TargetLock));
                }
            }
        }
    }

    void FireBomb(string bomb)
    {
        BombBehaviour b = ObjectPoolerSystem.SpawFromPool(bomb, pointChargeLaser.position, Quaternion.identity).GetComponent<BombBehaviour>();
        if (detectedEnemy != null)
        {
            b.enemyTransform = detectedEnemy;
            b.hasEnemyTarget = true;
            if (detectedEnemy.TryGetComponent(out IDetectable dtc))
                dtc.ShowArrow(false);

            CleanFire();
        }
        b.direction = pointChargeLaser.TransformDirection(Vector3.forward);
    }

    void CleanFire()
    {
        holdTimer = 0;
        detectedEnemy = null;

        chargeLaserVFX.Stop();
        chargeLaserVFX.SetFloat(string_chargeSize, 0);
        arrowVFX.SetFloat(string_arrowCharge, 0);
        arrowVFX.SetFloat(string_arrowFade, 1);

        data.chargeWeaponState = ChargeWeaponState.Released;
        data.enemyDetectionState = EnemyDetectionState.None;
    }
    #endregion

    #region Interfaces
    public void UpgradeWeapon() => UpdateLaser();

    #endregion
}
