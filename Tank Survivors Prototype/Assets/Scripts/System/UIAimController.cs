using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAimController : MonoBehaviour
{
    public static UIAimController instance;

    Image image;

    private void Start()
    {
        instance = this;
        image = GetComponent<Image>();
        Cursor.visible = false;
    }

    public void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void AimDeactivate(bool value)
    {
        Cursor.visible = value;
        image.enabled = !value;
    }
}
