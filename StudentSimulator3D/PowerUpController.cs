using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    /// <summary>
    /// Сверхспособность имеет два типа и длительность
    /// </summary>
    public struct PowerUp//сверхспособности
    {
        public enum Type
        {
            MUILTIPLIER,//умножение очков
            IMMORTALITY,//бессмертие

        }
        public Type PowerUpType;
        public float Duration;
    }



    PowerUp[] powerUps = new PowerUp[2];
    Coroutine[] powerUpsCors = new Coroutine[2];
    /// <summary>
    ///  Объект иницализируется в среде Unity3D, управляет логикой игры
    /// </summary>
    public GameManager GM;
    /// <summary>
    /// Объект персонажа 
    /// </summary>
    public PlayerMovement PM;



    /// <summary>
    /// Метод инициализирует элементы массива сверхспособностей
    /// </summary>
    void Start()
    {
        powerUps[0] = new PowerUp() { PowerUpType = PowerUp.Type.MUILTIPLIER, Duration = 8 };
        powerUps[1] = new PowerUp() { PowerUpType = PowerUp.Type.IMMORTALITY, Duration = 5 };



    }

    /// <summary>
    /// Метод реализует использование сверхспособности любого типа
    /// </summary>
    /// <param name="type"></param>
    public void PowerUpUse(PowerUp.Type type)
    {
        PowerUpReset(type);
        powerUpsCors[(int)type] = StartCoroutine(PowerUpCor(type));

        switch (type)
        {
            case PowerUp.Type.MUILTIPLIER:
                GM.PowerUpMultiplier = 2;
                break;
            case PowerUp.Type.IMMORTALITY:
                PM.ImmortalityOn();
                break;

        }
    }
    /// <summary>
    /// Метод деактивирует сверхспособность любого типа
    /// </summary>
    /// <param name="type"></param>
    void PowerUpReset(PowerUp.Type type)
    {
        if (powerUpsCors[(int)type] != null)
            StopCoroutine(powerUpsCors[(int)type]);
        else
            return;

        powerUpsCors[(int)type] = null;

        switch (type)
        {
            case PowerUp.Type.MUILTIPLIER:
                GM.PowerUpMultiplier = 1;
                break;
            case PowerUp.Type.IMMORTALITY:
                PM.ImmortalityOff();
                break;


        }
    }
    /// <summary>
    /// Деактивирует все сверхспособности
    /// </summary>
    public void ResetAllPowerUps()
    {
        for (int i = 0; i < powerUps.Length; i++)
            PowerUpReset(powerUps[i].PowerUpType);


    }
    /// <summary>
    /// Реализует сверхспособность
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IEnumerator PowerUpCor(PowerUp.Type type)
    {
        float duration = powerUps[(int)type].Duration;


        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }



        PowerUpReset(type);
    }



}
