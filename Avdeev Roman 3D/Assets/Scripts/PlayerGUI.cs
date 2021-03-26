using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private HealthObject _playerHeath;
    private Rect _healthArea = new Rect(0, Screen.height-50, 100, 50);
    private string _healthUI;
    void Start()
    {
        _playerHeath = GetComponent<HealthObject>();
    }
    private void OnGUI()
    {
        _healthUI = @$"{_playerHeath.CurrentHealth} / {_playerHeath.MaxHealth}";
        GUI.Box(_healthArea,_healthUI);
    }
}
