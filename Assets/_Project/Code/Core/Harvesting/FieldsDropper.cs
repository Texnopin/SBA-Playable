using System.Collections.Generic;
using _Project.Code.Core.Items;
using _Project.Code.Services.ItemsCreation;
using UnityEngine;

namespace _Project.Code.Core.Harvesting
{
    public class FieldsDropper 
    {
        private readonly Dropper _dropper;
        
        public FieldsDropper(Dropper dropper, List<Field> fields)
        {
            _dropper = dropper;

            fields.ForEach(f => f.Harvested += () => OnHarvested(f.transform.position));
        }
        
        private void OnHarvested(Vector3 position) =>
            _dropper.CreateDrop(ItemType.Corn, position, 2);
    }
}