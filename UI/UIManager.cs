using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject panelHUD = null;

    [SerializeField]
    private GameObject panelPause = null;

    [SerializeField]
    private GameObject panelOptions = null;

    [SerializeField]
    private GameObject panelCredits = null;

    [SerializeField]
    private GameObject panelEnding = null;

    [SerializeField]
    private GameObject notAllCollected = null;

    [SerializeField]
    private GameObject allCollected = null;

    [Header("Collectibles")]
    [SerializeField]
    private GameObject collectible1 = null;
    [SerializeField]
    private GameObject collectible2 = null;
    [SerializeField]
    private GameObject collectible3 = null;

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
    public void ReturnMainMenu() //Button to Return to menu, not exit to menu
    {
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void ShowOptionsMenu() // Options Button
    {
        panelOptions.SetActive(true);
    }

    public void ShowCreditsMenu() // Credits Button
    {
        panelCredits.SetActive(true);
    }

    public void ShowPanelPause(bool value) // Pause Menu Button
    {
        panelPause.SetActive(value);
    }

    public void ShowHUD(bool value)
    {
        panelHUD.SetActive(value);
        collectible1.SetActive(false);
        collectible2.SetActive(false);
        collectible3.SetActive(false);
    }
    public void ShowEndScreen(bool value)
    {
        panelEnding.SetActive(value);
    }

    //Collectible Stuff Below
    public void Collectibles()
    {
        allCollected.SetActive(true);
        Invoke("CollectiblesWait", 3.5f);
    }
    private void CollectiblesWait()
    {
        allCollected.SetActive(false);
    }
    public void MissingCollectibles()
    {
        notAllCollected.SetActive(true);
        Invoke("MissingCollectiblesWait", 3.5f);
    }
    private void MissingCollectiblesWait()
    {
        notAllCollected.SetActive(false);
    }
    public void ShowCollectible(int number)
    {
        switch (number)
        {
            case 1:
                collectible1.SetActive(true);
                break;
            case 2:
                collectible2.SetActive(true);
                break;
            case 3:
                collectible3.SetActive(true);
                break;
        }
    }
}
