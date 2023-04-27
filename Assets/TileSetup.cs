using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.Tiles;
using UnityEngine;

public class TileSetup : MonoBehaviour {
    public static int TotalAmountOfTiles = 0;
    public static bool HasSetupTiles = false;
    public static List<TileCollisionHandler> TileCollisionHandlerList = new List<TileCollisionHandler>();
    public static TileSetup Instance;
    public AudioClip tileHitAudio;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        //Subscribe to the Scene Understanding event
        WorldGenerationController.onSceneGenerated.AddListener(OnSceneUnderstandingInitialized);
    }

    private void OnSceneUnderstandingInitialized() {
        WorldGenerationController.onSceneGenerated.RemoveListener(OnSceneUnderstandingInitialized);
        var planeTileManagers = FindObjectsOfType<PlaneTileManager>();
        var obstacleTileManagers = FindObjectsOfType<ObstacleTileManager>();
        var tileCollisionHandlersLists = new List<List<TileCollisionHandler>>();
        
        var numberOfTileManagers = planeTileManagers.Length + obstacleTileManagers.Length;
        foreach (var planeTileManager in planeTileManagers) {
            planeTileManager.OnTileCreationComplete += OnTileCreationComplete;
            planeTileManager.InitializeTiles();
        }
        
        foreach (var obstacleTileManager in obstacleTileManagers) {
            obstacleTileManager.OnTileCreationComplete += OnTileCreationComplete;
            obstacleTileManager.InitializeTiles();
        }
        
        void OnTileCreationComplete(List<TileCollisionHandler> tileCollisionHandlers) {
            tileCollisionHandlersLists.Add(tileCollisionHandlers);
            numberOfTileManagers--;
            if (numberOfTileManagers == 0) {
                //Unsubscribe from the Scene Understanding event
                foreach (var planeTileManager in planeTileManagers) {
                    planeTileManager.OnTileCreationComplete -= OnTileCreationComplete;
                }
                
                foreach (var obstacleTileManager in obstacleTileManagers) {
                    obstacleTileManager.OnTileCreationComplete -= OnTileCreationComplete;
                }

                var sceneObjects = WorldGenerationController.Instance.sceneObjects;
                //Get the combined bounds of all scene objects using their transform
                var sceneBounds = new Bounds();
                foreach (var sceneObject in sceneObjects) {
                    sceneBounds.Encapsulate(sceneObject.transform.position);
                }
                
                var sceneCenter = sceneBounds.center;

                foreach (var handlersLists in tileCollisionHandlersLists) {
                    TotalAmountOfTiles += handlersLists.Count;
                    TileCollisionHandlerList.AddRange(handlersLists);
                }

                StartCoroutine(CreateTiles());
                IEnumerator CreateTiles() {
                    //Get all plane tile managers to the left of the scene center on the x axis
                    foreach (var tileCollisionHandlersList in tileCollisionHandlersLists) {
                        foreach (var tileCollisionHandler in tileCollisionHandlersList) {
                            if (tileCollisionHandler.transform.position.x < sceneCenter.x) {
                                tileCollisionHandler.OnHit(TileCollisionHandler.HitType.TeamVirtual);
                            }

                            yield return new WaitForSeconds(0.01f);
                        }
                        
                        yield return new WaitForSeconds(0.01f);
                    }
                    HasSetupTiles = true;
                }
            }
        }
    }
}
