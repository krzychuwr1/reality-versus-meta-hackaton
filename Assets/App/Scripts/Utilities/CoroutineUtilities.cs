using System;
using System.Collections;
using UnityEngine;

namespace App.Scripts.Utilities
{
    public static class CoroutineUtilities
    {
        public static void ExecuteDelayed(this MonoBehaviour monoBehaviour, Action action, float time)
        {
            monoBehaviour.StartCoroutine(ExecuteDelayedInner(action, time));
        }

        private static IEnumerator ExecuteDelayedInner(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}