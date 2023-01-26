using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] GameModeData gameMode;

    [SerializeField] private TMP_Text minutesTMP;
    [SerializeField] private TMP_Text secundsTMP;

    int minutes;
    int seconds;
    int maxMinutes;

    private void Start()
    {
        StartCoroutine(Counter());
        maxMinutes = gameMode.maxMunites;
    }

    IEnumerator Counter()
    {
        while (true)
        {
            if (minutes < 10)
                minutesTMP.text = $"0{minutes.ToString()}";
            else
                minutesTMP.text = minutes.ToString();
            if (seconds < 10)
                secundsTMP.text = $"0{seconds.ToString()}";
            else
                secundsTMP.text = seconds.ToString();
            yield return new WaitForSeconds(1);
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            if(minutes == maxMinutes)
            {
                GameManager.instance.OpenResults(true);
            }
        }
    }
}
