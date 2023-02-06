using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class UpHealth : Skills
    {
        [SerializeField] private AliveEntityData playerData;

        public override void Use()
        {
            float res = (playerData.maxHealth / 100) * percent;
            playerData.maxHealth += res;
            Player.Instance.Healing(res);
            if (playerData.maxHealth >= skillLevelData.endValue)
            {
                playerData.maxHealth = skillLevelData.endValue;
                MaxLevel = true;
            }
            base.Use();
        }
    }
}