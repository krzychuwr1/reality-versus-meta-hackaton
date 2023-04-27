using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Tiles {
    public class PlaneTileManager : MonoBehaviour {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private float tileSize = 0.25f;
        [SerializeField] private Transform tileRoot;
        [SerializeField] private Renderer tileRenderer;
        private Vector2 _roundedTileSize;
        public event Action<List<TileCollisionHandler>> OnTileCreationComplete;

        public void InitializeTiles() {
            var bounds = tileRenderer.localBounds;
            float x = bounds.size.x * transform.localScale.x;
            float y = bounds.size.y * transform.localScale.y;
            CreateTileArea(x, y);
        }

        //Create a tile area with a horizontal orientation
        public void CreateTileArea(float x, float y) {
            var tiles = new List<TileCollisionHandler>();

            //Fit tiles per axis
            float tilesInX = Mathf.Clamp(Mathf.Floor(x / tileSize), 1, Mathf.Infinity);
            float tilesInY = Mathf.Clamp(Mathf.Floor(y / tileSize), 1, Mathf.Infinity);

            //Scale the tiles to fit the x and y axis
            _roundedTileSize = new Vector2(x / tilesInX, y / tilesInY);

            for (int i = 0; i < tilesInX; i++) {
                for (int j = 0; j < tilesInY; j++) {
                    var tileGo = Instantiate(tilePrefab);
                    //Convert offset to local space
                    tileGo.transform.localScale = new Vector3(_roundedTileSize.x, _roundedTileSize.y, 1);
                    tileGo.transform.localRotation = tileRoot.transform.rotation * Quaternion.Euler(0, 180, 0);
                    tileGo.transform.SetParent(tileRoot.transform, true);
                    var localPosition = Vector3.zero;
                    var localScale = tileGo.transform.localScale;
                    var scaleOffset = localScale / 2;

                    //Set the tile position to bottom left corner
                    localPosition += new Vector3(-scaleOffset.x, scaleOffset.y, 0);

                    //Set the tile position to the correct position according to the current tile index
                    localPosition -= new Vector3(i * localScale.x, -j * localScale.y, 0);
                    tileGo.transform.localPosition = localPosition;
                    tiles.Add(tileGo.GetComponent<TileCollisionHandler>());
                }
            }

            OnTileCreationComplete?.Invoke(tiles);
        }
    }
}
