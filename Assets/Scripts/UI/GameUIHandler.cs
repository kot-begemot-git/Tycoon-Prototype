using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private Button _officeBuilding;
    [SerializeField] private Button _ironFactoryBuilding;
    [SerializeField] private Button _plasticFactoryBuilding;
    [SerializeField] private Button _blueprintsMenuButton;
    [SerializeField] private Button _productsMenuButton;
    [SerializeField] private Button _exitToMenuButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private BuildingData _officeBuildingData;
    [SerializeField] private BuildingData _ironFactoryBuildingData;
    [SerializeField] private BuildingData _plasticFactoryBuildingData;
    [SerializeField] private GameObject _blueprintsMenu;
    [SerializeField] private GameObject _productsMenu;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Init();
    }

    private void Init()
    {
        _officeBuilding.onClick.AddListener(() => OnBuildClicked(_officeBuildingData));
        _ironFactoryBuilding.onClick.AddListener(() => OnBuildClicked(_ironFactoryBuildingData));
        _plasticFactoryBuilding.onClick.AddListener(() => OnBuildClicked(_plasticFactoryBuildingData));
        _productsMenuButton.onClick.AddListener(OnProductsMenuClicked);
        _blueprintsMenuButton.onClick.AddListener(OnBlueprintsMenuClicked);
        _saveButton.onClick.AddListener(OnSaveClicked);
        _exitToMenuButton.onClick.AddListener(OnExitClicked);
        _gameManager.PlayerModel.OnLevelChanged += UpdateLevel;
        _gameManager.PlayerModel.OnMoneyChanged += UpdateMoney;
    }

    private void UpdateLevel(int newLevel)
    {
        _levelText.text = $"Level: {newLevel}";
    }

    private void UpdateMoney(int newMoney)
    {
        _moneyText.text = $"Money: {newMoney}";
    }

    private void OnExitClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnSaveClicked()
    {
        
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
