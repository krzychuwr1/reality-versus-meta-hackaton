using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts;
using App.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

public class TileCollisionHandler : MonoBehaviour {
    [FormerlySerializedAs("_meshRenderer")]
    public MeshRenderer meshRenderer;
    public enum HitType {
        TeamVirtual,
        TeamReality,
    }

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnHit(HitType team) {
        meshRenderer.enabled = team == HitType.TeamReality;
        GameStateManager.Score += team == HitType.TeamVirtual ? 1 : -1;
    }
}
