using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class Platform : MonoBehaviour
{
    public static Platform Instance;

    public static bool IsMobile;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlatform(string str)
    {
        if (str == "0")
            IsMobile = false;
        else IsMobile = true;
    }
}
