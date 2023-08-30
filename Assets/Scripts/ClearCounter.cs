using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform targetTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
                //Debug.Log(kitchenObject.GetClearCounter());
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, targetTopPoint);
            kitchenObjectSOTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return targetTopPoint;
    }
    
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        this.kitchenObject = _kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
