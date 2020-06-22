using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // скорость движения
    public Vector2 speed = new Vector2(50, 50);
    // направление движения
    private Vector2 movement;

    WeaponScript[] weapons;

    Rigidbody2D rb2d; // создал объект с Rigidbody

    static private Transform playerRotate;
    private void Start()
    {
        playerRotate = gameObject.GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>(); // передал значения в rigidbody
        weapons = GetComponentsInChildren<WeaponScript>();
    }

    private void Update()
    {
        // проверка на столкновение
        if (gameObject.transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // границы камеры
        Camera cam = Camera.main;
        float distance = transform.position.z - Camera.main.transform.position.z;
        float leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + 1;
        float rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - 1;
        float topBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + 1;
        float downBorder = cam.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - 1;

        // извлечь информацию оси
        float inputX = Input.GetAxis("Horizontal"); // получаю инфу о передвижение по горизонтали
        float inputY = Input.GetAxis("Vertical");   // получаю инфу о передвижение по вертикали

        transform.position = new Vector3 // проверяю находится ли игрок в центре установленных рамок
            (
                Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                Mathf.Clamp(transform.position.y, topBorder, downBorder),
                transform.position.z
            );

        movement = new Vector2(speed.x * inputX, speed.y * inputY); // создаю новый вектор движения предаю скорость по x,y

        // стрельба
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if (shoot)
        {
            //WeaponScript weapon = GetComponent<WeaponScript>();
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null)
                {
                    // ложь, так как игрок не враг
                    weapon.Attack(false);
                    SoundEffectHelper.Instance.MakePlayerShotSound();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement; // все значения скорости вектора присваиваю физике объекта rigidbody
    }

    private void OnDestroy()
    {
        transform.parent.gameObject.AddComponent<GameOverScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool damagePlayer = false;

        // Столкновение с врагом
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // Смерть врага
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(enemyHealth.hp);
            }
            damagePlayer = true;
        }
        // Повреждения у игрока
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
}
