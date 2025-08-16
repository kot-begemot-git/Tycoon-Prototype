using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerModel
{
    public int Level { get; private set; }
    public int Money { get; private set; }

    public Dictionary<ResourceType, int> Resources { get; private set; }
    public Dictionary<string, int> Products { get; private set; }

    public PlayerModel()
    {
        Level = 1;
        Money = 10000;
        Resources = new();
        Products = new();
    }

    public void UpLevel()
    {
        Level++;
        Debug.Log(Level);
    }
    
    public void AddMoney(int value)
    {
        Money += value;
        Debug.Log(Money);
    }

    public void AddResource(ResourceType resource, int value)
    {
        Debug.Log(string.Join(", ", Resources.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
        if (Resources.ContainsKey(resource))
        {
            Resources[resource] += value;
        }
        else
        {
            Resources[resource] = value;
        }
    }
}
