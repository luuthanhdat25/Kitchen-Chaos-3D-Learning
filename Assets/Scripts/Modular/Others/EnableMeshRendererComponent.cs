using UnityEngine;

public class EnableMeshRendererComponent : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        if(this.meshRenderer != null) 
            meshRenderer.enabled = true;
    }
}
