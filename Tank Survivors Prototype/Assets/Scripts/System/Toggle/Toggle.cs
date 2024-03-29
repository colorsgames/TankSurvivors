using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toggle : MonoBehaviour
{
    [SerializeField] private RectTransform toggle;
    [SerializeField] private TMP_Text state_1, state_2;

    [SerializeField] private Color off, on;

    [SerializeField] protected int id;

    //int intState;

    protected bool state;

    public virtual void Start()
    {
        //PlayerPrefs.SetInt("State" + gameObject.name, 1);
        //intState = PlayerPrefs.GetInt("State" + gameObject.name);

        UpdateVis();
    }

    public virtual void Use()
    {
        if (state)
        {
            state = false;
            //PlayerPrefs.SetInt("State" + gameObject.name, 0);
            state_1.color = off;
            state_2.color = on;
        }
        else
        {
            state = true;
            //PlayerPrefs.SetInt("State" + gameObject.name, 1);
            state_1.color = on;
            state_2.color = off;
        }
        toggle.localPosition *= -1f;
    }

    void UpdateVis()
    {
        if (state)
        {
            state_1.color = on;
            state_2.color = off;
        }
        else
        {
            state_1.color = off;
            state_2.color = on;
            toggle.localPosition *= -1f;
        }
    }
}
