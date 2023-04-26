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

    private void OnCollisionEnter(Collision collision) {
        //We need to take into account the current player's team and maybe check the
        //collider tag to see if it's a player's team's projectile or an opposing team's projectile
        AudioSource.PlayClipAtPoint(TileSetup.Instance.tileHitAudio, transform.position);
    }

    public void OnHit(HitType team) {
        meshRenderer.enabled = team == HitType.TeamReality;
        GameStateManager.Score += team == HitType.TeamVirtual ? 1 : -1;
    }
}
