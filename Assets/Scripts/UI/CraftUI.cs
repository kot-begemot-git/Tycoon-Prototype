using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startCraftButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private CraftManager _craftManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _inStockResourceText;
    [SerializeField] private TextMeshProUGUI _needResourceText;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _mainPanel.SetActive(false);
        DisplayBlueprint();
    }

    private void Init()
    {
        _startCraftButton.onClick.AddListener(OnStartCraftClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
        _slider.onValueChanged.AddListener(OnSliderValueChanged);       
    }

    private void OnStartCraftClicked()
    {
        if (_gameManager.HasEnoughResources())
        {
            _craftManager.StartCraft();
            DisplayBlueprint();
        }
        else
        {
            _warningPanel.SetActive(true);
        }
    }

    private void OnExitClicked()
    {
        _mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnSliderValueChanged(float value)
    {
        _quantityText.text = $"Count: {value}";
        _craftManager.ProductionAmount = (int)value;
    }

    public void DisplayBlueprint()
    {
        BlueprintData blueprint = _craftManager.CurrentBlueprint;

        UpdateNeedResourcesUI(blueprint);
        UpdateInStockResourcesUI(blueprint);
        UpdateDescriptionUI(blueprint);
    }

    private void UpdateNeedResourcesUI(BlueprintData blueprint)
    {
        string firstResource = FormatResource(blueprint.FirstResource.ResourceType, blueprint.FirstResource.Amount);
        string secondResource = FormatResource(blueprint.SecondResource.ResourceType, blueprint.SecondResource.Amount);

        _needResourceText.text = $"Need: {firstResource}, {secondResource}";
    }

    private void UpdateInStockResourcesUI(BlueprintData blueprint)
    {
        PlayerModel playerModel = _gameManager.PlayerModel;

        string firstResourceName = blueprint.FirstResource.ResourceType.ToString();
        string secondResourceName = blueprint.SecondResource.ResourceType.ToString();

        string result = string.Join(", ",
            playerModel.Resources
                .Where(x => x.Key.ToString() == firstResourceName || x.Key.ToString() == secondResourceName)
                .Select(x => $"{x.Key} - {x.Value}")
        );

        _inStockResourceText.text = $"In Stock: {result}";
    }

    private void UpdateDescriptionUI(BlueprintData blueprint)
    {
        _descriptionText.text = blueprint.name;
    }

    private string FormatResource(ResourceType resourceType, int amount)
    {
        return $"{resourceType} - {amount}";
    }
}
