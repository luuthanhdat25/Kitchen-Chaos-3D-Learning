using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu()]
    //Default setting
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}