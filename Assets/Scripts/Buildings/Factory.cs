using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    [SerializeField]  private ResourceType _producedResource;
    private int _amount = 5;

    protected override void DoIncome()
    {
        GameManager.Instance.PlayerModel.AddResource(_producedResource, _amount * GameManager.Instance.PlayerModel.Level);
    }
}
