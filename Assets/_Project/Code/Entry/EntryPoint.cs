using System.Collections.Generic;
using System.Linq;
using _Project.Code.Core.Character;
using _Project.Code.Core.Harvesting;
using _Project.Code.Core.Items;
using _Project.Code.Services.CoroutineRunner;
using _Project.Code.Services.Input;
using _Project.Code.Services.ItemsCreation;
using _Project.Code.Services.ServiceLocator;
using Assets.CourseGame.Develop.DI;
using UnityEngine;

namespace _Project.Code.Entry
{
    [RequireComponent(typeof(CoroutineRunner))]
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<Item> _itemPrefabs;

        private CoroutineRunner _coroutineRunner;

        private void Awake()
        {
            L.Container = new DIContainer();

            L.Reg(_ => GetComponent<CoroutineRunner>());
            L.Reg(_ => new MoveInput(_joystick));

            L.Reg(_ => new ItemsCreator(_itemPrefabs));
            L.Reg(c => new Dropper(c.Resolve<ItemsCreator>()));
            L.Reg(c => new FieldsDropper(c.Resolve<Dropper>(), GetFields())).NonLazy();

            L.Container.Initialize();
        }

        private void OnDestroy()
        {
            L.Container.Dispose();
            L.Container = null;
        }

        private List<Field> GetFields() => FindObjectsOfType<Field>().ToList();
    }
}