using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace AnkleBreaker.Core.Editor
{
    /// <summary>
    /// Scans all loaded assemblies for [ABDefine] attributes and sets/removes
    /// scripting define symbols based on detected plugins.
    /// Runs once per domain reload. Near-zero cost when nothing changes.
    /// </summary>
    [InitializeOnLoad]
    internal static class ABDefineManager
    {
        static ABDefineManager()
        {
            UpdateDefines();
        }

        internal static void UpdateDefines()
        {
            // 1. Collect all ABDefine declarations from all assemblies
            var mappings = new Dictionary<string, string>(); // define → "TypeName, Assembly"
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                ABDefineAttribute[] attrs;
                try
                {
                    attrs = (ABDefineAttribute[])assemblies[i]
                        .GetCustomAttributes(typeof(ABDefineAttribute), false);
                }
                catch
                {
                    continue; // Skip dynamic/broken assemblies
                }

                for (int j = 0; j < attrs.Length; j++)
                {
                    var attr = attrs[j];
                    string incoming = attr.TypeName + ", " + attr.Assembly;

                    if (!mappings.ContainsKey(attr.Define))
                    {
                        mappings[attr.Define] = incoming;
                    }
                    else
                    {
                        string existing = mappings[attr.Define];
                        if (existing != incoming)
                        {
                            Debug.LogWarning(
                                $"[ABDefineManager] Conflict for define '{attr.Define}': " +
                                $"already mapped to '{existing}', ignoring '{incoming}' " +
                                $"from assembly '{assemblies[i].GetName().Name}'. " +
                                $"Convention: always use the same canonical type for a given define (see AB_XXX naming rules).");
                        }
                    }
                }
            }

            if (mappings.Count == 0)
                return;

            // 2. Read current defines
            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            if (targetGroup == BuildTargetGroup.Unknown)
                targetGroup = BuildTargetGroup.Standalone;

            var namedTarget = NamedBuildTarget.FromBuildTargetGroup(targetGroup);
            PlayerSettings.GetScriptingDefineSymbols(namedTarget, out string[] currentDefines);
            var defineSet = new HashSet<string>(currentDefines);

            bool changed = false;

            // 3. Check each mapping with O(1) Type.GetType lookup
            foreach (var kvp in mappings)
            {
                bool pluginPresent = Type.GetType(kvp.Value) != null;
                bool definePresent = defineSet.Contains(kvp.Key);

                if (pluginPresent && !definePresent)
                {
                    defineSet.Add(kvp.Key);
                    changed = true;
                }
                else if (!pluginPresent && definePresent)
                {
                    defineSet.Remove(kvp.Key);
                    changed = true;
                }
            }

            // 4. Only write if something changed — avoids triggering a recompile
            if (changed)
            {
                var result = new string[defineSet.Count];
                defineSet.CopyTo(result);
                PlayerSettings.SetScriptingDefineSymbols(namedTarget, result);
            }
        }
    }
}
