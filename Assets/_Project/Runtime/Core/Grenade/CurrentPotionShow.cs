using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

public class CurrentPotionShow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _potionTextName;

    [SerializeField]
    private SpritePotionAssociation[] _icons;

    [SerializeField]
    private RectTransform _greenIngrSlider;
    [SerializeField]
    private RectTransform _redIngrSlider;
    [SerializeField]
    private RectTransform _blueIngrSlider;
    [SerializeField]
    private float _maxIngrCount;
    
    [SerializeField]
    private RectTransform _greenIngrCostSlider;
    [SerializeField]
    private RectTransform _redIngrCostSlider;
    [SerializeField]
    private RectTransform _blueIngrCostSlider;

    [SerializeField]
    private Image _currentPotionImg;
    
    [SerializeField]
    private Image _prevPotionImg;
    
    [SerializeField]
    private Image _nextPotionImg;

    private IPotionSelector _potionSelector;

    private Dictionary<string, Sprite> _iconDict = new Dictionary<string, Sprite>();

    [Inject]
    private void Construct(IPotionSelector potionSelector)
    {
        _potionSelector = potionSelector;
        _potionSelector.SelectedPotionUpdated += SetText;

        SetCurrentIngredients(_potionSelector.CurrentIngredientsCount);
        SetCurrentPotionCost(_potionSelector.CurrentPotionAmount);

        _potionSelector.SelectedPotionAmountChanged += SetCurrentPotionCost;
        _potionSelector.CurrentIngredientCountChanged += SetCurrentIngredients;

        foreach (var icon in _icons)
        {
            _iconDict.Add(icon.Name, icon.icon);
        }
        SetText(_potionSelector.GetCurrentPotionName());

    }

    private void SetText(string text)
    {
        _potionTextName.text = text;
        if (_iconDict.TryGetValue(text, out var icon))
        {
            _currentPotionImg.sprite = icon;
            _currentPotionImg.enabled = true;
        }
        else
        {
            _currentPotionImg.enabled = false;
        }
        
        if (_iconDict.TryGetValue(_potionSelector.GetPreviousPotionName(), out var iconPrev))
        {
            _prevPotionImg.sprite = iconPrev;
            _prevPotionImg.enabled = true;
        }
        else
        {
            _prevPotionImg.enabled = false;
        }
        
        if (_iconDict.TryGetValue(_potionSelector.GetNextPotionName(), out var iconNext))
        {
            _nextPotionImg.sprite = iconNext;
            _nextPotionImg.enabled = true;
        }
        else
        {
            _nextPotionImg.enabled = false;
        }
    }

    private void SetCurrentIngredients(PotionIngredients ingredients)
    {
        _greenIngrSlider.anchorMax = _greenIngrSlider.anchorMax.MMSetY(ingredients.Green/_maxIngrCount);
        _redIngrSlider.anchorMax = _redIngrSlider.anchorMax.MMSetY(ingredients.Red/_maxIngrCount);
        _blueIngrSlider.anchorMax = _blueIngrSlider.anchorMax.MMSetY(ingredients.Blue/_maxIngrCount);
    }

    private void SetCurrentPotionCost(PotionIngredients cost)
    {
        _greenIngrCostSlider.anchorMax = _greenIngrCostSlider.anchorMax.MMSetY(cost.Green/_maxIngrCount);
        _redIngrCostSlider.anchorMax = _redIngrCostSlider.anchorMax.MMSetY(cost.Red/_maxIngrCount);
        _blueIngrCostSlider.anchorMax = _blueIngrCostSlider.anchorMax.MMSetY(cost.Blue/_maxIngrCount);
    }


    private void OnDestroy()
    {
        _potionSelector.SelectedPotionUpdated -= SetText;
    }
}

[Serializable]
public class SpritePotionAssociation
{
    public string Name;
    public Sprite icon;
}
