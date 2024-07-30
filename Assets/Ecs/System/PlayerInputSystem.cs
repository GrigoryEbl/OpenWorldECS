using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private const string _horizontalAxis = "Horizontal";
        private const string _verticalAxis = "Vertical";

        private readonly EcsFilter<InputEventComponent> _inputEventFilter;

        public void Run()
        {
            var horizontal = Input.GetAxisRaw(_horizontalAxis);
            var vertical = Input.GetAxisRaw(_verticalAxis);

            foreach(var entity in _inputEventFilter)
            {
                ref var inputEnvent = ref _inputEventFilter.Get1(entity);
                inputEnvent.Direction = new Vector3(horizontal, 0, vertical);
            }
        }
    }
}
