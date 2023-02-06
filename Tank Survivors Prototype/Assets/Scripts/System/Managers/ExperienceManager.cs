using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TemporarySkills;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text lvlTMP;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private List<Skills> skills = new List<Skills>();
    [SerializeField] private Transform content;

    [SerializeField] private float startMaxExp;
    [SerializeField] private float inscreaseMaxExpCoefficient;
    [SerializeField] private float delay = 0.5f;

    List<Skills> currentSkills = new List<Skills>();

    int lvl;
    float currentExp;
    float currentMaxExp;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentMaxExp = startMaxExp;
        SetParticle(false);
        UpdateExpBar(currentExp);
        lvlTMP.text = "LVL: " + lvl;
    }

    public void AddExp(float value)
    {
        currentExp += value;
        UpdateExpBar(currentExp);
        if (currentExp >= currentMaxExp)
        {
            lvl++;
            lvlTMP.text = "LVL: " + lvl;
            StartCoroutine(OpenLVLPanel());
        }
    }

    IEnumerator OpenLVLPanel()
    {
        GameManager.Instance.StopTime(true);

        SetParticle(true);

        yield return new WaitForSecondsRealtime(delay);

        GameManager.Instance.OpenNewLVLPanel(true);

        SpawnSkills();
    }

    public void ClosePanel()
    {
        currentMaxExp *= inscreaseMaxExpCoefficient;
        currentExp = 0;
        UpdateExpBar(currentExp);
        SetParticle(false);
        foreach (var item in currentSkills)
        {
            Destroy(item.gameObject);
        }
        currentSkills.Clear();
        GameManager.Instance.OpenNewLVLPanel(false);
        GameManager.Instance.StopTime(false);
    }

    void SpawnSkills()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].MaxLevel)
            {
                skills.Remove(skills[i]);
            }
        }
        bool[] busyId  = new bool[skills.Count];

        if (skills.Count > 3)
        {
            for (int i = 0; i < 3;)
            {
                int randId = Random.Range(0, skills.Count);
                if (!busyId[randId])
                {
                    Skills newSkill = Instantiate(skills[randId], content);
                    currentSkills.Add(newSkill);
                    busyId[randId] = true;
                    i++;
                }
            }
        }
        else
        {
            foreach (var item in skills)
            {
                Skills newSkill = Instantiate(item, content);
                currentSkills.Add(newSkill);
            }
        }
    }

    void SetParticle(bool value)
    {
        var emiss = particle.emission;
        emiss.enabled = value;
    }

    void UpdateExpBar(float value)
    {
        expBar.fillAmount = value / currentMaxExp;
    }
}
