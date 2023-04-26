using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Plane = Common.Plane;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    public class SceneFileCaptureStrategy : SceneCaptureStrategy
    {
        [SerializeField]
        WorldGenerationController worldGenerationController;
        [SerializeField]
        string debugScenePath;

        public override void CaptureScene(Action<Scene> onComplete)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            var fileinfo = new FileInfo(debugScenePath);
            if(fileinfo.Exists) {
                using (StreamReader reader = new StreamReader(fileinfo.FullName, false))
                {
                    var json = reader.ReadToEnd();
                    Scene deserializedScene = JsonUtility.FromJson<Scene>(json);
                    if (deserializedScene != null)
                        SampleController.Instance.Log("deserializedScene num walls: " + deserializedScene.walls.Length);
                    else
                        SampleController.Instance.Log("deserializedScene is NULL");

                    if (worldGenerationController)
                        worldGenerationController.GenerateWorld(deserializedScene);
                }
            } else {
                Debug.LogError("Scene file not found: " + debugScenePath);
            }
        }
    }
}
