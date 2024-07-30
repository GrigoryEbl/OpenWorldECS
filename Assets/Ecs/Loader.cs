using Ecs.Data;
using ECS.System;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private UnitInitData _playerData;
        [SerializeField] private UnitInitData _enemyData;
        [SerializeField] private Transform _spawmPoint;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.Add(new GameInitSystem(_playerData, _enemyData, _spawmPoint));
            _systems.Add(new PlayerInputSystem());
            _systems.Add(new PlayerMoveSystem());
            _systems.Add(new FollowSystem());
            _systems.Add(new AnimatedCharacterSystem());

            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}
