using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : IEcsRunSystem
{
    private readonly EcsFilter<AttackComponent, MovableComponent, FollowComponent> _atackFilter;

    public void Run()
    {
        foreach (var entity in _atackFilter)
        {
            ref var attackComponent = ref _atackFilter.Get1(entity);
            ref var movableComponent = ref _atackFilter.Get2(entity);
            ref var followComponent = ref _atackFilter.Get3(entity);

            var distance = Vector3.Distance(followComponent.Target.position, movableComponent.Transform.position);

            if (distance <= attackComponent.AttackDistance)
            {
                
            }

        }
    }
}
