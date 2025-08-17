using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Level;
    public int Money;
    public List<ResourceItem> Resources;
    public List<ProductItem> Products;
    public List<BuildingSaveData> Buildings;
}
