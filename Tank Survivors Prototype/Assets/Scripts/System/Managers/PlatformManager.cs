using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    public bool IsMobile { get { return isMobile; } }

    [SerializeField] private GameObject mobileControllers;
    [SerializeField] private GameObject aim;

    [SerializeField] private bool isMobile;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (isMobile)
        {
            mobileControllers.SetActive(true);
            aim.SetActive(false);
        }
    }
}
