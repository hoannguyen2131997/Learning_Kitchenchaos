using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player _player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (_player.HasKitchenObject())
            {
                // Player is carrying something
                _player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a KitchnObject here
            if (_player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(_player);
            }
        }
    }

    public override void InteractAlternate(Player _player)
    {
        if (HasKitchenObject())
        {
            // There is a KitchenObject here
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }

    public override void TestInteract(Player _player)
    {
        if (HasKitchenObject())
        {
            Debug.Log("Hoan - Test Interact");
        }
    }
}
