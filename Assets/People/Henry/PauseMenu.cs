using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pausemenu : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject pauseMenu;
    public void Pause(InputAction.CallbackContext ctx)
    {
            
        isPaused = true;
        Pausemenu.SetActive(true);

        Time.timeScale = 0;
    }

    private static void SetActive(bool v)
    {
        throw new NotImplementedException();
    }

    public void Resume(InputAction.CallbackContext ctx)
    {
        isPaused = false; 
        Pausemenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void Toggle(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;
        Pausemenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
    }
        


        public void Quit()
    {

    }


}
