using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] GameEvents events;
    public int highScore { get; private set; }
    public int currentScore { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentScore = 0;
        highScore = PlayerPrefs.GetInt(nameof(highScore));
        UIManager.Instance.UpdateHiScore(highScore);
    }

    private void OnEnable()
    {
        events.PlayerAtGate += UpdateScores;
        events.PlayerNoEntry += GameOver;
    }
    private void OnDisable()
    {
        events.PlayerAtGate -= UpdateScores;
        events.PlayerNoEntry -= GameOver;
    }

    void UpdateScores()
    {
        currentScore++;
        UIManager.Instance.UpdateScore(currentScore);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.Instance.UpdateHiScore(highScore);
        }
    }

    void ResetScore()
    {
        currentScore = 0;
        UIManager.Instance.UpdateScore(currentScore);
    }

    void SaveScores() => PlayerPrefs.SetInt(nameof(highScore), highScore);

    public void GameOver()
    {
        SaveScores();
        UIManager.Instance.GameOverUI();
    }

    public void RestartGame()
    {
        ResetScore();
        events.RaiseGameRestart();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() => Time.timeScale = 1;

    public void Quit()
    {
        SaveScores();
        Application.Quit();
    }
}
