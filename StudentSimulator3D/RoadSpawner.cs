using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    /// <summary>
    /// массив всевозможных блоков пути, инициализируется в инспекторе в среде Unity3D
    /// </summary>
    public GameObject[] RoadBlocksPrefabs;
    /// <summary>
    /// стартовый блок пути, инициализируется в инспекторе в среде Unity3D
    /// </summary>
    public GameObject StartBlock;
    public Transform PlayerTransf;

    float StartBlockXPos = 0;

    int BlocksCount = 15;
    float blockLength = 0;


    /// <summary>
    /// список текущих сгенерированных блоков пути
    /// </summary>
    List<GameObject> CurrentBlocks = new List<GameObject>();

    Vector3 StartPlayerPos;
    /// <summary>
    /// Метод вызывается при запуске игры один раз, устанавливает начальную позицию стартового блока дороги  
    /// </summary>
    void Start()
    {
        StartBlockXPos = PlayerTransf.position.x + 10;
        blockLength = 20;


        StartGame();

    }
    /// <summary>
    /// Старт игры
    /// </summary>
    public void StartGame()
    {



        foreach (var go in CurrentBlocks)//отчищаем список
        {
            Destroy(go);
        }
        CurrentBlocks.Clear();


        for (int i = 0; i < BlocksCount; i++)
        {
            SpawnBlock();
        }


    }


    /// <summary>
    /// Вызывает метод CheckForSpawn
    /// </summary>
    void LateUpdate()
    {
        CheckForSpawn();
    }
    /// <summary>
    /// Генерирует уровень
    /// </summary>
    void CheckForSpawn()
    {
        if (CurrentBlocks[0].transform.position.x - PlayerTransf.position.x < -25)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }
    /// <summary>
    /// Генерирует случайный блок, добавляет его в список 
    /// </summary>
    void SpawnBlock()
    {
        GameObject block = Instantiate(RoadBlocksPrefabs[Random.Range(0, RoadBlocksPrefabs.Length)], transform);
        Vector3 blockPos;
        if (CurrentBlocks.Count > 0)
            blockPos = CurrentBlocks[CurrentBlocks.Count - 1].transform.position + new Vector3(blockLength, 0, 0);
        else
            blockPos = new Vector3(StartBlockXPos, 0, 0);


        block.transform.position = blockPos;

        CurrentBlocks.Add(block);
    }
    /// <summary>
    /// Уничтожает блок, удаляет его из списка
    /// </summary>
    void DestroyBlock()
    {
        Destroy(CurrentBlocks[0]);
        CurrentBlocks.RemoveAt(0);
    }
}
