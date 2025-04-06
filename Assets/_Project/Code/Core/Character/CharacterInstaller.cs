using _Project.Code.Core.Collision;
using _Project.Code.Services.CoroutineRunner;
using _Project.Code.Services.Input;
using UnityEngine;
using System.Collections.Generic;
using _Project.Code.Core.Harvesting;
using _Project.Code.Core.Items.Handlers;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Core.Items.Transfering;
using _Project.Code.Services.ServiceLocator;

namespace _Project.Code.Core.Character
{
    public class CharacterInstaller : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private OverlapCollisionDetector _castingDetector;
        [SerializeField] private CharacterAnimator _animator;

        [SerializeField] private List<ItemInventory> _storages;

        public void Start()
        {
            var input = L.Resolve<MoveInput>();
            var coroutineRunner = L.Resolve<CoroutineRunner>();
            
            _mover.Init(input);
            
            var provider = new InventoryProvider(_storages);
            
            var harvester = new Harvester(_castingDetector);
            
            var itemTransferer = new ItemTransferer();
            var inventoryTransferer = new InventoryTransferer(coroutineRunner);

            var itemHandler = new ItemCollisionHandler(_castingDetector, provider, itemTransferer);
            var inventoryHandler = new InventoryCollisionHandler(_castingDetector, provider, inventoryTransferer);
        }

        private void Update() => _animator.Direction = _mover.Direction;
    }
}