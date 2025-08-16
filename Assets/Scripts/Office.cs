using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : Building
{
    private int _income = 1000;
    protected override void DoIncome()
    {
        _gameManager.PlayerModel.AddMoney(_gameManager.PlayerModel.Level * _income);
    }
}
