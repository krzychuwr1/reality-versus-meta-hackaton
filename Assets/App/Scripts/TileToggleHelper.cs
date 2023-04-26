using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileToggleHelper : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start() {
        if(meshRenderer == null) {
            meshRenderer = GetComponent<MeshRenderer>();
        }
    }
    public void Toggle() {
        meshRenderer.enabled = !meshRenderer.enabled;
    }
}
