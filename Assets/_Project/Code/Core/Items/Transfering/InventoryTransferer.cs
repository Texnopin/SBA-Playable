using System;
using System.Collections;
using System.Linq;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Services.CoroutineRunner;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Items.Transfering
{
    public class InventoryTransferer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly TransferFactory _transferFactory;
        private Transfer _connection;

        private bool HasConnection => Connection != null;

        private Transfer Connection
        {
            get => _connection;
            set
            {
                _connection = value;

                if (value == null)
                    OnDisconnected();
                else
                    OnConnected();
            }
        }

        public InventoryTransferer(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _transferFactory = new TransferFactory();
        }

        public bool HasConnectionFor(ItemInventory inventory)
        {
            if (!HasConnection) return false;

            return Connection.Sender == inventory || Connection.Receiver == inventory;
        }

        public bool TrySetConnection(ItemInventory storage1, ItemInventory storage2)
        {
            if (HasConnection) return false;

            if (_transferFactory.TryCreate(storage1, storage2, out var connection) == false)
                return false;

            Connection = connection;

            return true;
        }

        public void Disconnect()
        {
            if (!HasConnection)
                throw new InvalidOperationException("No connection set");

            Connection = null;
        }

        private IEnumerator TransferingAll(ItemInventory from, ItemInventory to)
        {
            while (HasConnection)
            {
                if (CanTransfer(from, to) == false)
                {
                    Disconnect();
                    yield break;
                }

                var collectable = from.Get();
                collectable.transform.parent = null;

                yield return collectable.transform.DOMove(to.transform.position, 0.1f).Play().WaitForCompletion();

                to.Add(collectable);
                Debug.Log("Transfered unit");
                
                yield return null;
            }
        }

        private bool CanTransfer(ItemInventory from, ItemInventory to) => from.CanGet() && to.CanAdd();

        private void OnDisconnected()
        {
            Debug.Log("Disconnected");
        }

        private void OnConnected()
        {
            Debug.Log("Connected");
            _coroutineRunner.Launch(TransferingAll(Connection.Sender, Connection.Receiver));
        }
    }
}