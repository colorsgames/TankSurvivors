using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAimController : MonoBehaviour
{
    public static UIAimController instance;

    private void Start()
    {
        instance = this;
        Cursor.visible = false;
    }

    public void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void DestroyAim()
    {
        Cursor.visible = true;
        Destroy(gameObject);
    }
}
