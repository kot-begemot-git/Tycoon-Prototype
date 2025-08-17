using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    [SerializeField] private DragAndDrop _dragAndDropSystem;
    private GameManager _gameManager;
    private BuildingData _currentBuildingData;
    private GameObject _currentBuilding;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void StartBuilding(BuildingData buildingData)
    {
        _currentBuildingData = buildingData;
        _currentBuilding = Instantiate(buildingData.prefab);
        _dragAndDropSystem.StartDrag(_currentBuilding);
    }

    public void EndBuilding()
    {
        _currentBuilding.GetComponent<Building>().IsBuilt = true;
        _currentBuilding = null;
        _gameManager.PlayerModel.AddMoney(-_currentBuildingData.buildCost);
        _currentBuildingData = null; 
    }
}
