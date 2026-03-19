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
        SceneManager.LoadScene("Main");
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

    IEnumerator Scroll()
    {
        foreach(Sprite spr in backgrounds)
        {
            currentImage.sprite = spr;
            yield return new WaitForSeconds(swapTime);
        }
        StartCoroutine(Scroll());
    }
}
