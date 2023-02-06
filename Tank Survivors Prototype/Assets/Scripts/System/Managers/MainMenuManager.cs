using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameModeData gameMode;

    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject gameModes;
    [SerializeField] private GameObject skills;

    private void Start()
    {
        OpenSkills(false);
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
        UpgradeManager.onButtonDown.Invoke();
    }

    public void SetGameMinutes(int value)
    {
        gameMode.maxMunites = value;
        StartGame();
    }

    public void StartGame()
    {
        SkillsManager.Instance.SetSkills();
        SceneManager.LoadScene(1);
    }

    public void SetProfitRatio(int value)
    {
        gameMode.profitRatio = value;
    }
}
