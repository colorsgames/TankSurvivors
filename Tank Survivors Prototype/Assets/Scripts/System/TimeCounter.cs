using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter Instance;

    [SerializeField] GameModeData gameMode;

    [SerializeField] private TMP_Text minutesTMP;
    [SerializeField] private TMP_Text secundsTMP;

    public string Minutes { get { return minutesTMP.text; } }
    public string Seconds { get { return secundsTMP.text; } }

    int minutes;
    int seconds;
    int maxMinutes;

    private void Awake()
    {
        Instance = this;
    }

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
/*            if(minutes == maxMinutes - 1 & seconds == 0)
            {
                GameManager.decreaseSpawnRate.Invoke();
            }
            if(minutes == maxMinutes)
            {
                GameManager.instance.OpenResults(true);
            }*/
        }
    }
}
