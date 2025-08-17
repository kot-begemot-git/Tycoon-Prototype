using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    [SerializeField] private DragAndDrop _dragAndDropSystem;
    private BuildingData _currentBuildingData;
    private GameObject _currentBuilding;

    public void StartBuilding(BuildingData buildingData)
    {
        _currentBuildingData = buildingData;
        _currentBuilding = Instantiate(buildingData.prefab);

        Building buildingComponent = _currentBuilding.GetComponent<Building>();
        if (buildingComponent != null)
        {
            buildingComponent.BuildingData = buildingData;
        }

        _dragAndDropSystem.StartDrag(_currentBuilding);
    }

    public void EndBuilding()
    {
        _currentBuilding.GetComponent<Building>().IsBuilt = true;
        _currentBuilding = null;
        GameManager.Instance.PlayerModel.Money = -_currentBuildingData.buildCost;
        _currentBuildingData = null; 
    }
}
