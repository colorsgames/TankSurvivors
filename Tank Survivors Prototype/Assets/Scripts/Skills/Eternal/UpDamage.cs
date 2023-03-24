using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class UpDamage : Skills
    {
        public override void Start()
        {
            if (Progress.Instance.progressInfo.priceSkill4 == 0)
                Progress.Instance.progressInfo.priceSkill4 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill4;
            currentPrice = Progress.Instance.progressInfo.priceSkill4;
            currentValue = SkillsManager.Instance.PlayerDamage;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill4 = level;
            Progress.Instance.progressInfo.priceSkill4 = currentPrice;
            float res = (SkillsManager.Instance.PlayerDamage / 100) * percent;
            SkillsManager.Instance.IncreaseDamage(res);
            currentValue = SkillsManager.Instance.PlayerDamage;
        }
    }
}
