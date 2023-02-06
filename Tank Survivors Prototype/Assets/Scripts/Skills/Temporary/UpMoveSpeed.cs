using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class UpMoveSpeed : Skills
    {
        [SerializeField] private AliveEntityData playerData;

        public override void Use()
        {
            float res = (playerData.moveSpeed / 100) * percent;
            playerData.moveSpeed += res;
            if (playerData.moveSpeed >= skillLevelData.endValue)
            {
                playerData.moveSpeed = skillLevelData.endValue;
                MaxLevel = true;
            }
            base.Use();
        }
    }
}