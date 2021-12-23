using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private Canvas playScoreCanvas;
    [SerializeField] private TextMeshProUGUI scoreInGame;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI recordScore;

    public void StartGame()
    {
        playScoreCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        endGameCanvas.enabled = false;
    }

    public void EndGame(int score)
    {
        playScoreCanvas.enabled = false;
        endGameCanvas.enabled = true;
        finalScore.text = score.ToString();
        recordScore.text = PlayerPrefs.GetInt("RECORD_SCORE").ToString();
    }

    public void UpdateScoreUI(int score)
    {
        scoreInGame.text = score.ToString();
    }
}
