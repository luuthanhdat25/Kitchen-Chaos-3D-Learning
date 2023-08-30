using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [SerializeField] private Transform prefab;
    public Transform GetPrefab() => prefab;
    
    [SerializeField] private Sprite sprite;
    public Sprite GetSprite() => sprite;
    
    [SerializeField] private string objectName;
    public string GetObjectName() => objectName;
}
