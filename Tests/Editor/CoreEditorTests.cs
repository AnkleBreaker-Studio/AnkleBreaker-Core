using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using AnkleBreaker.Core.Editor;

namespace AnkleBreaker.Core.Editor.Tests
{
    // ───────────────────────────────────────────────
    //  DependenciesInstaller Tests (existing)
    // ───────────────────────────────────────────────

    [TestFixture]
    public class DependenciesInstallerTests
    {
        [Test]
        public void CheckAllDependencies_MethodExists()
        {
            var method = typeof(AnkleBreakerCoreDependenciesInstaller)
                .GetMethod("CheckAllDependencies", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "CheckAllDependencies should exist as public static");
        }

        [Test]
        public void InstallPackageAsync_MethodExists()
        {
            var method = typeof(AnkleBreakerCoreDependenciesInstaller)
                .GetMethod("InstallPackageAsync", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "InstallPackageAsync should exist as public static");
        }

        [Test]
        public void StartCoroutine_MethodExists()
        {
            var method = typeof(AnkleBreakerCoreDependenciesInstaller)
                .GetMethod("StartCoroutine", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "StartCoroutine should exist as public static");
        }

        [Test]
        public void DISMISSED_KEY_IsPublicConst()
        {
            var field = typeof(AnkleBreakerCoreDependenciesInstaller)
                .GetField("DISMISSED_KEY", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(field, "DISMISSED_KEY should be a public static field");
            Assert.AreEqual("AB_UtilsInspector_Dismissed", field.GetValue(null));
        }
    }

    [TestFixture]
    public class DependencyWindowTests
    {
        [Test]
        public void WindowClass_Exists()
        {
            Assert.IsNotNull(typeof(AnkleBreakerDependencyWindow));
            Assert.IsTrue(typeof(UnityEditor.EditorWindow).IsAssignableFrom(typeof(AnkleBreakerDependencyWindow)));
        }

        [Test]
        public void ShowWindow_MethodExists()
        {
            var method = typeof(AnkleBreakerDependencyWindow)
                .GetMethod("ShowWindow", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "ShowWindow should exist as public static");
        }
    }

    // ───────────────────────────────────────────────
    //  ABDefineAttribute Tests
    // ───────────────────────────────────────────────

    [TestFixture]
    public class ABDefineAttributeTests
    {
        [Test]
        public void Constructor_StoresProperties()
        {
            var attr = new ABDefineAttribute("AB_FMOD", "FMODUnity.RuntimeManager", "FMODUnity");

            Assert.AreEqual("AB_FMOD", attr.Define);
            Assert.AreEqual("FMODUnity.RuntimeManager", attr.TypeName);
            Assert.AreEqual("FMODUnity", attr.Assembly);
        }

        [Test]
        public void AttributeUsage_AllowsMultiple()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(ABDefineAttribute), typeof(AttributeUsageAttribute));

            Assert.IsNotNull(usage);
            Assert.IsTrue(usage.AllowMultiple, "ABDefineAttribute must allow multiple on same assembly");
        }

        [Test]
        public void AttributeUsage_TargetsAssembly()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(ABDefineAttribute), typeof(AttributeUsageAttribute));

            Assert.IsNotNull(usage);
            Assert.AreEqual(AttributeTargets.Assembly, usage.ValidOn);
        }

        [Test]
        public void IsSealed()
        {
            Assert.IsTrue(typeof(ABDefineAttribute).IsSealed,
                "ABDefineAttribute should be sealed");
        }

        [Test]
        public void InheritsFromAttribute()
        {
            Assert.IsTrue(typeof(Attribute).IsAssignableFrom(typeof(ABDefineAttribute)));
        }
    }

    // ───────────────────────────────────────────────
    //  ABDefineManager Tests
    // ───────────────────────────────────────────────

    [TestFixture]
    public class ABDefineManagerTests
    {
        [Test]
        public void Class_IsInternal()
        {
            var type = typeof(ABDefineAttribute).Assembly.GetType("AnkleBreaker.Core.Editor.ABDefineManager");
            Assert.IsNotNull(type, "ABDefineManager should exist in AnkleBreaker.Core.Editor");
            Assert.IsTrue(type.IsNotPublic, "ABDefineManager should be internal");
        }

        [Test]
        public void Class_IsStatic()
        {
            var type = typeof(ABDefineAttribute).Assembly.GetType("AnkleBreaker.Core.Editor.ABDefineManager");
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsAbstract && type.IsSealed, "ABDefineManager should be static (abstract + sealed)");
        }

        [Test]
        public void Class_HasInitializeOnLoadAttribute()
        {
            var type = typeof(ABDefineAttribute).Assembly.GetType("AnkleBreaker.Core.Editor.ABDefineManager");
            Assert.IsNotNull(type);
            var attr = type.GetCustomAttribute<UnityEditor.InitializeOnLoadAttribute>();
            Assert.IsNotNull(attr, "ABDefineManager should have [InitializeOnLoad]");
        }

        [Test]
        public void UpdateDefines_MethodExists()
        {
            var type = typeof(ABDefineAttribute).Assembly.GetType("AnkleBreaker.Core.Editor.ABDefineManager");
            Assert.IsNotNull(type);
            var method = type.GetMethod("UpdateDefines",
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            Assert.IsNotNull(method, "UpdateDefines should exist as a static method");
        }
    }
}
