using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public bool isLooping = false;

    private List<Transform> backgroundPart;

    private void Start()
    {
        // Корневую пустышку нужно зациклить?
        if (isLooping)
        {
            // инициализирую список
            backgroundPart = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                // получаю ребенка у корневого объекта
                Transform child = transform.GetChild(i);
                // проверяю есть ли у него объект рендерера
                if (child.GetComponent<Renderer>() != null)
                    backgroundPart.Add(child);
            }
            // выполняю сортировку по x кто левее тот первый в списке
            backgroundPart = backgroundPart.OrderBy(t => t.position.x).ToList();
        }
    }

    private void Update()
    {
        Vector3 move = new Vector3(speed.x * direction.x, speed.y * direction.y, 0); // движение корневого объекта
        move *= Time.deltaTime; // движение умножаю на скорость отрисовки кадра
        transform.Translate(move); // задаю движение корневому объекту

        if (isLooping) // корневой объект зацикленный
        {
            // узнаю координаты камеры
            Vector3 pointCamera = Camera.main.transform.position;
            // получаю последний объекта списка
            Transform firstChild = backgroundPart.FirstOrDefault();
            // проверяю есть ли ссылка на объект
            if (firstChild != null)
            {
                // Проверяю ушел ли объект налево
                if ((pointCamera.x - 30) > firstChild.transform.position.x)
                {
                    // получаю последний объект списка
                    Transform lastChild = backgroundPart.LastOrDefault();
                    // вычитаю из последнего расстояние по x до первого элемента
                    Vector2 distance = new Vector2(lastChild.position.x - firstChild.position.x, 0);
                    // Vector3 с дефолтными значениями
                    Vector3 bias = default;
                    // узнаю объект является ли платформой
                    if (transform.gameObject.tag == "Platform")
                    {
                        // передаю значения
                        bias = new Vector3(firstChild.position.x + 50, Random.Range(-5, 5),transform.position.z);
                        // двигаю элементы
                        firstChild.position = bias;
                    }
                    else if (transform.gameObject.tag == "Background")
                    {
                        bias = new Vector3(distance.x + 20.45f, firstChild.transform.position.y, firstChild.position.z);
                        firstChild.transform.Translate(bias);
                    }
                    // удаляю найденный объект
                    backgroundPart.Remove(firstChild);
                    // добавляю нужный объект в конец списка
                    backgroundPart.Add(firstChild);
                }
            }
        }
    }
}
