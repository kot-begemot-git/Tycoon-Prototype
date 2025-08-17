using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerModel
{
    private int _level;
    private int _money;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(_level);
        }
    }

    public int Money
    {
        get => _money;
        set
        {
            _money += value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public Dictionary<ResourceType, int> Resources { get; private set; }
    public Dictionary<string, int> Products { get; private set; }

    public event Action<int> OnLevelChanged;
    public event Action<int> OnMoneyChanged;
    public event Action<ResourceType, int> OnResourceChanged;
    //public event Action<string, int> OnProductChanged;

    public PlayerModel()
    {
        _level = 1;
        _money = 0;
        Resources = new();
        Products = new();
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
        OnResourceChanged?.Invoke(resource, value);
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
