using UnityEngine;

public class BossScript : MonoBehaviour
{
    private bool hasSpawn;

    // Параметры компонентов
    private Move moveScript;
    private WeaponScript[] weapons;
    private Animator animator;
    private SpriteRenderer[] renderers;
    private Collider2D collider2D;

    // Поведение босса (не совсем AI)
    public float minAttackCooldown = 0.5f;
    public float maxAttackCooldown = 2f;

    private float aiCooldown;
    private bool isAttacking;
    private Vector2 positionTarget;

    private void Awake()
    {
        // Получить оружие только один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        // Отключить скрипты при отсутсвии спауна
        moveScript = GetComponent<Move>();

        // получить аниматор
        animator = GetComponent<Animator>();

        // Получить рендеры в детях
        renderers = GetComponentsInChildren<SpriteRenderer>();

        // Получить Collider2D
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        hasSpawn = false;
        // Отключить все
        // -- Collider
        collider2D.enabled = false;
        // -- Движение
        moveScript.enabled = false;
        // -- Стрельба
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }

        // Дефолтное поведение
        isAttacking = false;
        aiCooldown = maxAttackCooldown;
    }

    private void Update()
    {
        // Проверим появился ли враг
        if (hasSpawn == false)
        {
            if (Camera.main.transform.position.x + 20 <= transform.position.x)
            {
                Spawn();
            }
        }
        else
        {
            // AI
            // Перемещение или атака
            aiCooldown -= Time.deltaTime;

            if (aiCooldown <= 0f)
            {
                isAttacking = !isAttacking;
                aiCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                positionTarget = Vector2.zero;

                // Настроить или сбросить анимацию атаки
                animator.SetBool("Attack", isAttacking);
            }
            // Атака
            if (isAttacking)
            {
                moveScript.direction = Vector2.zero;

                foreach (WeaponScript weapon in weapons)
                {
                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                    {
                        weapon.Attack(true);
                        SoundEffectHelper.Instance.MakeEnemyShotSound();
                    }
                }
            }
            // Перемещение
            else
            {
                // Выбрать цель?
                if (positionTarget == Vector2.zero)
                {
                    // Получить точку на экране, преобразовать ее в цель в игровом мире
                    Vector2 randomPoint = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

                    positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
                }

                // У нас есть цель ? Если да, найти новую
                if (collider2D.OverlapPoint(positionTarget))
                {
                    // Сбросить, выбрать в следующем кадре
                    positionTarget = Vector2.zero;
                }
                // Идти к точке
                Vector3 direction = ((Vector3)positionTarget - this.transform.position);

                // Помните об использовании
                moveScript.direction = Vector3.Normalize(direction);
            } 
        } 
    }

    private void Spawn()
    {
        hasSpawn = true;

        // Включить все
        // -- Коллайдер
        collider2D.enabled = true;
        // -- Движение
        moveScript.enabled = true;
        // -- Стрельба
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }

        //// Остановить основной скроллинг
        //foreach (MoveCamera moveCamera in FindObjectsOfType())
        //{
        //    if (moveCamera)
        //    {
        //        moveCamera.speed = 0f;
        //    }
        //}
    }
    private void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        // В случае попадания изменить анимацию
        ShotScript shot = otherCollider2D.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot == false)
            {
                // Прекратить атаку и начать движение
                aiCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                isAttacking = false;

                // Изменить анимацию
                animator.SetTrigger("Hit");
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Небольшой совет: Вы можете отобразить отладочную информацию в вашей сцене с Гимзо
        if (hasSpawn && isAttacking == false)
        {
            Gizmos.DrawSphere(positionTarget, 0.25f);
        }
    }
}
