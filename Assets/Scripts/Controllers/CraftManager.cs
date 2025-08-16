using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public int ProductionAmount { get; set; }
    public BlueprintData CurrentBlueprint { get; set; }
    [SerializeField] private GameManager _gameManager;

    public void StartCraft()
    {
        ResourceAmount firstRes = CurrentBlueprint.FirstResource;
        ResourceAmount secondRes = CurrentBlueprint.SecondResource;

        int firstAmount = firstRes.Amount * ProductionAmount;
        int secondAmount = secondRes.Amount * ProductionAmount;

        _gameManager.PlayerModel.AddResource(firstRes.ResourceType, -firstAmount);
        _gameManager.PlayerModel.AddResource(secondRes.ResourceType, -secondAmount);

        string productName = CurrentBlueprint.name;
        if (_gameManager.PlayerModel.Products.ContainsKey(productName))
        {
            _gameManager.PlayerModel.Products[productName] += ProductionAmount;
        }
        else
        {
            _gameManager.PlayerModel.Products[productName] = ProductionAmount;
        }
    }
}
