using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextbox = default;

    private int _previousScore;

    private void Start()
    {
        _previousScore = 0;
    }

    private void Update()
    {
        int currentScore = ScoreBoard.Instance.Score;

        if(_previousScore != currentScore)
        {
            _scoreTextbox.text = $"SCORE: {currentScore}";
            _previousScore = currentScore;
        }
    }
}
