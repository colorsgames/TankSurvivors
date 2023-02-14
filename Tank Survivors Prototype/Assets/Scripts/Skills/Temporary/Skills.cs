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
        [SerializeField] private TMP_Text nameTMP;
        [SerializeField] private TMP_Text descriptionTMP;

        [SerializeField] private string ruLvl = "Ур.: ", engLvl = "Lvl: ";
        [SerializeField] private string ruNew = "Новое", engNew = "New";
        [SerializeField] private string ruName, engName;
        [SerializeField] private string ruDescription, engDescription;

        [SerializeField] protected SkillLevelData skillLevelData;

        [SerializeField] protected float percent;

        private void Start()
        {
            nameTMP.text = LanguageManager.isEng ? engName : ruName;
            descriptionTMP.text = LanguageManager.isEng ? engDescription : ruDescription;
            if (skillLevelData.level == 0)
            {
                levelTMP.text = LanguageManager.isEng ? engNew : ruNew;
            }
            else
            {
                levelTMP.text = LanguageManager.isEng ? engLvl : ruLvl + skillLevelData.level;
            }
        }

        public virtual void Use()
        {
            skillLevelData.level++;
            ExperienceManager.Instance.ClosePanel();
        }
    }
}