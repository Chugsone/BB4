using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TycoonHopping : MonoBehaviour
{
    public void TycoonHop()
    {
        SceneManager.LoadScene("Tycoon");
    }

    public void TycoonHop2()
    {
        SceneManager.LoadScene("Tycoon2");
    }

    public void MapHop()
    {
        SceneManager.LoadScene("Map");
    }

}
