using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class PlayerHealth : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.PlayerHealth;
            base.Start();
        }

        public override void Use()
        {
            float res = (SkillsManager.Instance.PlayerHealth / 100) * percent;
            SkillsManager.Instance.IncreaseHealth(res);
            currentValue = SkillsManager.Instance.PlayerHealth;
            base.Use();
        }
    }
}