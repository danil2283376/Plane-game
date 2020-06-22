using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript1 : MonoBehaviour
{
    // компоненты босса
    private BoxCollider2D boxCollider2d;
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private HealthScript healthScript;
    private Move move;
    private WeaponScript[] weapons;

    private bool hasSpawn;
    private Vector2 targetMove;

    // действия босса
    public float minCooldown = 0.5f;
    public float maxCooldown = 2f;

    private float cooldownBoss;
    private bool isAttaked;
    
    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        healthScript = GetComponent<HealthScript>();

        move = GetComponent<Move>();

        weapons = GetComponentsInChildren<WeaponScript>();
    }

    private void Start()
    {
        boxCollider2d.enabled = false;

        animator.enabled = false;

        healthScript.enabled = false;

        move.enabled = false;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
        isAttaked = false;
        cooldownBoss = maxCooldown;
    }

    private void Update()
    {
        if (!hasSpawn)
        {
            Spawn();
        }
        else
        {
            cooldownBoss -= Time.deltaTime;
            if (cooldownBoss <= 0f)
            {
                isAttaked = !isAttaked;
                cooldownBoss = Random.Range(minCooldown, maxCooldown);
                targetMove = Vector2.zero;
                animator.SetBool("Attack", isAttaked);
            }

            // атака
            if (isAttaked)
            {
                move.direction = Vector2.zero;

                foreach (WeaponScript weapon in weapons)
                {
                    if (weapon != null && weapon.CanAttack && weapon.enabled)
                    {
                        weapon.Attack(true);
                        SoundEffectHelper.Instance.MakeEnemyShotSound();
                    }
                }
            }
            // двигаемся
            else
            {
                // у босса не установлена точка ?
                if (targetMove == Vector2.zero)
                {
                    // задать позицию боссу
                    Vector2 newDirection = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                    // получить точку в мировом пространстве экрана
                    targetMove = Camera.main.ViewportToWorldPoint(newDirection);
                }
                // босс в точке?
                if (boxCollider2d.OverlapPoint(targetMove))
                {
                    // перемещаться вновь и вновь
                    targetMove = Vector2.zero;
                }

                Vector2 direction = ((Vector3)targetMove - transform.position);
                move.direction = Vector3.Normalize(direction);
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        ShotScript shot = otherObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot == false)
            {
                cooldownBoss = Random.Range(minCooldown, maxCooldown);
                isAttaked = false;

                animator.SetTrigger("Hit");
            }
        }
    }

    private void Spawn()
    {
        MoveCamera moveCamera = GameObject.Find("Render").GetComponent<MoveCamera>();
        moveCamera.speed = 0f;

        hasSpawn = true;

        boxCollider2d.enabled = true;

        animator.enabled = true;

        healthScript.enabled = true;

        move.enabled = true;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
