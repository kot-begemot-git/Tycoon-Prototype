using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    private int _interval = 1;
    private float _currentTime = 0;
    public BuildingData BuildingData;
    public bool IsBuilt { get; set; }


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
