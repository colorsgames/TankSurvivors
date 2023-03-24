using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static bool isMenu;

    public static MainMenuManager Instance;

    [SerializeField] private GameModeData gameMode;

    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject gameModes;
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject settings;

    int startSkillsCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        OpenSettings(false);
        isMenu = true;
    }

    public void IStart()
    {
        startSkillsCount++;
        if(startSkillsCount >= 6)
        {
            OpenSkills(false);
            ADManager.Instance.ShowAD();
            startSkillsCount = 0;
        }
    }

    public void OpesGameModes(bool value)
    {
        mainButtons.SetActive(!value);
        gameModes.SetActive(value);
    }

    public void OpenSkills(bool value)
    {
        mainButtons.SetActive(!value);
        skills.SetActive(value);
        UpgradeManager.Instance.Clear();
        UpgradeManager.onButtonDown.Invoke();
    }

    public void OpenSettings(bool value)
    {
        mainButtons.SetActive(!value);
        settings.SetActive(value);
    }

    public void SetGameMinutes(int value)
    {
        gameMode.maxMunites = value;
        StartGame();
    }

    public void StartGame()
    {
        SkillsManager.Instance.SetSkills();
        isMenu = false;
        SceneManager.LoadScene(1);
    }

    public void SetProfitRatio(int value)
    {
        gameMode.profitRatio = value;
    }
}
