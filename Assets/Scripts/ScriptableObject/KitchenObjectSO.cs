using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [SerializeField] private Transform prefab;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string objectName;
    
    public Transform GetPrefab() => prefab;
    public Sprite GetSprite() => sprite;
    public string GetObjectName() => objectName;
}
