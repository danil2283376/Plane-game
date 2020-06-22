using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy1 : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = new Vector2(-1, 0);

    Rigidbody2D rbd2;
    Camera cam;
    Vector3 NewPos; // новая позиция
    int typeMove; // тип движения
    // ограничивать противника границами
    private bool borderEnemy = false;

    private void Start()
    {
        // получаю физику противника
        rbd2 = GetComponent<Rigidbody2D>();
        // получаю главную камеру
        cam = Camera.main;
        // вид движения
        typeMove = Random.Range(0, 45);

        if (typeMove >= 21 && typeMove <= 40)
        {
            borderEnemy = true;
            MoveAllCorner();
        }
        if (typeMove >= 41 && typeMove <= 45)
        {
            borderEnemy = true;
        }
    }

    private void Update()
    {
        if (borderEnemy)
        {
            transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, cam.transform.position.x, cam.transform.position.x + 15),
                    Mathf.Clamp(transform.position.y, cam.transform.position.y - 5, cam.transform.position.y + 5),
                    transform.position.z
                    );
        }
        if (typeMove >= 1 && typeMove <= 20)
        {
            MoveDefault();
        }
        //if (typeMove >= 41 && typeMove <= 45)
        //{
            // телепорт
        //}
    }

    // обычное движение
    private void MoveDefault()
    {
        Vector2 _moveFrom = transform.position;
        Vector2 _moveTo = new Vector2(cam.transform.position.x - 25, transform.position.y);
        transform.position = Vector2.Lerp(_moveFrom, _moveTo, Time.deltaTime / 1f);
    }

    //двигаться во все стороны
    private void MoveAllCorner()
    {
        NewPos = new Vector3(
            Random.Range(cam.transform.position.x + 10, cam.transform.position.x + 15),
            Random.Range(cam.transform.position.y - 5, cam.transform.position.y + 5),
            transform.position.z
            );

        gameObject.AddComponent<MoveAction>().MoveWithSpeed(speed, NewPos, DelayRandomTime);
    }

    // движение телепортом
    private void MoveTeleport()
    {
        // доделать телепорт противников
    }

    // задержка после движения
    private void DelayRandomTime()
    {
        float DelayTime = Random.Range(0, 2);
        gameObject.AddComponent<TimeAction>().Delay(DelayTime, MoveAllCorner);
    }
}
