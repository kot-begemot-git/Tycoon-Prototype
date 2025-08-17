using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public int ProductionAmount { get; set; }
    public BlueprintData CurrentBlueprint { get; set; }
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void StartCraft()
    {
        ResourceAmount firstRes = CurrentBlueprint.FirstResource;
        ResourceAmount secondRes = CurrentBlueprint.SecondResource;

        int firstAmount = firstRes.Amount * ProductionAmount;
        int secondAmount = secondRes.Amount * ProductionAmount;

        _gameManager.PlayerModel.AddResource(firstRes.ResourceType, -firstAmount);
        _gameManager.PlayerModel.AddResource(secondRes.ResourceType, -secondAmount);

        string productName = CurrentBlueprint.name;
        _gameManager.PlayerModel.AddProduct(productName, ProductionAmount);
    }
}
