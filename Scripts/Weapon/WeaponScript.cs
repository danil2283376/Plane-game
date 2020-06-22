using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // переменные дизайнера
    // префаб снаряда для стрельбы
    public Transform shotPrefab;

    // время перезарядки в секундах
    public float shootingRate = 0.25f;

    // перезарядка
    private float shootCooldown;

    private void Start()
    {
        shootCooldown = 0f;
    }

    private void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    //Создайте новый снаряд, если это возможно
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            // присваиваю перезарядке скорость стрельбы
            shootCooldown = shootingRate;
            // Создайте новый выстрел
            var shotTransform = Instantiate(shotPrefab) as Transform;
            //определяет положение
            shotTransform.position = transform.position;

            // Свойство врага
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Сделайте так, чтобы выстрел всегда был направлен на него
            MoveScriptBullet move = shotTransform.gameObject.GetComponent<MoveScriptBullet>();
            if (move != null)
            {
                // в двухмерном пространстве это будет справа от спрайта
                move.direction = this.transform.right;
            }
        }
    }
    // готово ли оружие выпустить новый снаряд
    public bool CanAttack
    {
        get
        {
            // проверяет равна ли перезарядка 0 или меньше возвращает true или false
            return shootCooldown <= 0f; 
        }
    }
}
