using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public int buildCost;
    public GameObject prefab;
}
