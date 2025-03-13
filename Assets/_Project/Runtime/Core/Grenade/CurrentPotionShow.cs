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
    [SerializeField] private TMP_Text _potionTextName;

    [SerializeField] private SpritePotionAssociation[] _icons;

    [SerializeField] private RectTransform _greenIngrSlider;
    [SerializeField] private RectTransform _redIngrSlider;
    [SerializeField] private RectTransform _blueIngrSlider;
    [SerializeField] private float _maxIngrCount;

    [SerializeField] private RectTransform _greenIngrCostSlider;
    [SerializeField] private RectTransform _redIngrCostSlider;
    [SerializeField] private RectTransform _blueIngrCostSlider;

    [SerializeField] private List<GameObject> _potionObjects;


    private IPotionSelector _potionSelector;

    private Dictionary<string, Image> _iconDict = new();

    [Inject]
    private void Construct(IPotionSelector potionSelector)
    {
        _potionSelector = potionSelector;
        _potionSelector.SelectedPotionUpdated += SetText;

        SetCurrentIngredients(_potionSelector.CurrentIngredientsCount);
        SetCurrentPotionCost(_potionSelector.CurrentPotionAmount);

        _potionSelector.SelectedPotionAmountChanged += SetCurrentPotionCost;
        _potionSelector.CurrentIngredientCountChanged += SetCurrentIngredients;

        initImages();

        _potionTextName.text = _potionSelector.GetCurrentPotionName();
        SetText(_potionSelector.GetCurrentPotionName());
    }

    private void initImages()
    {
        for (int i = 0; i < _potionObjects.Count; i++)
        {
            var obj = _potionObjects[i];
            obj.transform.GetChild(2).GetComponent<Image>().sprite = _icons[i].icon;
            _iconDict.Add(_icons[i].Name, obj.transform.GetChild(1).GetComponent<Image>());
        }
    }

    private void SetText(string text)
    {
        _iconDict[_potionTextName.text].color = Color.black;
        _potionTextName.text = text;
        _iconDict[_potionTextName.text].color = Color.green;
    }

    private void SetCurrentIngredients(PotionIngredients ingredients)
    {
        _greenIngrSlider.anchorMax = _greenIngrSlider.anchorMax.MMSetY(ingredients.Green / _maxIngrCount);
        _redIngrSlider.anchorMax = _redIngrSlider.anchorMax.MMSetY(ingredients.Red / _maxIngrCount);
        _blueIngrSlider.anchorMax = _blueIngrSlider.anchorMax.MMSetY(ingredients.Blue / _maxIngrCount);
    }

    private void SetCurrentPotionCost(PotionIngredients cost)
    {
        _greenIngrCostSlider.anchorMax = _greenIngrCostSlider.anchorMax.MMSetY(cost.Green / _maxIngrCount);
        _redIngrCostSlider.anchorMax = _redIngrCostSlider.anchorMax.MMSetY(cost.Red / _maxIngrCount);
        _blueIngrCostSlider.anchorMax = _blueIngrCostSlider.anchorMax.MMSetY(cost.Blue / _maxIngrCount);
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