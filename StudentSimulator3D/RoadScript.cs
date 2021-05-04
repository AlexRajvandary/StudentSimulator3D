using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    /// <summary>
    /// Объект иницализируется в среде Unity3D, управляет логикой игры
    /// </summary>
    GameManager GM;

    /// <summary>
    /// Вектор движения дороги
    /// </summary>
    Vector3 moveVec;

    /// <summary>
    /// Объект монет, инициализируется в среде Unity3D
    /// </summary>
    public GameObject CoinObj;

    /// <summary>
    /// шанс генерации монет
    /// </summary>
    public int CoinChance;

    /// <summary>
    /// поле служит проверкой на наличие сверхспособностей
    /// </summary>
    bool powerUpSpawn;

    /// <summary>
    /// список сверхспособностей
    /// </summary>
    public List<GameObject> PowerUps;

    /// <summary>
    /// метод вызывается в старте игры
    /// </summary>
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        moveVec = new Vector3(-1, 0, 0);

        CoinObj.SetActive(Random.Range(0, 101) <= CoinChance);

        powerUpSpawn = Random.Range(0, 101) <= 10;
        if (powerUpSpawn)
            PowerUps[Random.Range(0, PowerUps.Count)].SetActive(true);
    }

    /// <summary>
    /// Передвигает дорогу, и создается илюзия движения персонажа вперед
    /// </summary>
    void Update()
    {
        if (GM.CanPlay)
            transform.Translate(moveVec * Time.deltaTime * GM.MoveSpeed);
    }
}
