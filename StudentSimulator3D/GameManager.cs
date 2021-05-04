using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Игровой объект экрана поражения, инициализируется в редакторе в среде Unity3D 
    /// </summary>
    public GameObject ResultObj;

    /// <summary>
    /// Игровой объект персонажа, инициализируется в редакторе в среде Unity3D 
    /// </summary>
    public PlayerMovement PM;

    /// <summary>
    /// Игровой объект родительского блока всех блоков пути, инициализируется в редакторе в среде Unity3D
    /// </summary>
    public RoadSpawner RS;

    /// <summary>
    /// поле служит тришерром для проверки состояния персонажа
    /// </summary>
    public bool CanPlay = true;

    /// <summary>
    /// игровые объекты текста служат пользовательским интерфейсом
    /// </summary>
    public Text PointsTXT,
                CoinsTXT;
    /// <summary>
    /// Поле служит хранением количества набранных очков
    /// </summary>
    float Points;

    /// <summary>
    /// Поля служат для реализации игровых сверхспособностей 
    /// </summary>
    public float PointsBaseValue, PointsMultiplier, PowerUpMultiplier;

    /// <summary>
    /// Скорость перемещения персонажа
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// Переменная служит для хранения количества монет
    /// </summary>
    public int Coins = 0;

    /// <summary>
    /// Метод реализует начало игры. Также вызывается при рестарте.
    /// </summary>
    public void StartGame()
    {
        ResultObj.SetActive(false);
        RS.StartGame();

        CanPlay = true;
        PM.ac.SetTrigger("ReSpawn");
        Points = 0;
        MoveSpeed = 7;
        PointsMultiplier = 1;
        PowerUpMultiplier = 1;
    }

    /// <summary>
    /// Постепенно увеличивает скорость персонажа и скорость получения очков. Увеличивет сложность игры. 
    /// </summary>
    private void Update()
    {
        if (CanPlay)
        {
            // Постепенное увеличение скорости набора очков
            Points += PointsBaseValue * PointsMultiplier * PowerUpMultiplier * Time.deltaTime;
            PointsMultiplier += 0.05f * Time.deltaTime;
            PointsMultiplier = Mathf.Clamp(PointsMultiplier, 1, 10);

            //Постепенное увеличение скорости для увеличения сложности игры
            MoveSpeed += 0.1f * Time.deltaTime;
            MoveSpeed = Mathf.Clamp(MoveSpeed, 1, 20);
        }

        PointsTXT.text = ((int)Points).ToString();
    }

    /// <summary>
    /// Активирует экран поражения
    /// </summary>
    public void Smert()
    {

        ResultObj.SetActive(true);
    }
    /// <summary>
    /// Реализует добавления и сборку монет
    /// </summary>
    /// <param name="number"></param>
    public void AddCoins(int number)
    {
        Coins += number;
        CoinsTXT.text = Coins.ToString();
    }

}
