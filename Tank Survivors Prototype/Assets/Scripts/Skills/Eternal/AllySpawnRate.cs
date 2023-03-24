using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class AllySpawnRate : Skills
    {
        public override void Start()
        {
            if (Progress.Instance.progressInfo.priceSkill1 == 0)
                Progress.Instance.progressInfo.priceSkill1 = startPrice;
            level = Progress.Instance.progressInfo.levelSkill1;
            currentPrice = Progress.Instance.progressInfo.priceSkill1;
            currentValue = SkillsManager.Instance.AllySpawnRate;
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            Progress.Instance.progressInfo.levelSkill1 = level;
            Progress.Instance.progressInfo.priceSkill1 = currentPrice;
            float res = percent;
            SkillsManager.Instance.DecreaseAllySpawnRate(res);
            currentValue = SkillsManager.Instance.AllySpawnRate;
        }
    }
}
