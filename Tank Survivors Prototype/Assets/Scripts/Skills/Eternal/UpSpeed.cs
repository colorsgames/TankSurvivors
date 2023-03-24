using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class UpSpeed : Skills
    {
        public override void Start()
        {
            if (Progress.Instance.progressInfo.priceSkill5 == 0)
                Progress.Instance.progressInfo.priceSkill5 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill5;
            currentPrice = Progress.Instance.progressInfo.priceSkill5;
            currentValue = SkillsManager.Instance.Speed;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill5 = level;
            Progress.Instance.progressInfo.priceSkill5 = currentPrice;
            float res = (SkillsManager.Instance.Speed / 100) * percent;
            SkillsManager.Instance.UpSpeed(res);
            currentValue = SkillsManager.Instance.Speed;
        }
    }
}