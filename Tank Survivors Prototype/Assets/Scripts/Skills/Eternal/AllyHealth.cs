using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class AllyHealth : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.AllyHeath;
            base.Start();
        }

        public override void Use()
        {
            float res = (SkillsManager.Instance.AllyHeath / 100) * percent;
            SkillsManager.Instance.IncreaseAllyHealth(res);
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Use();
        }
    }
}