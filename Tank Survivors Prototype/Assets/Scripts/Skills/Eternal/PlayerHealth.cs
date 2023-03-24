using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class PlayerHealth : Skills
    {
        public override void Start()
        {
            if (Progress.Instance.progressInfo.priceSkill3 == 0)
                Progress.Instance.progressInfo.priceSkill3 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill3;
            currentPrice = Progress.Instance.progressInfo.priceSkill3;
            currentValue = SkillsManager.Instance.PlayerHealth;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill3 = level;
            Progress.Instance.progressInfo.priceSkill3 = currentPrice;
            float res = (SkillsManager.Instance.PlayerHealth / 100) * percent;
            SkillsManager.Instance.IncreaseHealth(res);
            currentValue = SkillsManager.Instance.PlayerHealth;
        }
    }
}