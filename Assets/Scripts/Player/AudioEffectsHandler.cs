using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsHandler : MonoBehaviour
{
    public SoundAudioClip[] soundAudioClips;

    static SoundAudioClip[] soundAudioClipsArray;

    private void Awake() => soundAudioClipsArray = soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public EffectsSounds sound;
        public AudioClip audioClip;
    }

    public static AudioClip GetAudioClip(EffectsSounds sound)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        }
        return null;
    }
}

public enum EffectsSounds
{
    PlayerLaser_Lv1,
    PlayerLaser_Lv2,
    PlayerLaser_Lv3,
    Boost,
    Break,
    TargetLock,
    LaserPickUp,
    BombPickUp,
    LifePickUp,
    ChargeLaser,
    ChargeLaserExplode,
    ChargeLaserRelease,
    SmartBombExplode,
    SmartBombRelease,
    LaserHit,
    LaserHit_NoDamage,
    LaserHit_Reflect,
    BarrelRoll,
    OpenWings,
    CloseWings,
    RingCollect_1,
    RingCollect_2,
    RingCollect_3,
    ThreeRingsCollect,
    HitShield_Lv1,
    HitShield_Lv2,
    HitShield_Lv3,
    DamageObstacle,
    BossDefeat,
    BossExplosion,
    Explosion_1,
    Explosion_2,
    Explosion_3,
    PauseGame,
    Select_UI,
    Start_UI,
}
