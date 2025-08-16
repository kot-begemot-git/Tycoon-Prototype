using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    protected GameManager _gameManager;
    private int _interval = 1;
    private float _currentTime = 0;
    public bool IsBuilt { get; set; }

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        IncomeTimer();   
    }

    private void IncomeTimer()
    {
        if (IsBuilt)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _interval)
            {
                _currentTime = 0;
                DoIncome();
            }
        }
    }

    protected abstract void DoIncome();
}
