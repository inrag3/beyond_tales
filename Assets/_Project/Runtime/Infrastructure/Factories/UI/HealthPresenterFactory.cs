using _Project.Runtime.Core.Health;
using _Project.Runtime.Meta.Health;
using Zenject;
using HealthPresenter = _Project.Runtime.Meta.Health.HealthPresenter;

namespace _Project.Runtime.Infrastructure.Factories.UI
{
    public class HealthPresenterFactory : IHealthPresenterFactory
    {
        private readonly IInstantiator _instantiator;
        public HealthPresenterFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public void Create(HealthView view, IHealth health)
        {
            var presenter = _instantiator.Instantiate<HealthPresenter>(new object[]{view, health});
            presenter.Initialize();
        }
    }

    public interface IHealthPresenterFactory
    {
        public void Create(HealthView view, IHealth health);
    }
}