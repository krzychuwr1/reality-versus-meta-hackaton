using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisionHandler : MonoBehaviour {
    private MeshRenderer _meshRenderer;
    public enum HitType {
        TeamVirtual,
        TeamReality,
    }

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision) {
        //We need to take into account the current player's team and maybe check the
        //collider tag to see if it's a player's team's projectile or an opposing team's projectile
    }

    public void OnHit(HitType team) {
        _meshRenderer.enabled = team == HitType.TeamReality;
    }
}
