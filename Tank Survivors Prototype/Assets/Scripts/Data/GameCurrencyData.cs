using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Game Currency Data", menuName = "Game Currency Data", order = 65)]
public class GameCurrencyData : MonoBehaviour
{
    public static int TotalMoney { get { return Progress.Instance.progressInfo.totalMoney; } }

    public static void IncreaseTotalMoney(int value)
    {
        Progress.Instance.progressInfo.totalMoney += value;
    }

    public static void DecreaseTotalMoney(int value)
    {
        Progress.Instance.progressInfo.totalMoney -= value;
    }

/*    public static void SaveMoney()
    {
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
        totalMoney = 0;
    }

    public static void LoadMoney()
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoney");
    }*/
}
