using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Создание экземпляра частиц
public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляров SpecialEffectsHelper!");
        }

        Instance = this;
    }
    // Создать взрыв в данной точке
    public void Explosion(Vector3 position)
    {
        //Дым над водой
        instantiate(smokeEffect, position);

        // да - даам

        // Огонь в небе
        instantiate(fireEffect, position);
    }
    // Создание экземпляра системы частиц из префаба
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
            prefab,
            position,
            Quaternion.identity
            ) as ParticleSystem;

        // Убедитесь, что это будет уничтожено
        Destroy(
            newParticleSystem.gameObject,
            newParticleSystem.startLifetime
            );

        return newParticleSystem;
    }
}
