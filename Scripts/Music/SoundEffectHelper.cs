using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHelper : MonoBehaviour
{
    // Синглтон
    public static SoundEffectHelper Instance;
    
    public AudioClip explosionSound;
    public AudioClip playerShotSound;
    public AudioClip enemyShotSound;

    private void Awake()
    {
        // регестрируем синглтон
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляр SoundEffectsHelper");
        }
        Instance = this;
    }

    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }

    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    public void MakeEnemyShotSound()
    {
        MakeSound(enemyShotSound);
    }
    // Играть данный звук
    private void MakeSound(AudioClip originalClip)
    {
        // Поскольку это не 3D - звук, его положение на сцене не имеет значения.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }

}
