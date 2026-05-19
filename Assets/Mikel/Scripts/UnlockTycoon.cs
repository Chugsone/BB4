using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockTycoon : MonoBehaviour
{
    [SerializeField] private int level;

    public void OnButtonPress()
    {
        Debug.Log("Button Pressed");
        UnlockNewLevel();
        SceneManager.LoadScene(level);
    }


    void UnlockNewLevel()
    {
        PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("UnlockedLevels", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
        PlayerPrefs.Save();
    }


}
