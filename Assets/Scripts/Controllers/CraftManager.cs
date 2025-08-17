using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public int ProductionAmount { get; set; }
    public BlueprintData CurrentBlueprint { get; set; }

    public void SetProductionAmountDef()
    {
        ProductionAmount = 1;
    }

    public void StartCraft()
    {
        ResourceAmount firstRes = CurrentBlueprint.FirstResource;
        ResourceAmount secondRes = CurrentBlueprint.SecondResource;

        int firstAmount = firstRes.Amount * ProductionAmount;
        int secondAmount = secondRes.Amount * ProductionAmount;

        GameManager.Instance.PlayerModel.AddResource(firstRes.ResourceType, -firstAmount);
        GameManager.Instance.PlayerModel.AddResource(secondRes.ResourceType, -secondAmount);

        string productName = CurrentBlueprint.name;
        GameManager.Instance.PlayerModel.AddProduct(productName, ProductionAmount);
    }
}
