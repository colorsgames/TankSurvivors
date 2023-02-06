using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class UpDamage : Skills
    {
        [SerializeField] private WeaponData playerData;

        public override void Use()
        {
            float res = (playerData.minDamage / 100) * percent;
            playerData.minDamage += res;
            if (playerData.minDamage >= skillLevelData.endValue)
            {
                playerData.minDamage = skillLevelData.endValue;
                MaxLevel = true;
            }
            base.Use();
        }
    }
}