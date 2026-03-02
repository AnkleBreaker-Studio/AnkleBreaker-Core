using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace AnkleBreaker.Core.Editor
{
    public class AnkleBreakerCoreDependenciesInstaller
    {
        [InitializeOnLoadMethod]
        public static void CheckAllDependencies()
        {
#if !AB_UTILS_INSPECTOR
            StartCoroutine(InstallPackageAsync(
                "https://github.com/AnkleBreaker-Studio/utils-inspector.git#Release",
                "AB_UTILS_INSPECTOR-Install",
                "AnkleBreaker.Utils.Inspector"));
#endif
        }

        [MenuItem("Help/AnkleBreaker/Core/Update All Requirements (Package Manager)", priority = 0)]
        public static void InstallRequirements()
        {
            StartCoroutine(InstallPackageAsync(
                "https://github.com/AnkleBreaker-Studio/utils-inspector.git#Release",
                "AB_UTILS_INSPECTOR-Install",
                "AnkleBreaker.Utils.Inspector"));
        }

        [MenuItem("Help/AnkleBreaker/Core/Documentation", priority = 3)]
        public static void Documentation()
        {
            //Application.OpenURL("https://ANKLEBREAKERDOC");
        }

        [MenuItem("Help/AnkleBreaker/Support", priority = 4)]
        public static void Support()
        {
            //Application.OpenURL("https://discord.gg/ANKLEBREAKERASSETSUPPORT");
        }

        private static IEnumerator InstallPackageAsync(string packageUrl, string sessionKey, string displayName)
        {
            yield return null;

            if (SessionState.GetBool(sessionKey, false))
            {
                Debug.Log($"{displayName} install is already in progress...");
                yield break;
            }

            SessionState.SetBool(sessionKey, true);
            AddRequest addRequest = Client.Add(packageUrl);

            Debug.Log($"Installing {displayName} ...");
            while (addRequest.Status == StatusCode.InProgress)
                yield return null;

            if (addRequest.Status == StatusCode.Failure)
                Debug.LogError($"PackageManager's {displayName} install failed, Error Message: {addRequest.Error.message}");
            else if (addRequest.Status == StatusCode.Success)
                Debug.Log($"{displayName} {addRequest.Result.version} installation complete");

            SessionState.SetBool(sessionKey, false);
        }

        #region Editor Coroutine System
        private static List<IEnumerator> _coroutines;

        private static void StartCoroutine(IEnumerator handle)
        {
            if (_coroutines == null)
            {
                _coroutines = new List<IEnumerator>();
                EditorApplication.update += EditorUpdate;
            }

            _coroutines.Add(handle);
        }

        private static void EditorUpdate()
        {
            for (int i = _coroutines.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (!_coroutines[i].MoveNext())
                        _coroutines.RemoveAt(i);
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                    _coroutines.RemoveAt(i);
                }
            }

            if (_coroutines.Count == 0)
            {
                EditorApplication.update -= EditorUpdate;
                _coroutines = null;
            }
        }
        #endregion
    }
}