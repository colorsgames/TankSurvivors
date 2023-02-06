using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporarySkills
{
    public class AllyCount : Skills
    {
        [SerializeField] private SpawnAliveEntityData allyData;

        public override void Use()
        {
            allyData.maxCount++;
            if(allyData.maxCount == skillLevelData.endValue)
            {
                MaxLevel = true;
            }
            base.Use();
        }
    }
}
