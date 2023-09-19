using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject; 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player _player)
    {
        if (!_player.HasKitchenObject())
        {
            // Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, _player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
