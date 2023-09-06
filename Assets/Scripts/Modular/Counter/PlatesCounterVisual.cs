using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform platesVisualPrefab;
        private List<GameObject> platesVisualGameObjectList;

        private void Awake()
        {
            platesVisualGameObjectList = new List<GameObject>();
        }

        private void Start() => SubscribeEvent();
        
        private void SubscribeEvent()
        {
            platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }

        private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
        {
            Transform newPlate = Instantiate(platesVisualPrefab, counterTopPoint);

            float plateOffset = 0.1f;
            newPlate.localPosition = new Vector3(0, plateOffset * platesVisualGameObjectList.Count, 0);
            platesVisualGameObjectList.Add(newPlate.gameObject);
        }

        private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
        {
            GameObject removedObject = platesVisualGameObjectList[^1];
            platesVisualGameObjectList.Remove(removedObject);
            Destroy(removedObject);
        }
    }
}