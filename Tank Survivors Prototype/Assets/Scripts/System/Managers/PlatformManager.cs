using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    public bool IsMobile { get { return Platform.IsMobile; } }

    [SerializeField] private GameObject mobileControllers;
    [SerializeField] private GameObject aim;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (Platform.IsMobile)
        {
            mobileControllers.SetActive(true);
            aim.SetActive(false);
        }
        else
        {
            mobileControllers.SetActive(false);
        }
    }
}
