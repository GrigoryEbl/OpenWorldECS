using Leopotam.Ecs;
using System.Runtime.InteropServices;
using UnityEngine;

public class FollowSystem : IEcsRunSystem
{
    private readonly EcsFilter<FollowComponent, MovableComponent, FindTargetComponent> _enemyFollowFilter;
    private readonly float _stopDistance = 2f;
    private readonly float _distanceVisibility = 12f;

    public void Run()
    {
        foreach (var entity in _enemyFollowFilter)
        {
            ref var followComponent = ref _enemyFollowFilter.Get1(entity);
            ref var movableComponent = ref _enemyFollowFilter.Get2(entity);
            ref var findTargetComponent = ref _enemyFollowFilter.Get3(entity);

            followComponent.Target = findTargetComponent.Target;

            if (followComponent.Target == null)
            {
                Debug.Log("Target null");
                continue;
            }

            var direction = (followComponent.Target.position - movableComponent.Transform.position).normalized;
            direction.y = 0;

            var distance = Vector3.Distance(followComponent.Target.position, movableComponent.Transform.position);

            movableComponent.IsMoving = distance > _stopDistance && distance < _distanceVisibility;

            if (movableComponent.IsMoving)
            {
                movableComponent.Transform.position += direction * (Time.deltaTime * movableComponent.Speed);
            }
        }
    }
}
