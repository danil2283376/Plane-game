using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoritmMobs : MonoBehaviour
{
    public GameObject prefabEnemy;
    public GameObject shotEnemy;

    public static List<GameObject> enemy = new List<GameObject>();

    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        for (int i = 1; i <= 3; i++)
        {
           /*enemy[i] = */Instantiate(
                prefabEnemy,
                new Vector3(cam.transform.position.x + 20, Random.Range(- 7, 7), prefabEnemy.transform.position.z),
                Quaternion.identity
                );
        }
    }

    private void Update()
    {

    }
    // Дать мобам шанс на добавление оружия
    private void AddWeapon()
    {

    }
    // шанс выпадения хилки, возможности стрельбы для игрока
    private void ChanceToDropItem()
    {

    }
    // спавн босса
    private void SpawnBoss()
    {

    }
}
