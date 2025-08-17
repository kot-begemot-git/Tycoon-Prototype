using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GraphicsSettingsManager : MonoBehaviour
{
    [SerializeField]
    public UniversalRenderPipelineAsset HighQualityAsset;
    [SerializeField]
    public UniversalRenderPipelineAsset MediumQualityAsset;
    [SerializeField]
    public UniversalRenderPipelineAsset LowQualityAsset;

    private const string SettingsPath = "graphics_settings.json";
    private GameSettings _currentSettings;

    public string CurrentPresetName =>
        QualitySettings.renderPipeline != null ?
        QualitySettings.renderPipeline.name : "Default";

    private void Awake()
    {
        LoadSettings();
        ApplySavedGraphics();
    }

    public void LoadSettings()
    {
        string filePath = Path.Combine(Application.persistentDataPath, SettingsPath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _currentSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            _currentSettings = new GameSettings();
        }
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(_currentSettings, true);
        string filePath = Path.Combine(Application.persistentDataPath, SettingsPath);
        File.WriteAllText(filePath, json);
    }

    public void ApplySavedGraphics()
    {
        if (string.IsNullOrEmpty(_currentSettings.GraphicsPresetName))
        {
            ApplyGraphicsPreset(MediumQualityAsset);
            return;
        }

        UniversalRenderPipelineAsset selectedPreset = null;
        UniversalRenderPipelineAsset[] allPresets = { HighQualityAsset, MediumQualityAsset, LowQualityAsset };

        foreach (var preset in allPresets)
        {
            if (preset != null && preset.name == _currentSettings.GraphicsPresetName)
            {
                selectedPreset = preset;
                break;
            }
        }

        if (selectedPreset != null)
        {
            ApplyGraphicsPreset(selectedPreset);
        }
        else
        {
            Debug.LogWarning($"Graphics preset '{_currentSettings.GraphicsPresetName}' not found. Using default.");
            ApplyGraphicsPreset(MediumQualityAsset);
        }
    }

    public void ApplyGraphicsPreset(UniversalRenderPipelineAsset preset)
    {
        if (preset == null)
        {
            Debug.LogError("Graphics preset is not assigned!");
            return;
        }

        GraphicsSettings.renderPipelineAsset = preset; 
        QualitySettings.renderPipeline = preset;       
        _currentSettings.GraphicsPresetName = preset.name;
        SaveSettings();

        Debug.Log($"Applied graphics preset: {preset.name}");
    }
}



