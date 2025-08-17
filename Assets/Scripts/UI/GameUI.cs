using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
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
        _saveButton.onClick.AddListener(OnSaveClicked);
        _exitToMenuButton.onClick.AddListener(OnExitClicked);
        GameManager.Instance.PlayerModel.OnLevelChanged += UpdateLevel;
        GameManager.Instance.PlayerModel.OnMoneyChanged += UpdateMoney;
        LoadPlayerStatsUI();
    }

    private void LoadPlayerStatsUI()
    {
        _moneyText.text = $"Money: {GameManager.Instance.PlayerModel.Money.ToString()}";
        _levelText.text = $"Level: {GameManager.Instance.PlayerModel.Level.ToString()}";
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
        GameManager.Instance.SaveGame();
    }

    private void OnBuildClicked(BuildingData buildingData)
    {
        if (GameManager.Instance.CheckMoneyConstruction(buildingData))
        {
            GameManager.Instance.StartBuilding(buildingData);
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

    private void OnDestroy()
    {
        _officeBuilding.onClick.RemoveAllListeners();
        _ironFactoryBuilding.onClick.RemoveAllListeners();
        _plasticFactoryBuilding.onClick.RemoveAllListeners();
        _productsMenuButton.onClick.RemoveAllListeners();
        _blueprintsMenuButton.onClick.RemoveAllListeners();
        _exitToMenuButton.onClick.RemoveAllListeners();
        _saveButton.onClick.RemoveAllListeners();

        if (GameManager.Instance != null && GameManager.Instance.PlayerModel != null)
        {
            GameManager.Instance.PlayerModel.OnLevelChanged -= UpdateLevel;
            GameManager.Instance.PlayerModel.OnMoneyChanged -= UpdateMoney;
        }
    }
}
