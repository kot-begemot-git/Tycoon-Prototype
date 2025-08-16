using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintPanelUI : MonoBehaviour
{
    [SerializeField] private Button _carBlueprintButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _craftPanel;
    [SerializeField] private CraftManager _craftManager;
    [SerializeField] private BlueprintData _carBlueprint;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _carBlueprintButton.onClick.AddListener(() => OnBlueprintClicked(_carBlueprint));
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnBlueprintClicked(BlueprintData blueprint)
    {
        _craftManager.CurrentBlueprint = blueprint;
        _craftPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnExitClicked()
    {
        gameObject.SetActive(false);
    }
}
