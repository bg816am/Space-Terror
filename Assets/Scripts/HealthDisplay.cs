using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI _playerHealth;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GetComponent<TextMeshProUGUI>();
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHealth.text = _player.GetHealth().ToString();
    }
}
