using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    public int Score { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    public void RemovePoints(int points)
    {
        Score = Mathf.RoundToInt(Mathf.Clamp(Score - points, 0f, int.MaxValue));
    }
}
