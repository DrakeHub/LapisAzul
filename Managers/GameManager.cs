using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    private int level = 0;

    public bool IsPaused { get; private set; } = false;
    private bool allCollected = false;

    [SerializeField]
    private List<GameObject> collectibleCount = new List<GameObject>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (collectibleCount.Count >= 3)
        {
            if (!allCollected)
            {
                UIManager.Instance.Collectibles();
                allCollected = true;
                //collectibleCount.Clear();
            }
        }
    }

    public void AddCollectible(GameObject collectibleCaught, int number)
    {
        if (!collectibleCount.Contains(collectibleCaught))
        {
            collectibleCount.Add(collectibleCaught);
            UIManager.Instance.ShowCollectible(number);
        }
    }

    public void LoadNextLevel()
    {
        CancelInvoke();
        StopAllCoroutines();
        collectibleCount.Clear();
        allCollected = false;
        Time.timeScale = 1f;
        if (level + 1 < SceneManager.sceneCountInBuildSettings)
        {       
            level++;
            StartCoroutine( LoadNextLevelAsync(level) );
            UIManager.Instance.ShowHUD(true);
        }
        else
        {
            print("End Game!!!");
        }
    }

    public void LoadMainMenu()
    {
        CancelInvoke();
        StopAllCoroutines();
        collectibleCount.Clear();
        allCollected = false;
        StartCoroutine(LoadNextLevelAsync(0));
        level = 0;
        Time.timeScale = 1f;
        IsPaused = false;
        UIManager.Instance.ShowHUD(false);
        UIManager.Instance.ShowPanelPause(false);
    }

    private IEnumerator LoadNextLevelAsync(int level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }
        print(asyncLoad.progress);
        yield return null;
    }

    public void PauseGame(bool pause)
    {
        if (level != 0)
        {
            IsPaused = pause;
            if (pause)
            {
                UIManager.Instance.ShowPanelPause(pause);
                Time.timeScale = 0f;
            }
            else
            {
                IsPaused = false;
                Time.timeScale = 1f;
                UIManager.Instance.ShowPanelPause(pause);
            }
        }
    }

    public void TriggerEndGame()
    {
        if(collectibleCount.Count >= 3)
        {
            StartCoroutine(EndGame());
        }
        else
        {
            UIManager.Instance.MissingCollectibles();
        }
    }
    private IEnumerator EndGame()
    {
        UIManager.Instance.ShowEndScreen(true);
        yield return new WaitForSeconds(10f);
        UIManager.Instance.ShowEndScreen(false);
        LoadMainMenu();
    }

}
