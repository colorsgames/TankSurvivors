using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class DecreaseDelay : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.ShotDelay;
            base.Start();
        }

        public override void Use()
        {
            float res = percent;
            SkillsManager.Instance.DecreaseDelay(res);
            currentValue = SkillsManager.Instance.ShotDelay;
            base.Use();
        }
    }
}