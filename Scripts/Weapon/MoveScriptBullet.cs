using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScriptBullet : MonoBehaviour
{
    // скорость противника
    public Vector2 speed = new Vector2(10, 10);
    // направление движения
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement; // значения перемещения

    Rigidbody2D rb2d;

    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        rb2d = GetComponent<Rigidbody2D>(); // ссылка на RigidBody объекта
    }

    private void Update()
    {
        // перемещение 
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y); // скорость x умножаем на направление по x, так же с y
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement; // передаем в кадре передвежение вектора, физике rigidbody
    }
}
