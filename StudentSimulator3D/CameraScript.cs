using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /// <summary>
    /// Объект цели, за которым должна следить камера
    /// </summary>
    public Transform Target;


    Vector3 startDistance, moveVec;
    /// <summary>
    /// Вызывается в старте игры и устанавливает расстояние между камерой и игроком
    /// </summary>
    void Start()
    {
        startDistance = transform.position - Target.position;
    }

    /// <summary>
    /// Меняет положение камеры при необходимости
    /// </summary>
    void Update()
    {
        //Камера меняет свое положение только по x
        moveVec = Target.position + startDistance;
        //Камера не будет перемещаться по z
        moveVec.z = 0;
        //камера не будет прыгать вместе с игроком
        moveVec.y = startDistance.y;

        transform.position = moveVec;
    }
}

