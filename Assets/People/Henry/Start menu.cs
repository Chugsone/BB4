using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Startmenu : MonoBehaviour
{
    [SerializeField] private float swapTime = 5f;
    [SerializeField] private Sprite[] backgrounds;

    [SerializeField] private Image currentImage;
    public void StartButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitButton()
    {
        Debug.Log("Quit the game!");
        Application.Quit(); 
    }

    private void Start()
    {
        StartCoroutine(Scroll());
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    IEnumerator Scroll()
    {
        foreach(Sprite spr in backgrounds)
        {
            currentImage.sprite = spr;
            for (int i = 0; i < 20; i++)
            {
                currentImage.color = new Color(1f, 1f, 1f, 0f + (i / 20f));
                yield return new WaitForSeconds(.05f);

            }
            currentImage.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(swapTime);
            for (int i = 0; i < 20; i++)
            {
                currentImage.color = new Color(1f, 1f, 1f, 1f - (i / 20f));
                yield return new WaitForSeconds(.05f);
                
            }
            yield return new WaitForSeconds(.1f);


        }
        StartCoroutine(Scroll());
    }
}
