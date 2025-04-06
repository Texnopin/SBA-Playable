using System;
using _Project.Code.Core.Collision;
using UnityEngine;

namespace _Project.Code.Core.Harvesting
{
    public class Harvester : IDisposable
    {
        private readonly ICollisionDetector _detector;

        private Field _harvestingField;

        public Harvester(ICollisionDetector detector)
        {
            _detector = detector;

            _detector.Triggered += OnTriggered;
        }

        public void Dispose() => _detector.Triggered -= OnTriggered;

        private bool IsHarvesting => _harvestingField != null;
        
        private void OnTriggered(Collider2D collider, CollisionType type)
        {
            if (!collider.TryGetComponent(out Field field)) return;
            
            if (type == CollisionType.Exit && _harvestingField == field)
            {
                field.Abort();
                
                _harvestingField.Harvested -= Cleanup;
                
                Cleanup();
            }
            else if (!IsHarvesting && field.CanHarvest)
            {
                field.Harvest();
                
                _harvestingField = field;
                
                _harvestingField.Harvested += Cleanup;
            }
        }

        private void Cleanup()
        {
            _harvestingField = null;
        }
    }
}