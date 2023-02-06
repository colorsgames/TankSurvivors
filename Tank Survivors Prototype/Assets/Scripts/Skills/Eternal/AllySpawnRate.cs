using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class AllySpawnRate : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Start();
        }

        public override void Use()
        {
            float res = (SkillsManager.Instance.AllySpawnRate / 100) * percent;
            SkillsManager.Instance.DecreaseAllySpawnRate(res);
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Use();
        }
    }
}
