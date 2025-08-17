using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _highButton;
    [SerializeField] private Button _mediumButton;
    [SerializeField] private Button _lowButton;
    private GraphicsSettingsManager _graphicsSettingsManager;

    private void Awake()
    {
        _graphicsSettingsManager = FindObjectOfType<GraphicsSettingsManager>();
        Init();
    }

    private void Init()
    {
        _newGameButton.onClick.AddListener(OnStartNewGameClicked);
        _loadGameButton.onClick.AddListener(OnLoadGameClicked);
        _settingsButton.onClick.AddListener(OnOpenSettingsClicked);
        _exitButton.onClick.AddListener(OnExitGameClicked);
        _backButton.onClick.AddListener(OnCloseSettingsClicked);
        _highButton.onClick.AddListener(() => ApplyGraphicsPreset(_graphicsSettingsManager.HighQualityAsset));
        _mediumButton.onClick.AddListener(() => ApplyGraphicsPreset(_graphicsSettingsManager.MediumQualityAsset));
        _lowButton.onClick.AddListener(() => ApplyGraphicsPreset(_graphicsSettingsManager.LowQualityAsset));

        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    private void OnOpenSettingsClicked()
    {
        _mainPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    private void OnCloseSettingsClicked()
    {
        _settingsPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

    private void ApplyGraphicsPreset(UniversalRenderPipelineAsset preset)
    {
        if (_graphicsSettingsManager == null) return;
        _graphicsSettingsManager.ApplyGraphicsPreset(preset);
    }

    private void OnStartNewGameClicked()
    {
        GameManager.Instance.StartNewGame();
    }

    private void OnLoadGameClicked()
    {
        GameManager.Instance.LoadGame();
    }

    private void OnExitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        _newGameButton.onClick.RemoveAllListeners();
        _loadGameButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
        _highButton.onClick.RemoveAllListeners();
        _mediumButton.onClick.RemoveAllListeners();
        _lowButton.onClick.RemoveAllListeners();
    }
}