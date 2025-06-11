using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI highScore, currentScore;
    [SerializeField] TextMeshProUGUI hiScorePG, currentScorePG;
    [SerializeField] GameObject PostGameScreen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateScore(int score) => currentScore.text = score.ToString();
    public void UpdateHiScore(int hiScore) => highScore.text = hiScore.ToString();
    public void GameOverUI()
    {
        hiScorePG.text = highScore.text;
        currentScorePG.text = currentScore.text;
        PostGameScreen.SetActive(true);
    }
}
