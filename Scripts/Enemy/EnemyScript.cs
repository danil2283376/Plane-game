using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // за кем наблюдать
    public Transform playerTarget;
    // скорость поворота
    public float speed = 5f;

    // спавн начался
    private bool hasSpawn = false;
    // скрипт движения
    private MoveEnemy1 moveScript1;
    // скрипт оружий
    private WeaponScript[] weapons;
    //Script на сцене
    private GameObject scriptCountKills;

    private void Awake()
    {
        // Получить оружие только один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        // Отключить скрипты, чтобы деактивировать объекты при отсутсвии спавна
        moveScript1 = GetComponent<MoveEnemy1>();

        scriptCountKills = GameObject.Find("Scripts");
    }

    private void Start()
    {
        // отключаю BoxCollider пока за камерой
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        // отключаю движения пока за камерой
        moveScript1.enabled = false;
        // отключаю оружие пока за камерой
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    private void Update()
    {

        // проверяю дошла ли камера до противников
        if (gameObject.transform.position.x < (Camera.main.transform.position.x + 15))
        {

            // начинаю спавн
            if (hasSpawn == false) 
            {
                Spawn();
            }
            // если спавн начат
            else
            {

                // оружие на изготовку
                foreach (WeaponScript weapon in weapons)
                {
                    // если на оружие есть ссылка && оно активно && оружие может атаковать
                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                    {
                        // передаю в метод по аттаке сигнал на то что можно аттаковать
                        weapon.Attack(true);
                    }
                }

                // сделать что бы противники смотрели на игрока
                // https://www.youtube.com/watch?v=S7-unUDLI6A
                //Vector3 direction = (playerTarget.position - transform.position).normalized;
                //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);

                // если противник ушел за левую часть экрана, уничтожить объект
                if (gameObject.transform.position.x < (Camera.main.transform.position.x - 15))
                {
                    // уничтожение объекта
                    CountKills countKillsEnemy = scriptCountKills.GetComponent<CountKills>();
                    // после смерти противника присваиваю + 1
                    countKillsEnemy.Kills();
                    // обновляю счетчик
                    countKillsEnemy.CountKillsEnemy();
                    Destroy(gameObject);
                }
            }
        }
    }
    // метод который начинает спавн
    private void Spawn()
    {
        hasSpawn = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        moveScript1.enabled = true;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
