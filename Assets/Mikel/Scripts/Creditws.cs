using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Creditws : MonoBehaviour
{
    public void creditbuttonMikel()
    {
        SceneManager.LoadScene("ChaseCredits");
    }

    public void creditbuttonTaylor()
    {
        SceneManager.LoadScene("TaylorCredit");
    }
    public void creditbuttonHenry()
    {
        SceneManager.LoadScene("HenryCredit");
    }
    public void creditbuttonRian()
    {
        SceneManager.LoadScene("RianCredit");
    }

}
