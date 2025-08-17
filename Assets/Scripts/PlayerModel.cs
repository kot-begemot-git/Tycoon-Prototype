using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerModel
{
    public int Level { get; private set; }
    public int Money { get; private set; }

    public Dictionary<ResourceType, int> Resources { get; private set; }
    public Dictionary<string, int> Products { get; private set; }

    public event Action<int> OnLevelChanged;
    public event Action<int> OnMoneyChanged;
    //public event Action<ResourceType, int> OnResourceChanged;
    //public event Action<string, int> OnProductChanged;

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
        OnLevelChanged?.Invoke(Level);
        Debug.Log(Level);
    }
    
    public void AddMoney(int value)
    {
        Money += value;
        OnMoneyChanged?.Invoke(Money);
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
        //OnResourceChanged?.Invoke(resource, Resources[resource]);
    }

    public void AddProduct(string name, int amount)
    {
        if (Products.ContainsKey(name))
        {
            Products[name] += amount;
        }
        else
        {
            Products[name] = amount;
        }
        //OnProductChanged?.Invoke(name, Products[name]);
    }
}
