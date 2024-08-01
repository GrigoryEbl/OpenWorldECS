using Leopotam.Ecs;
using UnityEngine;

public class FindTargetSystem : IEcsRunSystem
{
    private readonly EcsFilter<FindTargetComponent,MovableComponent> _findTargetFilter;

    public void Run() 
    {
        foreach (var entity in _findTargetFilter)
        {
            ref var findTargetComponent = ref _findTargetFilter.Get1(entity);
            ref var movableComponent = ref _findTargetFilter.Get2(entity);

            findTargetComponent.ScanPoint = movableComponent.Transform.position;

            if (findTargetComponent.Target == null)
                Find(ref findTargetComponent);
        }
    }

    private void Find(ref FindTargetComponent findTargetComponent)
    {
        Collider[] colliders = Physics.OverlapSphere(findTargetComponent.ScanPoint, findTargetComponent.ScanRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Player player))
            {
                findTargetComponent.Target = player.transform;
                Debug.Log("Target Finded: " + findTargetComponent.Target);
                return;
            }
        }
    }
}