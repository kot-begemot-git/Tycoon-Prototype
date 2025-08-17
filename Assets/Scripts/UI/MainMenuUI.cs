using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button highButton;
    [SerializeField] private Button middleButton;
    [SerializeField] private Button lowButton;
    [SerializeField] private Button backButton;
    [SerializeField] private UniversalRenderPipelineAsset highQualityAsset;
    [SerializeField] private UniversalRenderPipelineAsset middleQualityAsset;
    [SerializeField] private UniversalRenderPipelineAsset lowQualityAsset;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        newGameButton.onClick.AddListener(OnStartNewGameClicked);
        loadGameButton.onClick.AddListener(OnLoadGameClicked);
        settingsButton.onClick.AddListener(OnOpenSettingsClicked);
        exitButton.onClick.AddListener(OnExitGameClicked);
        highButton.onClick.AddListener(() => ApplyGraphicsPreset(highQualityAsset));
        middleButton.onClick.AddListener(() => ApplyGraphicsPreset(middleQualityAsset));
        lowButton.onClick.AddListener(() => ApplyGraphicsPreset(lowQualityAsset));
        backButton.onClick.AddListener(OnCloseSettingsClicked);

        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    private void OnStartNewGameClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnLoadGameClicked()
    {
        Debug.Log("Load game not implemented yet.");
    }

    private void OnOpenSettingsClicked()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void OnCloseSettingsClicked()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void OnExitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }


    private void ApplyGraphicsPreset(UniversalRenderPipelineAsset preset)
    {
        if (preset == null)
        {
            Debug.LogError("Graphics preset is not assigned!");
            return;
        }

        Debug.Log($"Applied graphics preset: {preset.name}");
        QualitySettings.renderPipeline = preset;
    }
}

