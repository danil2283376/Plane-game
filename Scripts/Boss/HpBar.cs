using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Sprite[] stageBoss;

    HealthScript healthScript;
    SpriteRenderer spriteBody;

    Animator animator;
    private void Start()
    {
        healthScript = GetComponent<HealthScript>();
        spriteBody = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (healthScript.hp <= 75 && healthScript.hp >= 50)
        {
            spriteBody.sprite = stageBoss[0];
        }
        if (healthScript.hp < 50 && healthScript.hp >= 25)
        {
            spriteBody.sprite = stageBoss[1];
        }
        if (healthScript.hp < 25 && healthScript.hp > 0)
        {
            spriteBody.sprite = stageBoss[2];
        }
        // может потом исправить
        //if (healthScript.hp <= 0)
        //{
        //    // запустить анимацию смерти
        //    animator.SetBool("Die", true);
        //}
    }
}
