using System.Collections;
using System.Collections.Generic;
using App.Scripts;
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
    public void Toggle(bool type) {
        meshRenderer.enabled = type;
        AudioSource.PlayClipAtPoint(AudioManager.Instance.tileHitAudio, transform.position);
        GameStateManager.Score += type ? -1 : 1;
    }
}
