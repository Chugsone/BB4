using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class DifferentScenes : MonoBehaviour
{

    private enum TextType
    {
        Count = 0,
        Income = 1,
        Button = 2,

    }
    private GameManager gameManager;
    [SerializeField] private TextType textType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.instance;
        if (textType == TextType.Count && gameManager.countText == null)
        {

            gameManager.countText = gameObject.GetComponent <TMP_Text>();
        }
        
       if (textType == TextType.Income && gameManager.incomeText == null)
        {
            gameManager.incomeText = gameObject.GetComponent <TMP_Text>();
        }
        if (textType == TextType.Button)
        {
            Button button = gameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
           // button.onClick.AddListener(gameManager.manualClick.onClick.GetPersistentListeners(0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
