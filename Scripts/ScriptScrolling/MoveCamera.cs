using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 4f;
    public Vector2 direction = new Vector2 (1, 0);
    [Range (4f, 8f)]
    public float visibleCamera;

    private void Update()
    {
        //Vector3 moveCamera = new Vector3(speed * direction, 0);
        //moveCamera *= Time.deltaTime;

        //transform.Translate(moveCamera);

        Camera.main.transform.Translate(direction * speed * Time.deltaTime);
    }
}
