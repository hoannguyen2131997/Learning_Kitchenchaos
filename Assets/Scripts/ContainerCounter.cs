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
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectSOTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(_player);
        
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
