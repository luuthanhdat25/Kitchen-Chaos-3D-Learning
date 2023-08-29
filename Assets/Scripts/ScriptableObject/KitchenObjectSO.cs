using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [SerializeField] private Transform prefab;
    public Transform Prefab => prefab;
    
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    
    [SerializeField] private string objectName;
    public string ObjectName => objectName;
}
