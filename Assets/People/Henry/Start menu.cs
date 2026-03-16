using UnityEngine;
using UnityEngine.SceneManagement;

public class Startmenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitButton()
    {
        Debug.Log("Quit the game!");
        Application.Quit(); 
    } 
}
