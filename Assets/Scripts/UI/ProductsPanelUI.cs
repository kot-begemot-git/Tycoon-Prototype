using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductsPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _productsPanel;
    [SerializeField] private TextMeshProUGUI _carProductsText;
    [SerializeField] private Button _exitButton;
   
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        var products = GameManager.Instance.PlayerModel.Products;
        if (products.ContainsKey("Car"))
        {
            _carProductsText.text = $"Car: {products["Car"]}";
        }
        else
        {
            _carProductsText.text = $"Car: 0";
        }
    }

    private void Init()
    {
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnExitClicked()
    {
        _productsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }
}
