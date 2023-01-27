using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Game Currency Data", menuName = "Game Currency Data", order = 65)]
public class GameCurrencyData : MonoBehaviour
{
    public static int TotalMoney { get { return totalMoney; } }

    private static int totalMoney;

    public static void IncreaseTotalMoney(int value)
    {
        totalMoney += value;
    }

    public static void DecreaseTotalMoney(int value)
    {
        totalMoney -= value;
    }
}
