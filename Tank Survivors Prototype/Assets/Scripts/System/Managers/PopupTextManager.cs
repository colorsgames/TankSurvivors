using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager Instance;

    [SerializeField] private DamageTextController damageText;

    private ObjectPool<DamageTextController> pool;

    private void Start()
    {
        Instance = this;
        //SpawnDamageText(new Vector3(-100, 0, 0), ColorType.playerDamage, "0");

        pool = new ObjectPool<DamageTextController>(CreateText, OnTakeText, OnReturnText);
    }

    public void SpawnDamageText(Vector3 pos, ColorType cType, string text)
    {
        DamageTextController dText = GetText();
        dText.transform.position = pos;
        dText.SetDamageText(text, cType);
    }

    DamageTextController CreateText()
    {
        DamageTextController dText = Instantiate(damageText);
        dText.SetPool(pool);
        return dText;
    }

    void OnTakeText(DamageTextController dText)
    {
        dText.HideObject(false);
    }

    void OnReturnText(DamageTextController dText)
    {
        dText.HideObject(true);
    }

    DamageTextController GetText()
    {
        return pool.Get();
    }
}
