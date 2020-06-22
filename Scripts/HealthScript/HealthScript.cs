using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : AbstractCheckBuy
{
    // нахожу объект в иерархии
    private GameObject scriptCountKills;

    private void Awake()
    {
        // вызываю при респавне объекта
        scriptCountKills = GameObject.Find("Scripts");

        if (gameObject.name == "player")
        {
            base.textNameItem = "Buy Health Point(+100)";
            
            if (base.ExistWord())
            {
                hp += 100;
                HealthPlayer healthPlayerText = gameObject.GetComponent<HealthPlayer>();
                healthPlayerText.SubtractionHP(0, 200);
            }
        }
    }

    public int hp = 0; // хитпоинты

    public bool isEnemy = true; // враг или игрок

    public void Damage(int damageCount) // наносим урон и проверяем убило ли
    {
        hp -= damageCount; // отнимаем от жизней урон
        if (gameObject.tag == "Player")
        {
            // передаю урон в интерфейс
            gameObject.GetComponent<HealthPlayer>().SubtractionHP(damageCount, hp);
        }

        if (hp <= 0)
        {
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectHelper.Instance.MakeExplosionSound();
            // смерть
            if (gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
            if (gameObject.tag == "Enemy")
            {
                // создаю объект интерфейса
                CountKills countKillsEnemy = scriptCountKills.GetComponent<CountKills>();
                // после смерти противника присваиваю + 1
                countKillsEnemy.Kills();
                // запускаю метод по обновлению текста
                countKillsEnemy.CountKillsEnemy();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // это выстрел?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            // Избегайте дружественного огня
            if (shot.isEnemyShot != isEnemy) // если выстрел противника не равен выстрелу противника выполнить
            {
                Damage(shot.damage);

                Destroy(shot.gameObject);
            }
        }
    }
}
