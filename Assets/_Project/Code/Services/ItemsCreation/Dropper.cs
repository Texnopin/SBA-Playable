using System;
using System.Collections.Generic;
using _Project.Code.Core.Items;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Services.ItemsCreation
{
    public class Dropper
    {
        private readonly ItemsCreator _creator;

        public Dropper(ItemsCreator creator)
        {
            _creator = creator;
        }

        public void CreateDrop(ItemType type, Vector3 position, int count = 4)
        {
            var items = new List<Item>(count);

            for (int i = 0; i < count; i++)
            {
                var item = _creator.Create(type);
                
                item.transform.position = position;
                item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                item.enabled = false;

                items.Add(item);
            }

            Animate(items, () =>
                items.ForEach(i => i.enabled = true));
        }

        private void Animate(IEnumerable<Item> items, Action onCompleted = null)
        {
            var duration = 0.3f;
            var radius = 0.3f;
            var power = 0.3f;
            var jumps = 1;

            var sequence = DOTween.Sequence();

            foreach (var item in items)
            {
                Vector3 pointOnCircle = UnityEngine.Random.insideUnitCircle.normalized;

                Vector3 targetPosition = item.transform.position + pointOnCircle * radius;

                sequence.Join(item.transform
                    .DOJump(targetPosition, power, jumps, duration));
            }

            sequence.OnComplete(() => onCompleted?.Invoke()).Play();
        }
    }
}