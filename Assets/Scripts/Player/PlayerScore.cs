using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public GameObject digitScorePanel;

    public static int score = 0;

    static TextMeshProUGUI[] textDigits;

    public static event Action<int> onAddScore;
    public static void OnAddScore(int scoreToAdd) { onAddScore?.Invoke(scoreToAdd); }

    void Awake() => onAddScore += (int value) => AddScore(value);

    void Start() => textDigits = digitScorePanel.GetComponentsInChildren<TextMeshProUGUI>();

    void AddScore(int increaseAmount)
    {
        score += increaseAmount;

        if (score > 999)
            score = 999;

        UpdateScore();
    }

    void UpdateScore()
    {
        int maxScaleSize = 3;
        float timeScale = .1f;

        for (int i = 0; i < score.ToString().Length; i++)
        {
            int digit = (score / (int)Mathf.Pow(10, i)) % 10;
            int digitIndex = (textDigits.Length - 1) - i;
            if (digit == int.Parse(textDigits[digitIndex].text))
                continue;

            textDigits[digitIndex].text = digit.ToString();
            textDigits[digitIndex].transform.localScale = Vector3.one * maxScaleSize;
            textDigits[digitIndex].transform.DOScale(Vector3.one, timeScale);
        }
    }
}
