using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Tiles {
    public class PlaneTileManager : MonoBehaviour {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private int tileSize = 1;
        [SerializeField] private Transform tileRoot;
        [SerializeField] private Renderer tileRenderer;
        private Vector2 _roundedTileSize;
        public event Action<List<TileCollisionHandler>> OnTileCreationComplete;

        private void Awake() {
            var bounds = tileRenderer.localBounds;
            float x = bounds.size.x * transform.localScale.x;
            float y = bounds.size.y * transform.localScale.y;
            CreateTileArea(x, y);
        }

        //Create a tile area with a horizontal orientation
        public void CreateTileArea(float x, float y) {
            var tiles = new List<TileCollisionHandler>();

            //Fit tiles per axis
            float tilesInX = Mathf.Floor(x / tileSize);
            float tilesInY = Mathf.Floor(y / tileSize);

            //Scale the tiles to fit the x and y axis
            _roundedTileSize = new Vector2(x / tilesInX, y / tilesInY);
            var offset = new Vector3(x / 2, y / 2) - new Vector3(_roundedTileSize.x / 2, _roundedTileSize.y / 2);

            for (int i = 0; i < tilesInX; i++) {
                for (int j = 0; j < tilesInY; j++) {
                    var tileGo = Instantiate(tilePrefab);
                    //Convert offset to local space
                    var localOffset = transform.TransformDirection(offset);
                    tileGo.transform.localScale = new Vector3(_roundedTileSize.x, _roundedTileSize.y, 1);
                    tileGo.transform.localRotation = tileRoot.transform.rotation * Quaternion.Euler(0, 180, 0);
                    tileGo.transform.SetParent(tileRoot.transform, true);
                    var localPosition = tileGo.transform.localPosition;
                    localPosition = Vector3.zero;
                    var scaleOffset = tileGo.transform.localScale / 2;

                    //Set the tile position to bottom left corner
                    localPosition += new Vector3(-scaleOffset.x, scaleOffset.y, 0);

                    //Set the tile position to the correct position according to the current tile index
                    localPosition -= new Vector3(i * tileGo.transform.localScale.x, -j * tileGo.transform.localScale.y, 0);
                    tileGo.transform.localPosition = localPosition;
                    tiles.Add(tileGo.GetComponent<TileCollisionHandler>());
                }
            }
            
            OnTileCreationComplete?.Invoke(tiles);
        }
    }
}
