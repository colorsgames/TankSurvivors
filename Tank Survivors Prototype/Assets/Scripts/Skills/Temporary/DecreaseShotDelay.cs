using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class DecreaseShotDelay : Skills
    {
        [SerializeField] private WeaponData playerData;

        public override void Use()
        {
            float res = percent;
            playerData.delay -= res;
            if (playerData.delay <= skillLevelData.endValue)
            {
                playerData.delay = skillLevelData.endValue;
                MaxLevel = true;
            }
            base.Use();
        }
    }
}