using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSo;
    }

    public void SetClearCounter(ClearCounter _clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        
        this.clearCounter = _clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }
        
        clearCounter.SetKitchenObject(this);
        
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
