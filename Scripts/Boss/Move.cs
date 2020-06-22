using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);

    public Vector2 direction;

    private Vector2 movement;

    Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y
            );
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }
}
