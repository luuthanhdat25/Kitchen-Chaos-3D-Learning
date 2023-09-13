using System;
using KitchenObjects.Counter;
using UnityEngine;

namespace DefaultNamespace.Modular
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            CuttingCounter.ResetStaticData();
            BaseCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}