using Leopotam.Ecs;
using UnityEngine;

public class HealthSystem : IEcsRunSystem
{
    private readonly EcsFilter<HealthComponent> _healthFilter;

    public void Run()
    {
        foreach (var entity in _healthFilter)
        {
            ref var healthComponent = ref _healthFilter.Get1(entity);

            if (healthComponent.Health <= 0)
                Debug.Log("DIED");
        }
    }
}
