using JetBrains.Annotations;
using UnityEngine;

public class WinLoseidk : MonoBehaviour
{
    public GameObject button;
    public bool winLose = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }



    // Update is called once per frame
    void Update()
    {
        

        if (winLose == true)
        {
            button.SetActive(true);
            //button.GetComponent<UnityEngine.UI.Button>().enabled = true;
        }
        
        if (winLose == false)
        {
            button.SetActive(false);
            //button.GetComponent<UnityEngine.UI.Button>().enabled = false;
        }

    }
}
