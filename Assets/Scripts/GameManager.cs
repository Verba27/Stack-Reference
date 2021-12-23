using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    private int score;
    
    void Start()
    {
        Time.timeScale = 0;
        spawner.onInstance += UpdateScore;
        spawner.onGameOver += EndGame;
    }
    public void StartGame()
    {
        score = 0;
        uiManager.StartGame();
        spawner.IsOnPlay(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpdateScore(bool oninstance)
    {
        if (true)
        {
            score++;
            uiManager.UpdateScoreUI(score);
        }
        
        audioManager.PlaySound(0);
    }

    public void EndGame(bool ongameover)
    {
        if (score > PlayerPrefs.GetInt("RECORD_SCORE"))
        {
            PlayerPrefs.SetInt("RECORD_SCORE", score);
        }
        spawner.IsOnPlay(false);
        uiManager.EndGame(score);
        audioManager.PlaySound(1);
        Time.timeScale = 0;
        Debug.Log("game over");
    }
    
}
