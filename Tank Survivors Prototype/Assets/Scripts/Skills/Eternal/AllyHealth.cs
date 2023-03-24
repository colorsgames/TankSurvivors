using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class AllyHealth : Skills
    {
        public override void Start()
        {
            if(Progress.Instance.progressInfo.priceSkill0 == 0)
                Progress.Instance.progressInfo.priceSkill0 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill0;
            currentPrice = Progress.Instance.progressInfo.priceSkill0;
            currentValue = SkillsManager.Instance.AllyHeath;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill0 = level;
            Progress.Instance.progressInfo.priceSkill0 = currentPrice;
            float res = (SkillsManager.Instance.AllyHeath / 100) * percent;
            SkillsManager.Instance.IncreaseAllyHealth(res);
            currentValue = SkillsManager.Instance.AllyHeath;
        }
    }
}