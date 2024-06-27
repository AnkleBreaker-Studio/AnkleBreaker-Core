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
            StartCoroutine(CheckAndInstallAllDependenciesCoroutine());
        }

        private static IEnumerator CheckAndInstallAllDependenciesCoroutine()
        {
            var listProc = Client.List();

            while (!listProc.IsCompleted)
                yield return null;

#if !AB_UTILS
            yield return null;
            AddRequest sysProc = null;

            if (!SessionState.GetBool("AB_UTILS-Install", false))
            {
                SessionState.SetBool("AB_UTILS-Install", true);
                sysProc = Client.Add("https://github.com/AnkleBreaker-Studio/AnkleBreaker-Utils.git#Release");
            }
            else
            {
                Debug.Log("AnkleBreaker.Utils checking dependency is already in progress...");
                yield break;
            }

            if (sysProc.Status == StatusCode.InProgress)
            {
                Debug.Log("Installing AnkleBreaker.Utils ...");
                while (sysProc.Status == StatusCode.InProgress)
                {
                    yield return null;
                }
            }

            if (sysProc.Status == StatusCode.Failure)
                Debug.LogError("PackageManager's AnkleBreaker.Utils install failed, Error Message: " + sysProc.Error.message);
            else if (sysProc.Status == StatusCode.Success)
                Debug.Log("AnkleBreaker.Utils " + sysProc.Result.version + " installation complete");

            SessionState.SetBool("AB_UTILS-Install", false);
#endif
        }

        [MenuItem("Help/AnkleBreaker/Core/Update All Requirements (Package Manager)", priority = 0)]
        public static void InstallRequirements()
        {
            if (!SessionState.GetBool("AB_UTILS-Install", false))
            {
                StartCoroutine(InstallAnkleBreakerUtils());
            }
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

        private static IEnumerator InstallAnkleBreakerUtils()
        {
            yield return null;
            AddRequest sysProc = null;

            if (!SessionState.GetBool("AB_UTILS-Install", false))
            {
                SessionState.SetBool("AB_UTILS-Install", true);
                sysProc = Client.Add("https://github.com/AnkleBreaker-Studio/AnkleBreaker-Utils.git#Release");
            }
            else
            {
                Debug.Log("AnkleBreaker.Utils checking dependency is already in progress...");
                yield break;
            }

            if (sysProc.Status == StatusCode.InProgress)
            {
                Debug.Log("Installing AnkleBreaker.Utils ...");
                while (sysProc.Status == StatusCode.InProgress)
                {
                    yield return null;
                }
            }

            if (sysProc.Status == StatusCode.Failure)
                Debug.LogError("PackageManager's AnkleBreaker.Utils install failed, Error Message: " + sysProc.Error.message);
            else if (sysProc.Status == StatusCode.Success)
                Debug.Log("AnkleBreaker.Utils " + sysProc.Result.version + " installation complete");

            SessionState.SetBool("AB_UTILS-Install", false);
        }

        private static List<IEnumerator> coroutines;

        private static void StartCoroutine(IEnumerator handle)
        {
            if (coroutines == null)
            {
                EditorApplication.update -= EditorUpdate;
                EditorApplication.update += EditorUpdate;
                coroutines = new List<IEnumerator>();
            }

            coroutines.Add(handle);
        }

        private static void EditorUpdate()
        {
            List<IEnumerator> done = new List<IEnumerator>();

            if (coroutines != null)
            {
                foreach (var e in coroutines)
                {
                    if (!e.MoveNext())
                        done.Add(e);
                    else
                    {
                        if (e.Current != null)
                            Debug.Log(e.Current.ToString());
                    }
                }
            }

            foreach (var d in done)
                coroutines.Remove(d);
        }
    }
}