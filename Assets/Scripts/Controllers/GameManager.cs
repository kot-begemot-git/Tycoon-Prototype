using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public PlayerModel PlayerModel;
    private double _baseAmount = 100000; 
    private double _multiplier = 10;
    private ConstructionManager _constructionManager;
    private CraftManager _craftManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        InitCoreSystems();
    }

    private void Update()
    {
        CheckLevelUp();
    }

    private void InitCoreSystems()
    {
        PlayerModel = new PlayerModel();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene") 
        {
            _constructionManager = FindObjectOfType<ConstructionManager>();
            _craftManager = FindObjectOfType<CraftManager>();
        }
        else if (scene.name == "MainMenu") 
        {
            _constructionManager = null;
            _craftManager = null;
        }
    }

    private void CheckLevelUp()
    {
        double nextLevelRequirement = GetMoneyRequiredForLevel(PlayerModel.Level + 1);

        if (PlayerModel.Money >= nextLevelRequirement)
        {
            PlayerModel.UpLevel();
        }
    }

    private double GetMoneyRequiredForLevel(int level)
    {
        if (level <= 1) return 0;
        return _baseAmount * Mathf.Pow((float)_multiplier, level - 2);
    }

    public bool CheckMoneyConstruction(BuildingData buildingData)
    {
        return buildingData.buildCost <= PlayerModel.Money;
    }

    public void StartBuilding(BuildingData buildingData)
    {
        _constructionManager.StartBuilding(buildingData);
    }

    public bool HasEnoughResources()
    {
        if (_craftManager.ProductionAmount <= 0) return false;
        BlueprintData blueprint = _craftManager.CurrentBlueprint;
        ResourceAmount firstRes = blueprint.FirstResource;
        ResourceAmount secondRes = blueprint.SecondResource;

        int requiredFirstRes = firstRes.Amount * _craftManager.ProductionAmount;
        int requiredSecondRes = secondRes.Amount * _craftManager.ProductionAmount;

        PlayerModel.Resources.TryGetValue(firstRes.ResourceType, out int firstResPlayerAmount);
        PlayerModel.Resources.TryGetValue(secondRes.ResourceType, out int secondResPlayerAmount);

        return firstResPlayerAmount >= requiredFirstRes &&
               secondResPlayerAmount >= requiredSecondRes;
    }
}
