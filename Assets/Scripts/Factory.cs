using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    [SerializeField]  private ResourceType _producedResource;
    private int _amount = 5;

    protected override void DoIncome()
    {
        _gameManager.PlayerModel.AddResource(_producedResource, _amount * _gameManager.PlayerModel.Level);
    }
}
