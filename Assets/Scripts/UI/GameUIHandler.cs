using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private Button _officeBuilding;
    [SerializeField] private Button _ironFactoryBuilding;
    [SerializeField] private Button _plasticFactoryBuilding;
    [SerializeField] private Button _blueprintsMenuButton;
    [SerializeField] private Button _productsMenuButton;
    [SerializeField] private BuildingData _officeBuildingData;
    [SerializeField] private BuildingData _ironFactoryBuildingData;
    [SerializeField] private BuildingData _plasticFactoryBuildingData;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _blueprintsMenu;
    [SerializeField] private GameObject _productsMenu;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _officeBuilding.onClick.AddListener(() => OnBuildClicked(_officeBuildingData));
        _ironFactoryBuilding.onClick.AddListener(() => OnBuildClicked(_ironFactoryBuildingData));
        _plasticFactoryBuilding.onClick.AddListener(() => OnBuildClicked(_plasticFactoryBuildingData));
        _productsMenuButton.onClick.AddListener(OnProductsMenuClicked);
        _blueprintsMenuButton.onClick.AddListener(OnBlueprintsMenuClicked);
    }

    private void OnBuildClicked(BuildingData buildingData)
    {
        if (_gameManager.CheckMoneyConstruction(buildingData))
        {
            _gameManager.StartBuilding(buildingData);
        }
    }

    private void OnProductsMenuClicked()
    {
        _productsMenu.SetActive(true);
    }

    private void OnBlueprintsMenuClicked()
    {
        _blueprintsMenu.SetActive(true);
    }
}
