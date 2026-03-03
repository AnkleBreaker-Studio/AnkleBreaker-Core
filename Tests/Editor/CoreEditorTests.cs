using System.Reflection;
using NUnit.Framework;
using AnkleBreaker.Core.Editor;

namespace AnkleBreaker.Core.Editor.Tests
{
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
}
