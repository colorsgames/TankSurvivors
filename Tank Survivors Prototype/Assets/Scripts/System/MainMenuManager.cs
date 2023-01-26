using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameModeData gameMode;

    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject gameModes;
    [SerializeField] private GameObject skills;

    public void OpesGameModes(bool value)
    {
        mainButtons.SetActive(!value);
        gameModes.SetActive(value);
    }

    public void OpenSkills(bool value)
    {
        mainButtons.SetActive(!value);
        skills.SetActive(value);
    }

    public void SetGameMinutes(int value)
    {
        gameMode.maxMunites = value;
        SceneManager.LoadScene(1);
    }

    public void SetProfitRatio(int value)
    {
        gameMode.profitRatio = value;
    }
}
