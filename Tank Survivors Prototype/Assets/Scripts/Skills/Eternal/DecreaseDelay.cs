using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class DecreaseDelay : Skills
    {
        public override void Start()
        {
            if (Progress.Instance.progressInfo.priceSkill2 == 0)
                Progress.Instance.progressInfo.priceSkill2 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill2;
            currentPrice = Progress.Instance.progressInfo.priceSkill2;
            currentValue = SkillsManager.Instance.ShotDelay;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill2 = level;
            Progress.Instance.progressInfo.priceSkill2 = currentPrice;
            float res = percent;
            SkillsManager.Instance.DecreaseDelay(res);
            currentValue = SkillsManager.Instance.ShotDelay;
        }
    }
}