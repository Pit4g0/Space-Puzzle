using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI timeText;

    private bool activeWinPanel = false;
    public float time;
    private SoundsManager soundsManager;

    private void Start()
    {
        soundsManager = FindAnyObjectByType<SoundsManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverPanel != null && (winPanel == null || !activeWinPanel))
            {
                ShowGameOverPanel();
            }
        }

        if (time != 0 && timeText != null && !activeWinPanel)
        {
            time -= Time.deltaTime;
            timeText.text = "Time: " + Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                soundsManager.PlaySound(SoundsManager.Sounds.TimerStop);
                ShowGameOverPanel();
            }
        }        
    }

    private void ShowGameOverPanel()
    {
        timeText.text = "";
        gameOverPanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        //soundsManager.PlaySound(SoundsManager.Sounds.Finish);

        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(winPanel != null)
        {
            winPanel.SetActive(true);
            activeWinPanel = true;
        }
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("load");
    }

    public void loadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSelectedLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
