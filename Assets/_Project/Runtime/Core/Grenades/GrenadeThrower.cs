using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using _Project.Runtime.Config;
using _Project.Runtime.Core.Grenades.GlobalWorldChange;
using _Project.Runtime.Core.Grenades.PotionLogic;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades
{
    public class GrenadeThrower : IInitializable, ITickable, IDisposable, IPotionSelector

    {
        private bool _isRecoveringGrenades = false;
        private bool _readyToThrow = true;


        private readonly IHerbalistProvider _herbalistProvider;
        private readonly IInputService _inputService;

        private readonly IGrenadeConfig _grenadeConfig;
        private readonly Timer _throwCoolDownTimer;
        private readonly Timer _grenadeRecoveryTimer;
        private readonly IPotionApplierFactory _potionApplierFactory;
        private readonly IReadOnlyList<Action> _potionsApplyFunctions;
        private readonly IReadOnlyList<string> _potionsNames;
        private int _currentPotionIntex = 0;
        private PotionIngredients _potionIngredients;
        private readonly List<PotionIngredients> _potionPrices;
        private readonly IGlobalWorldChangeProvider _globalWorldChangeProvider;

        public event Action<string> SelectedPotionUpdated;

        public PotionIngredients CurrentIngredientsCount => new(_potionIngredients);
        public PotionIngredients CurrentPotionAmount => new(_potionPrices[_currentPotionIntex]);

        public event Action<PotionIngredients> CurrentIngredientCountChanged;
        public event Action<PotionIngredients> SelectedPotionAmountChanged;


        public GrenadeThrower(
            IHerbalistProvider herbalistProvider,
            IInputService inputService,
            IGrenadeConfig grenadeConfig,
            IPotionApplierFactory potionApplierFactory,
            IGlobalWorldChangeProvider globalWorldChangeProvider,
            Timer throwCoolDownTimer,
            Timer grenadeRecoveryTimer
        )
        {
            _herbalistProvider = herbalistProvider;
            _inputService = inputService;
            _grenadeConfig = grenadeConfig;
            _potionApplierFactory = potionApplierFactory;
            _globalWorldChangeProvider = globalWorldChangeProvider;
            _throwCoolDownTimer = throwCoolDownTimer;
            _grenadeRecoveryTimer = grenadeRecoveryTimer;
            _potionsApplyFunctions = new ReadOnlyCollection<Action>(new List<Action>()
            {
                () =>
                {
                    _potionApplierFactory.ApplyWorldChange(_inputService.Mouse,
                        _herbalistProvider.Herbalist.Transform.position);
                },
                () =>
                {
                    _potionApplierFactory.ApplyHealing(herbalistProvider);
                },
                () =>
                {
                    _potionApplierFactory.ApplyExplosion(_inputService.Mouse,
                        _herbalistProvider.Herbalist.Transform.position);
                },
                () =>
                {
                    _potionApplierFactory.ApplyPoison(_inputService.Mouse,
                        _herbalistProvider.Herbalist.Transform.position);
                }
            });
            _potionsNames = new ReadOnlyCollection<string>(new List<string>()
            {
                "Мир", "Подорожник", "Бдыщ", "Яд"
            });
            _potionIngredients = new PotionIngredients(1, 1, 2);
            _potionPrices = new List<PotionIngredients>()
            {
                new(1, 1, 1),
                new(2, 0, 0),
                new(0, 2, 0),
                new(0, 2, 2),
            };
        }

        public void AddIngredient(PotionIngredients ingredient)
        {
            _potionIngredients = (_potionIngredients+ingredient).ClipTop();
            CurrentIngredientCountChanged?.Invoke(CurrentIngredientsCount);
        }

        public string GetCurrentPotionName()
        {
            return _potionsNames[_currentPotionIntex];
        }

        public void Initialize()
        {
            _throwCoolDownTimer.TimeEnded += ResetThrow;
            _grenadeRecoveryTimer.TimeEnded += RecoverGrenade;
            CurrentIngredientCountChanged += ingredients =>
            {
                Debug.Log(
                    $"CurrentIngredientCount: r={ingredients.Red} g={ingredients.Green} b={ingredients.Blue}");
            };
        }

        public void Tick()
        {
            if (_inputService.IsPotionFirstButtonPressed)
            {
                MovePotionIndex(0);
                return;
            }
            if (_inputService.IsPotionSecondButtonPressed)
            {
                MovePotionIndex(1);
                return;
            }
            if (_inputService.IsPotionThirdButtonPressed)
            {
                MovePotionIndex(2);
                return;
            }
            if (_inputService.IsPotionFourthButtonPressed)
            {
                MovePotionIndex(3);
                return;
            }
            
            if (_inputService.IsPotionApplyButtonPressed)
                TryCallPotion();
        }

        public string GetPreviousPotionName()
        {
            var ind = _currentPotionIntex;
            _currentPotionIntex =
                (_currentPotionIntex - 1 + _potionsApplyFunctions.Count) % _potionsApplyFunctions.Count;
            var name = _potionsNames[_currentPotionIntex];
            _currentPotionIntex = ind;

            return name;
        }

        public string GetNextPotionName()
        {
            var ind = _currentPotionIntex;
            _currentPotionIntex++;
            _currentPotionIntex %= _potionsApplyFunctions.Count;
            var name = _potionsNames[_currentPotionIntex];
            _currentPotionIntex = ind;

            return name;
        }

        private void MovePotionIndex(int ind)
        {
            _currentPotionIntex = ind;
            SelectedPotionUpdated?.Invoke(_potionsNames[_currentPotionIntex]);
            SelectedPotionAmountChanged?.Invoke(CurrentPotionAmount);
        }

        private void TryCallPotion()
        {
            if (!_readyToThrow || _globalWorldChangeProvider.IsActive)
                return;
            if (!CurrentIngredientsCount.IsNotLess(CurrentPotionAmount))
            {
                return;
            }

            _readyToThrow = false;

            _potionIngredients -= CurrentPotionAmount;
            CurrentIngredientCountChanged?.Invoke(CurrentIngredientsCount);
            _herbalistProvider.Herbalist.Transform.DOLookAt(_inputService.Mouse, 0.2f).OnComplete(() =>
            {
                ThrowGrenade();

                _throwCoolDownTimer.Start(_grenadeConfig.GrenadeThrowsTimeout);

                
                if (!_isRecoveringGrenades)
                {
                    _isRecoveringGrenades = true;
                    _grenadeRecoveryTimer.Start(_grenadeConfig.GrenadeRecoveryTimeout);
                }
            });
        }

        private void ThrowGrenade()
        {
            _potionsApplyFunctions[_currentPotionIntex].Invoke();
        }


        private void ResetThrow()
        {
            _readyToThrow = true;
        }

        private void RecoverGrenade()
        {
            _isRecoveringGrenades = false;
        }

        public void Dispose()
        {
            _throwCoolDownTimer.TimeEnded -= ResetThrow;
            _grenadeRecoveryTimer.TimeEnded -= RecoverGrenade;
        }

        public void FillIngredientTillPotion(int potionId)
        {
            AddIngredient((_potionPrices[potionId] - _potionIngredients).Clip());
        }
        
        public bool NeedIngredient(int potionId)
        {
            var potion =(_potionPrices[potionId] - _potionIngredients).Clip();
            return potion.Red+potion.Green+potion.Blue > 0;
        }
    }

    public interface IPotionSelector
    {
        public event Action<string> SelectedPotionUpdated;
        public string GetCurrentPotionName();

        public string GetPreviousPotionName();

        public string GetNextPotionName();

        //получить текущие значения 1 раз, при инициалзиации интерфейса
        //текущий баланс интредиентов
        public PotionIngredients CurrentIngredientsCount { get; }

        //текущая стоимость выбранного зелья
        public PotionIngredients CurrentPotionAmount { get; }

        //подписаться на обновления
        //изменился текущий баланс
        public event Action<PotionIngredients> CurrentIngredientCountChanged;

        //выбранное зелье изменилось и его баланс тоже
        public event Action<PotionIngredients> SelectedPotionAmountChanged;

        public void AddIngredient(PotionIngredients ingredient);

        void FillIngredientTillPotion(int potionId);
    }

    //dto для хранения и передачи инфы об ингредиентах зелья
    public class PotionIngredients
    {
        public int Red;
        public int Green;
        public int Blue;

        public PotionIngredients(int r, int g, int b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public PotionIngredients(PotionIngredients p)
        {
            Red = p.Red;
            Green = p.Green;
            Blue = p.Blue;
        }

        public static PotionIngredients operator -(PotionIngredients o1, PotionIngredients o2)
        {
            return new PotionIngredients(o1.Red - o2.Red, o1.Green - o2.Green, o1.Blue - o2.Blue);
        }

        public static PotionIngredients operator +(PotionIngredients o1, PotionIngredients o2)
        {
            return new PotionIngredients(o1.Red + o2.Red, o1.Green + o2.Green, o1.Blue + o2.Blue);
        }
        
        public PotionIngredients Clip()
        {
            return new PotionIngredients(Mathf.Max(Red,0),Mathf.Max(Green,0),Mathf.Max(Blue,0));
        }
        
        public PotionIngredients ClipTop()
        {
            return new PotionIngredients(Mathf.Min(Red,6),Mathf.Min(Green,6),Mathf.Min(Blue,6));
        }

        public bool IsNotLess(PotionIngredients o2)
        {
            return Red >= o2.Red && Green >= o2.Green && Blue >= o2.Blue;
        }
    }
}