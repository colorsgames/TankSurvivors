using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TemporarySkills
{
    public class Skills : MonoBehaviour
    {
        public bool MaxLevel
        { 
            get { return skillLevelData.isMax; } 
            protected set { skillLevelData.isMax = value; }
        }

        [SerializeField] private TMP_Text levelTMP;

        [SerializeField] protected SkillLevelData skillLevelData;

        [SerializeField] protected float percent;

        private void Start()
        {
            if(skillLevelData.level == 0)
            {
                levelTMP.text = "New";
            }
            else
            {
                levelTMP.text = "Level: " + skillLevelData.level;
            }
        }

        public virtual void Use()
        {
            skillLevelData.level++;
            ExperienceManager.Instance.ClosePanel();
        }
    }
}