using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
     private TextMeshProUGUI _playerScore;
     private GameSession _gameSession;

    // Start is called before the first frame update
    private void Start()
    {
        _playerScore = GetComponent<TextMeshProUGUI>();
        _gameSession = FindObjectOfType<GameSession>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        _playerScore.text = _gameSession.GetScore().ToString();
    }
}
