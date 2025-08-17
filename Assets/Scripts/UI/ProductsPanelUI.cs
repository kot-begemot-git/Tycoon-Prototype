using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductsPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _productsPanel;
    [SerializeField] private TextMeshProUGUI _carProductsText;
    [SerializeField] private Button _exitButton;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Init();
    }
    private void OnEnable()
    {
        var products = _gameManager.PlayerModel.Products;
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
}
