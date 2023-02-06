using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill Level Data", menuName = "Skill Level Data", order = 65)]
public class SkillLevelData : ScriptableObject
{
    public int level;
    public float endValue;

    public bool isMax;
    public bool startOneLvl;

    public void ResetValue()
    {
        level = 0;
        isMax = false;
        if(startOneLvl)
            level = 1;
    }
}
