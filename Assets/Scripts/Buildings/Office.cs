using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : Building
{
    private int _income = 1000;
    protected override void DoIncome()
    {
        GameManager.Instance.PlayerModel.Money = GameManager.Instance.PlayerModel.Level * _income;
    }
}
