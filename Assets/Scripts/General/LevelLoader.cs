using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [Tooltip("How many scenes are before the main gameplay scenes")]
    private int currentLevel;

    const string level_name_convention = "Level_";
    public int currentUnlockedLevel { get; private set; }


    public static string PLAYER_PREFS_UNLOCKED_LEVELS_KEY = "levelUnlocked";

    [SerializeField] GameObject loadingPnl;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] Image progressBar;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
        }

    }

    private void OnEnable()
    {
        currentUnlockedLevel = PlayerPrefs.GetInt(PLAYER_PREFS_UNLOCKED_LEVELS_KEY, 1);

    }

    public void LoadNextLevel()
    {


        currentLevel = GetCurrentLevelNumber();

        string nextLevelName = $"{level_name_convention}{currentLevel + 1}";
        if (SceneManager.GetSceneByName(nextLevelName) != null)
        {
            PlayerPrefs.SetInt(PLAYER_PREFS_UNLOCKED_LEVELS_KEY, currentLevel + 1);
            StartCoroutine(LoadSceneAsync(nextLevelName));
        }

    }
    public void ReloadLevel()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }


    public void LoadReachedLevel()
    {
        StartCoroutine(LoadSceneAsync(level_name_convention + currentUnlockedLevel));
    }
    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(LoadSceneAsync(levelIndex));
    }
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }


    public void LoadLevelWithNumber(int number)
    {
        if (SceneManager.GetSceneByName(level_name_convention + number) != null)
            StartCoroutine(LoadSceneAsync(level_name_convention + number));
    }
    public int GetCurrentLevelNumber()
    {
        string currentLevel = SceneManager.GetActiveScene().name.Substring(SceneManager.GetActiveScene().name.IndexOf("_") + 1);
        return int.Parse(currentLevel);
    }


    IEnumerator LoadSceneAsync(string levelToLoad)
    {
        loadingPnl.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);
        print("LEVEL LOADER TEXT : COLOR : "+ progressText.color);
        print("LEVEL LOADER TEXT : font asset : "+progressText.fontMaterial);
        print("LEVEL LOADER TEXT : enabled : "+progressText.isActiveAndEnabled);
        print("LEVEL LOADER TEXT : rtl : "+ progressText.isRightToLeftText);


        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.fillAmount = progress;
            progressText.text = "Loading: " + (progress * 100f).ToString("F0") + "%";

            yield return null;
        }
        loadingPnl.SetActive(false);
    }

    IEnumerator LoadSceneAsync(int levelToLoad)
    {
        loadingPnl.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.fillAmount = progress;
            progressText.text = "Loading: " + (progress * 100f).ToString("F0") + "%";

            yield return null;
        }
        loadingPnl.SetActive(false);

    }
}
