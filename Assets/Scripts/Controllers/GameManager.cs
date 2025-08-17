using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerModel PlayerModel;
    private double _baseAmount = 100000; 
    private double _multiplier = 10;
    private ConstructionManager _constructionManager;
    private CraftManager _craftManager;

    private float _saveTimer = 0f;
    private const float AUTO_SAVE_INTERVAL = 60f;
    private List<BuildingSaveData> _buildingsToLoad;
    private bool _isNewGame = true;

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
        AutoSaveTimer();
    }

    private void InitCoreSystems()
    {
        PlayerModel = new PlayerModel();
    }

    private void AutoSaveTimer()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            _saveTimer += Time.deltaTime;
            if (_saveTimer >= AUTO_SAVE_INTERVAL)
            {
                SaveGame();
                _saveTimer = 0f;
                Debug.Log("Auto-saved game");
            }
        }
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            Level = PlayerModel.Level,
            Money = PlayerModel.Money,
            Resources = new List<ResourceItem>(),
            Products = new List<ProductItem>(),
            Buildings = new List<BuildingSaveData>()
        };

        foreach (var resource in PlayerModel.Resources)
        {
            data.Resources.Add(new ResourceItem
            {
                Type = resource.Key,
                Amount = resource.Value
            });
        }

        foreach (var product in PlayerModel.Products)
        {
            data.Products.Add(new ProductItem
            {
                Name = product.Key,
                Amount = product.Value
            });
        }

        Building[] buildings = FindObjectsOfType<Building>();
        foreach (Building building in buildings)
        {
            if (building.IsBuilt && building.BuildingData != null)
            {
                data.Buildings.Add(new BuildingSaveData
                {
                    BuildingName = building.BuildingData.buildingName,
                    Position = building.transform.position,
                    Rotation = building.transform.rotation
                });
            }
        }

        string json = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        File.WriteAllText(filePath, json);
        Debug.Log($"Game saved to: {filePath}");
    }

    public void LoadGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            PlayerModel = new();
            PlayerModel.Level = data.Level;
            PlayerModel.Money = data.Money;

            PlayerModel.Resources.Clear();
            foreach (ResourceItem item in data.Resources)
            {
                PlayerModel.Resources[item.Type] = item.Amount;
            }

            PlayerModel.Products.Clear();
            foreach (ProductItem item in data.Products)
            {
                PlayerModel.Products[item.Name] = item.Amount;
            }

            _buildingsToLoad = data.Buildings;
            _isNewGame = false;

            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogError("No save file found!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            _constructionManager = FindObjectOfType<ConstructionManager>();
            _craftManager = FindObjectOfType<CraftManager>();

            if (!_isNewGame && _buildingsToLoad != null)
            {
                LoadBuildings();
                _buildingsToLoad = null;
            }
        }
        else if (scene.name == "MainMenu")
        {
            _constructionManager = null;
            _craftManager = null;
        }
    }

    private void LoadBuildings()
    {
        Building[] existingBuildings = FindObjectsOfType<Building>();
        foreach (Building building in existingBuildings)
        {
            Destroy(building.gameObject);
        }

        foreach (BuildingSaveData buildingData in _buildingsToLoad)
        {
            BuildingData data = Resources.Load<BuildingData>($"Buildings/{buildingData.BuildingName}");
            if (data != null)
            {
                GameObject buildingObj = Instantiate(data.prefab, buildingData.Position, buildingData.Rotation);
                Building buildingComponent = buildingObj.GetComponent<Building>();
                buildingComponent.IsBuilt = true;
                buildingComponent.BuildingData = data;
            }
            else
            {
                Debug.LogError($"Building prefab not found: {buildingData.BuildingName}");
            }
        }
    }

    public void StartNewGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        PlayerModel = new PlayerModel();
        _isNewGame = true;

        SceneManager.LoadScene("GameScene");
    }

    private void CheckLevelUp()
    {
        double nextLevelRequirement = GetMoneyRequiredForLevel(PlayerModel.Level + 1);

        if (PlayerModel.Money >= nextLevelRequirement)
        {
            PlayerModel.Level++;
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
