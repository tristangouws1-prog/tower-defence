using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Settings")]
    public int startingGold = 200;
    public int startingLives = 20;

    int gold;
    int lives;
    bool isGameOver;

    public int Gold => gold;
    public int Lives => lives;
    public bool IsGameOver => isGameOver;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        gold = startingGold;
        lives = startingLives;
        UIManager.Instance?.UpdateHUD();
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        gold -= amount;
        UIManager.Instance?.UpdateHUD();
        return true;
    }

    public void EarnGold(int amount)
    {
        gold += amount;
        UIManager.Instance?.UpdateHUD();
    }

    public void LoseLife(int amount = 1)
    {
        lives -= amount;
        UIManager.Instance?.UpdateHUD();
        if (lives <= 0) TriggerGameOver();
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        UIManager.Instance?.ShowGameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
