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
        private const string UTILS_INSPECTOR_URL = "https://github.com/AnkleBreaker-Studio/AnkleBreaker-Utils-Inspector.git#Release";
        private const string UTILS_INSPECTOR_DISPLAY_NAME = "AnkleBreaker.Utils.Inspector";
        public const string DISMISSED_KEY = "AB_UtilsInspector_Dismissed";

        [InitializeOnLoadMethod]
        public static void CheckAllDependencies()
        {
#if !AB_UTILS_INSPECTOR
            if (!SessionState.GetBool(DISMISSED_KEY, false))
            {
                EditorApplication.delayCall += () =>
                {
                    AnkleBreakerDependencyWindow.ShowWindow();
                };
            }
#endif
        }

        [MenuItem("Help/AnkleBreaker/Core/Install Utils Inspector Package", priority = 0)]
        public static void InstallUtilsInspector()
        {
            StartCoroutine(InstallPackageAsync(
                UTILS_INSPECTOR_URL,
                "AB_UTILS_INSPECTOR-Install",
                UTILS_INSPECTOR_DISPLAY_NAME));
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

        public static IEnumerator InstallPackageAsync(string packageUrl, string sessionKey, string displayName)
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

        public static void StartCoroutine(IEnumerator handle)
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

    public class AnkleBreakerDependencyWindow : EditorWindow
    {
        private const string UTILS_INSPECTOR_URL = "https://github.com/AnkleBreaker-Studio/AnkleBreaker-Utils-Inspector.git#Release";
        private const string UTILS_INSPECTOR_DISPLAY_NAME = "AnkleBreaker.Utils.Inspector";

        private static GUIStyle _titleStyle;
        private static GUIStyle _messageStyle;
        private static GUIStyle _warningStyle;

        public static void ShowWindow()
        {
            var window = GetWindow<AnkleBreakerDependencyWindow>(true, "AnkleBreaker Core - Missing Dependency", true);
            window.minSize = new Vector2(500, 280);
            window.maxSize = new Vector2(500, 280);
            window.ShowUtility();
        }

        private void OnGUI()
        {
            InitStyles();

            EditorGUILayout.Space(15);

            EditorGUILayout.LabelField("Missing Recommended Package", _titleStyle);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField(
                "AnkleBreaker Core strongly recommends installing the Utils Inspector package.\n\n" +
                "Without it, some features will be disabled:\n" +
                "  - Custom inspector attributes (HideInNormalInspector, Button, etc.)\n" +
                "  - Enhanced editor tooling for AnkleBreaker components",
                _messageStyle);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField(
                "It is highly recommended to install this package for the best experience.",
                _warningStyle);

            EditorGUILayout.Space(15);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Install Utils Inspector", GUILayout.Width(200), GUILayout.Height(35)))
            {
                AnkleBreakerCoreDependenciesInstaller.StartCoroutine(
                    AnkleBreakerCoreDependenciesInstaller.InstallPackageAsync(
                        UTILS_INSPECTOR_URL,
                        "AB_UTILS_INSPECTOR-Install",
                        UTILS_INSPECTOR_DISPLAY_NAME));
                Close();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Remind Me Later", GUILayout.Width(140), GUILayout.Height(35)))
            {
                Close();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Don't Show Again (this session)", GUILayout.Width(250), GUILayout.Height(22)))
            {
                SessionState.SetBool(AnkleBreakerCoreDependenciesInstaller.DISMISSED_KEY, true);
                Close();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
        }

        private static void InitStyles()
        {
            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter
                };
            }

            if (_messageStyle == null)
            {
                _messageStyle = new GUIStyle(EditorStyles.wordWrappedLabel)
                {
                    fontSize = 12,
                    padding = new RectOffset(15, 15, 0, 0)
                };
            }

            if (_warningStyle == null)
            {
                _warningStyle = new GUIStyle(EditorStyles.wordWrappedLabel)
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                };
                _warningStyle.normal.textColor = new Color(1f, 0.6f, 0f);
            }
        }
    }
}