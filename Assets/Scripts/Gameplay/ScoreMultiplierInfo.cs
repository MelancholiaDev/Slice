using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMultiplierInfo : MonoBehaviour
{
    [SerializeField] private float multiplier;

    public void MultiplyScore()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (multiplier != 0)
        {
            scoreManager.AddScore((scoreManager.currentScore * multiplier) - scoreManager.currentScore);
        }

        scoreManager.finalScoreText = GameManager.Instance.FindInActiveObjectByName("Final Score Text").GetComponent<TextMeshProUGUI>();
        scoreManager.SetFinalScoreText();

        GameManager.Instance.ShowWinScreen();
    }

}
