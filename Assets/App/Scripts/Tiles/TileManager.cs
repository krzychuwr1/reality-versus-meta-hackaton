using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Tiles {
    public struct TileArea {
        public Orientation Orientation;
        public Vector3 WorldOrigin;
        public List<Tile> Tiles;
    }

    public enum Orientation {
        Vertical,
        Horizontal
    }

    public class TileManager : MonoBehaviour {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private int tileSize = 1;
        [SerializeField] private Transform tileRoot;
        private Vector2 _roundedTileSize;
        private List<TileArea> _tileAreas = new List<TileArea>();

        private void Awake() {
            //Example usage
            /*
            _tileAreas.Add(CreateTileVertical(new Vector3(0, 0, 0), 10, 10));
            _tileAreas.Add(CreateTileHorizontal(new Vector3(0, 0, 0), 10, 10));
            */
        }

        //Create a tile area with a horizontal orientation
        public TileArea CreateTileHorizontal(Vector3 worldCenter, float x, float y) {
            var tileArea = new TileArea();
            var tiles = new List<Tile>();

            //Fit tiles per axis
            float tilesInX = Mathf.Floor(x / tileSize);
            float tilesInY = Mathf.Floor(y / tileSize);

            //Scale the tiles to fit the x and y axis
            _roundedTileSize = new Vector2(x / tilesInX, y / tilesInY);
            worldCenter -= new Vector3(tilesInX / 2 + _roundedTileSize.x / 2, _roundedTileSize.y, tilesInY / 2 + _roundedTileSize.y / 2);

            //Create the tiles and add them to the dictionary
            for (int i = 0; i < tilesInX; i++) {
                for (int j = 0; j < tilesInY; j++) {
                    var position = new Vector3(worldCenter.x + _roundedTileSize.x * i, worldCenter.y, worldCenter.z + _roundedTileSize.y * j);
                    var scale = new Vector3(_roundedTileSize.x, _roundedTileSize.y, 1);
                    var rotation = new Vector3(90, 0, 0);
                    var tileGo = Instantiate(tilePrefab, position, Quaternion.Euler(rotation));
                    tileGo.transform.SetParent(tileRoot, true);
                    var tile = new Tile(tileGo, position, scale, rotation);
                    tiles.Add(tile);
                }
            }

            tileArea.Orientation = Orientation.Horizontal;
            tileArea.WorldOrigin = worldCenter;
            tileArea.Tiles = tiles;

            return tileArea;
        }

        //Create a tile area with a vertical orientation
        public TileArea CreateTileVertical(Vector3 worldCenter, float x, float y) {
            var tileArea = new TileArea();
            var tiles = new List<Tile>();

            //Fit tiles per axis
            float tilesInX = Mathf.Floor(x / tileSize);
            float tilesInY = Mathf.Floor(y / tileSize);

            //Scale the tiles to fit the x and y axis
            _roundedTileSize = new Vector2(x / tilesInX, y / tilesInY);
            worldCenter -= new Vector3(tilesInX / 2 + _roundedTileSize.x / 2, tilesInY / 2 + _roundedTileSize.y / 2, _roundedTileSize.x);

            //Create the tiles and add them to the dictionary
            for (int i = 0; i < tilesInX; i++) {
                for (int j = 0; j < tilesInY; j++) {
                    var position = new Vector3(worldCenter.x + _roundedTileSize.x * i, worldCenter.y + _roundedTileSize.y * j, worldCenter.z);
                    var scale = new Vector3(_roundedTileSize.x, _roundedTileSize.y, 1);
                    var rotation = new Vector3(0, 0, 0);
                    var tileGo = Instantiate(tilePrefab, position, Quaternion.Euler(rotation));
                    tileGo.transform.SetParent(tileRoot, true);
                    var tile = new Tile(tileGo, position, scale, rotation);
                    tiles.Add(tile);
                }
            }

            tileArea.Orientation = Orientation.Vertical;
            tileArea.WorldOrigin = worldCenter;
            tileArea.Tiles = tiles;

            return tileArea;
        }
    }
}
