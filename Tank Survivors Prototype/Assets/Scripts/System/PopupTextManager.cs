using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager instance;

    [SerializeField] private DamageTextController damageText;

    private void Start()
    {
        instance = this;
        SpawnDamageText(new Vector3(-100, 0, 0), ColorType.playerDamage, "0");
    }

    public void SpawnDamageText(Vector3 pos, ColorType cType, string text)
    {
        DamageTextController dText = Instantiate(damageText, pos, Quaternion.identity);
        dText.SetDamageText(text, cType);
    }
}
