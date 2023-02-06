using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalSkills
{
    public class UpSpeed : Skills
    {
        public override void Start()
        {
            currentValue = SkillsManager.Instance.Speed;
            base.Start();
        }

        public override void Use()
        {
            float res = (SkillsManager.Instance.Speed / 100) * percent;
            SkillsManager.Instance.UpSpeed(res);
            currentValue = SkillsManager.Instance.Speed;
            base.Use();
        }
    }
}