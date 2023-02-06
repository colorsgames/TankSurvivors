using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class UpDamage : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Start();
        }

        public override void Use()
        {
            float res = (SkillsManager.Instance.PlayerDamage / 100) * percent;
            SkillsManager.Instance.IncreaseDamage(res);
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Use();
        }
    }
}
