using System;

namespace AnkleBreaker.Core.Editor
{
    /// <summary>
    /// Declares a scripting define symbol that should be set when a specific type is detected.
    /// Place on assembly: [assembly: ABDefine("AB_FMOD", "FMODUnity.RuntimeManager", "FMODUnity")]
    ///
    /// Convention: All defines use the AB_ prefix followed by UPPER_SNAKE_CASE plugin name.
    /// Known defines:
    ///   AB_WWISE       → AkInitializer, AK.Wwise.Unity.MonoBehaviour
    ///   AB_FMOD        → FMODUnity.RuntimeManager, FMODUnity
    ///   AB_I2_LOCALIZE → I2.Loc.LocalizationManager, I2.Loc
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ABDefineAttribute : Attribute
    {
        public string Define   { get; }
        public string TypeName { get; }
        public string Assembly { get; }

        /// <param name="define">Scripting define symbol (ex: "AB_FMOD")</param>
        /// <param name="typeName">Fully qualified type name to detect (ex: "FMODUnity.RuntimeManager")</param>
        /// <param name="assembly">Assembly name containing the type (ex: "FMODUnity")</param>
        public ABDefineAttribute(string define, string typeName, string assembly)
        {
            Define   = define;
            TypeName = typeName;
            Assembly = assembly;
        }
    }
}
