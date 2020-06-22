using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoritmSpawnMobs : MonoBehaviour
{
    // префаб противников
    public GameObject prefabEnemyForward;
    public GameObject prefabEnemyAllCorner;
    public GameObject BossEnemy;

    // есть ли на сцене противники
    private bool existEnemy = false;
    // кол - во спавнить
    static private int quantityEnemy = 5;
     // создаю массив с противниками
    private GameObject[] enemy = new GameObject[quantityEnemy];
    // количество убитых
    private CountKills countKills;
    public int wave = 100;

    // Босс
    // можно ли спавнить босса
    private bool spawnBoss = false;
    // кол - во боссов
    private GameObject[] boss;
    // сколько нужно спавнить
    static int quantitySpawnBoss = 1;
    // кол - во живых на сцене
    private int quantityLiveBoss = 0;

    private void Start()
    {
        boss = new GameObject[10];
        // передаю с объекта счетчик
        countKills = GetComponent<CountKills>();
    }

    private void Update()
    {
        if (!spawnBoss)
        {
            if (countKills.countKills <= countKills.total)
            {
                // есть ли на сцене противники
                if (!existEnemy)
                {
                    // создаю противников
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject prefabEnemy = new GameObject();
                        if (i % 2 == 0)
                        {
                            prefabEnemy = prefabEnemyAllCorner;
                        }
                        else
                        {
                            prefabEnemy = prefabEnemyForward;
                        }
                        // спавню противников
                        enemy[i] = Instantiate(
                            prefabEnemy,
                            new Vector2(Camera.main.transform.position.x + 20, Random.Range(-5, 5)),
                            Quaternion.identity
                            );
                    }
                    // на сцене есть противники
                    existEnemy = true;
                }
                else
                {
                    // запускаю проверку противников
                    StartCoroutine(CheckEnemy());
                }
            }
        }
        else
        {
            Boss();
        } 
    }
    // сопрограмма на проверку противников
    private IEnumerator CheckEnemy()
    {
        //кол - во противников на сцене
        int quantityEnemy = 0;
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] != null)
            {
                quantityEnemy++;
            }
        }
        // если противников нет
        if (quantityEnemy == 0)
        {
            // если счетчик заполнился спавнить босса
            if (countKills.NeedKills() <= 0)
            {
                spawnBoss = true;
                existEnemy = false;
                StopCoroutine(CheckEnemy());
            }
            existEnemy = false;
        }

        yield return null;
    }
    // Боссы
    private void Boss()
    {
        // кол - во живых боссов, предсказываю что их 0
        if (quantityLiveBoss == 0)
        {
            // что бы не было больше 10 боссов, иначе импосибл
            if (quantitySpawnBoss <= 10)
            {
                // спавн босса(ов)
                Vector3 mainCamera = Camera.main.transform.position;
                for (int i = 0; i < quantitySpawnBoss; i++)
                {
                    boss[i] = Instantiate(
                        BossEnemy,
                        new Vector3(mainCamera.x + 20, Random.Range(mainCamera.y - 5, mainCamera.y + 5), - 10),
                        Quaternion.identity
                        );
                    quantityLiveBoss++;
                }
            }
            else
            {
                // если боссов стало больше 10, начать по новой
                quantitySpawnBoss = 1;
            }
        }
        else
        {
            // проверяю остались ли живые боссы
            int quantityBoss = 0;
            for (int i = 0; i < boss.Length; i++)
            {
                if (boss[i] != null)
                {
                    quantityBoss++;
                }
            }
            // если нет живых
            if (quantityBoss == 0)
            {
                // установить живых боссов на 0
                quantityLiveBoss = 0;
                // кол - во в следующий раз спавнить
                //quantitySpawnBoss++;
                if (quantitySpawnBoss <= 10)
                {
                    quantitySpawnBoss++;
                }
                else
                {
                    quantitySpawnBoss = 1;
                }
                // спавнить босса в следующем кадре
                spawnBoss = false;
                // начать следующий уровень
                countKills.nextlvl = true;
                // найти камеру для возобновления игры
                MoveCamera render = GameObject.Find("Render").GetComponent<MoveCamera>();
                render.speed = 3;
                countKills.total += wave; 
                countKills.NextLvl();
            }
        }
    }
}
