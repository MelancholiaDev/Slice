using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public float currentScore;
    public float fullScore;
    public TextMeshProUGUI finalScoreText;

    private void Start()
    {
        _scoreText = GameManager.Instance.FindInActiveObjectByName("Money Text").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fullScore += currentScore;
        currentScore = 0;
        _scoreText = GameManager.Instance.FindInActiveObjectByName("Money Text").GetComponent<TextMeshProUGUI>();
        _scoreText.text = "$ " + FullScore().ToString();
    }

    public void AddScore(float amount)
    {
        currentScore += amount;
        _scoreText.text = "$ " + FullScore().ToString();
    }

    public void SetFinalScoreText()
    {
        finalScoreText.text = "+ " + FullScore().ToString();
    }

    private float FullScore()
    {
        float n = fullScore + currentScore;
        return n;
    }
}
