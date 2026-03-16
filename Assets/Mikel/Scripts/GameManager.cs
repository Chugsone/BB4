using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text incomeText;
    public Button manualClick;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int updatesPerSecond = 5;

    const string SaveKey = "GM_Count";

    [HideInInspector] public float count = 0;
    float nextIdleTime = 1;
    float lastIncomeValue = 0;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (nextIdleTime < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextIdleTime = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
        }
    }

    void IdleCalculate()
    {
        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateMoneyPerSecond();
            storeUpgrade.UpdateUI();
        }
        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();

        Save();
    }

    public void ClickAction()
    {
        count++;
        count += lastIncomeValue * 0.02f;
        UpdateUI();
        Save();
    }

    public bool PurchaseAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            UpdateUI();
            Save();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        if (countText != null) countText.text = Mathf.RoundToInt(count).ToString();
        if (incomeText != null) incomeText.text = lastIncomeValue.ToString();
    }

    void Save()
    {
        PlayerPrefs.SetFloat(SaveKey, count);
        PlayerPrefs.Save();
    }

    void Load()
    {
        count = PlayerPrefs.GetFloat(SaveKey, 0f);
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void OnApplicationPause(bool paused)
    {
        if (paused) Save();
    }

    void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (countText == null)
        {
            var ctGO = GameObject.Find("CountText");
            if (ctGO != null) countText = ctGO.GetComponent<TMP_Text>();
        }

        if (incomeText == null)
        {
            var itGO = GameObject.Find("IncomeText");
            if (itGO != null) incomeText = itGO.GetComponent<TMP_Text>();
        }
        UpdateUI();
    }
}
