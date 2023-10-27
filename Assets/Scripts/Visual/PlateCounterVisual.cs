using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    
    private int countPlateVisual;
    private float plateOffsetY;
    //private List<GameObject> plateVisualGameObjectList;
    /*
      private void Awake()
        {
            plateVisualGameObjectList = new List<GameObject>();
        }
     */

    private void Start()
    {
        platesCounter.OnPlaterSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        countPlateVisual = PlatesObjectPool.Instance.CountPlates - 1;
        plateOffsetY = .1f;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        /*
         GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
         */
        plateOffsetY -= .1f;
        countPlateVisual++;
        PlatesObjectPool.Instance.PushPlateItem(countPlateVisual);
    }

    // Use pool pattern
    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisual = PlatesObjectPool.Instance.GetPlateItem(countPlateVisual);
        plateVisual.gameObject.SetActive(true);
        plateVisual.transform.SetParent(counterTopPoint);
        plateVisual.transform.localPosition = new Vector3(0, plateOffsetY, 0);
        plateOffsetY += .1f;
        countPlateVisual--;
    }
    
    // Old
    /*
        private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
        {
            Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
                
            float plateOffsetY = .1f;
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
            
            plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
        }
     */
}
