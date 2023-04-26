using UnityEngine;
namespace App.Scripts.Tiles {
    public struct Tile {
        public Vector3 Position;
        public Vector3 Scale;
        public Vector3 Rotation;
        public GameObject tile;
        
        public Tile(GameObject tile, Vector3 position, Vector3 scale, Vector3 rotation) {
            this.tile = tile;
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
    }
}
