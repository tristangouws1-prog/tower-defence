using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;

    [Header("Screens")]
    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateHUD()
    {
        if (goldText != null) goldText.text = $"Gold: {GameManager.Instance.Gold}";
        if (livesText != null) livesText.text = $"Lives: {GameManager.Instance.Lives}";
    }

    public void UpdateWave(int wave)
    {
        if (waveText != null) waveText.text = $"Wave {wave}";
    }

    public void ShowGameOver()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    public void ShowVictory()
    {
        if (victoryScreen != null) victoryScreen.SetActive(true);
    }
}
