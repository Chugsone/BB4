using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu1 : MonoBehaviour
{
    public Button[] buttons;
    private int unlockedLevel;

    private void Awake()
    {
        int unlockedLevels = PlayerPrefs.GetInt("unlockedLevels", 1);
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }




    public void OpenLevel(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelId);
    }

}
