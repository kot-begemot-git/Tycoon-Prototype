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
        GameManager.Instance.PlayerModel.OnResourceChanged += OnResourceChanged;
    }

    private void OnStartCraftClicked()
    {
        if (GameManager.Instance.HasEnoughResources())
        {
            _craftManager.StartCraft();
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

    private void DisplayBlueprint()
    {
        BlueprintData blueprint = _craftManager.CurrentBlueprint;

        _craftManager.SetProductionAmountDef();
        SetQuantityText();
        SetSliderDefaultValue();
        UpdateNeedResourcesUI(blueprint);
        UpdateInStockResourcesUI(blueprint);
        UpdateDescriptionUI(blueprint);
    }

    private void SetQuantityText()
    {
        _quantityText.text = $"Count: {_craftManager.ProductionAmount.ToString()}";
    }

    private void SetSliderDefaultValue()
    {
        _slider.value = _craftManager.ProductionAmount;
    }

    private void UpdateNeedResourcesUI(BlueprintData blueprint)
    {
        string firstResource = FormatResource(blueprint.FirstResource.ResourceType, blueprint.FirstResource.Amount);
        string secondResource = FormatResource(blueprint.SecondResource.ResourceType, blueprint.SecondResource.Amount);

        _needResourceText.text = $"Need: {firstResource}, {secondResource}";
    }

    private void UpdateInStockResourcesUI(BlueprintData blueprint)
    {
        PlayerModel playerModel = GameManager.Instance.PlayerModel;

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

    private void OnResourceChanged(ResourceType resource, int amount)
    {
        UpdateInStockResourcesUI(_craftManager.CurrentBlueprint);
    }

    private void OnDestroy()
    {
        _startCraftButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);

        if (GameManager.Instance != null && GameManager.Instance.PlayerModel != null)
        {
            GameManager.Instance.PlayerModel.OnResourceChanged -= OnResourceChanged;
        }
        if (_slider != null)
        {
            _slider.onValueChanged.RemoveAllListeners();
        }
    }
}
