using System.Collections;
using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using TMPro;
using UnityEngine;
using Zenject;

public class CurrentPotionShow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _potionTextName;
    
    private IPotionSelector _potionSelector;
    
    [Inject]
    private void Construct(IPotionSelector potionSelector)
    {
        _potionSelector = potionSelector;
        _potionSelector.SelectedPotionUpdated += SetText;
        SetText(_potionSelector.GetCurrentPotionName());
    }

    private void SetText(string text)
    {
        _potionTextName.text = text;
    }
    
    private void OnDestroy()
    {
        _potionSelector.SelectedPotionUpdated -= SetText;
    }
}
