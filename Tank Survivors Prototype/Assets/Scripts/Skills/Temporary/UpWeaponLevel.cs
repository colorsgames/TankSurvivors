using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class UpWeaponLevel : Skills
    {
        public override void Use()
        {
            base.Use();
            Player.Instance.NextWeapon();
            if(skillLevelData.level >= skillLevelData.endValue)
            {
                MaxLevel = true;
            }
        }
    }
}