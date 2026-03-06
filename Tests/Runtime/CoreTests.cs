using System;
using System.Collections.Generic;
using NUnit.Framework;
using AnkleBreaker.Core.MasterDelegates;
using AnkleBreaker.Core.MasterInterfaces;
using AnkleBreaker.Core.MasterStructs;
using AnkleBreaker.Core.MasterClasses;
using UnityEngine;

namespace AnkleBreaker.Core.Tests
{    
    #region Test Helpers

    // Concrete categories for testing
    public class TestCategoryBase : AnkleBreakerCategory { }
    public class TestCategoryChild : TestCategoryBase { }
    public class TestCategoryGrandChild : TestCategoryChild { }
    public class TestCategoryUnrelated : AnkleBreakerCategory { }

    #endregion

    [TestFixture]
    public class AssetIdentityStructTests
    {
        [Test]
        public void DefaultValues_AreNull()
        {
            var identity = new AssetIdentityStruct();
            Assert.IsNull(identity.TitleTerm);
            Assert.IsNull(identity.DescriptionTerm);
            Assert.IsNull(identity.Thumbnail);
        }

        [Test]
        public void Fields_CanBeAssigned()
        {
            var identity = new AssetIdentityStruct
            {
                TitleTerm = "test_title",
                DescriptionTerm = "test_desc"
            };
            Assert.AreEqual("test_title", identity.TitleTerm);
            Assert.AreEqual("test_desc", identity.DescriptionTerm);
        }
    }

    [TestFixture]
    public class ActionDelegateTests
    {
        [Test]
        public void ActionRef_SingleRef_ModifiesValue()
        {
            ActionRef<int> action = (ref int val) => { val = 42; };
            int x = 0;
            action(ref x);
            Assert.AreEqual(42, x);
        }

        [Test]
        public void ActionRef_TwoParams_ModifiesRefOnly()
        {
            ActionRef<string, int> action = (string s, ref int val) => { val = s.Length; };
            int result = 0;
            action("hello", ref result);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void ActionRefRef_BothModified()
        {
            ActionRefRef<int, int> action = (ref int a, ref int b) => { a = 10; b = 20; };
            int x = 0, y = 0;
            action(ref x, ref y);
            Assert.AreEqual(10, x);
            Assert.AreEqual(20, y);
        }

        [Test]
        public void ActionIn_ReadsValue()
        {
            int captured = 0;
            ActionIn<int> action = (in int val) => { captured = val; };
            action(in captured);
            // captured was 0 when passed in
            Assert.AreEqual(0, captured);
        }

        [Test]
        public void ActionInRef_CombinedUsage()
        {
            ActionInRef<int, int> action = (in int a, ref int b) => { b = a * 2; };
            int input = 5;
            int output = 0;
            action(in input, ref output);
            Assert.AreEqual(10, output);
        }
    }

    [TestFixture]
    public class InterfaceTests
    {
        [Test]
        public void IIsReady_InterfaceExists()
        {
            Assert.IsNotNull(typeof(IIsReady));
            Assert.IsTrue(typeof(IIsReady).IsInterface);
            var prop = typeof(IIsReady).GetProperty("IsLocallyReady");
            Assert.IsNotNull(prop);
            Assert.AreEqual(typeof(bool), prop.PropertyType);
        }

        [Test]
        public void IBehaviourBase_HasExpectedMembers()
        {
            Assert.IsTrue(typeof(IBehaviourBase).IsInterface);
            Assert.IsNotNull(typeof(IBehaviourBase).GetProperty("gameObject"));
            Assert.IsNotNull(typeof(IBehaviourBase).GetProperty("transform"));
            Assert.IsNotNull(typeof(IBehaviourBase).GetProperty("PrefabInstanceId"));
        }

        [Test]
        public void IAssetIdentitySO_HasExpectedMembers()
        {
            Assert.IsTrue(typeof(IAssetIdentitySO).IsInterface);
            Assert.IsNotNull(typeof(IAssetIdentitySO).GetProperty("name"));
            Assert.IsNotNull(typeof(IAssetIdentitySO).GetProperty("ID"));
            Assert.IsNotNull(typeof(IAssetIdentitySO).GetProperty("Categories"));
            Assert.IsNotNull(typeof(IAssetIdentitySO).GetProperty("IdentityPerLevel"));
        }
    }

    [TestFixture]
    public class AnkleBreakerCategoryExtensionsTests
    {
        private TestCategoryBase _baseCategory;
        private TestCategoryChild _childCategory;
        private TestCategoryGrandChild _grandChildCategory;
        private TestCategoryUnrelated _unrelatedCategory;

        [SetUp]
        public void SetUp()
        {
            _baseCategory = ScriptableObject.CreateInstance<TestCategoryBase>();
            _childCategory = ScriptableObject.CreateInstance<TestCategoryChild>();
            _grandChildCategory = ScriptableObject.CreateInstance<TestCategoryGrandChild>();
            _unrelatedCategory = ScriptableObject.CreateInstance<TestCategoryUnrelated>();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(_baseCategory);
            UnityEngine.Object.DestroyImmediate(_childCategory);
            UnityEngine.Object.DestroyImmediate(_grandChildCategory);
            UnityEngine.Object.DestroyImmediate(_unrelatedCategory);
        }

        // --- ImplementsType (single) ---

        [Test]
        public void ImplementsType_ExactMatch_ReturnsTrue()
        {
            Assert.IsTrue(_baseCategory.ImplementsType(typeof(TestCategoryBase)));
        }

        [Test]
        public void ImplementsType_ChildType_WithSubclass_ReturnsTrue()
        {
            Assert.IsTrue(_baseCategory.ImplementsType(typeof(TestCategoryChild), checkSubclass: true));
        }

        [Test]
        public void ImplementsType_ChildType_WithoutSubclass_ReturnsFalse()
        {
            Assert.IsFalse(_baseCategory.ImplementsType(typeof(TestCategoryChild), checkSubclass: false));
        }

        [Test]
        public void ImplementsType_UnrelatedType_ReturnsFalse()
        {
            Assert.IsFalse(_baseCategory.ImplementsType(typeof(TestCategoryUnrelated)));
        }

        [Test]
        public void ImplementsType_ParentType_ReturnsFalse()
        {
            // Child checking against parent type — parent is NOT subclass of child
            Assert.IsFalse(_childCategory.ImplementsType(typeof(TestCategoryBase), checkSubclass: true));
        }

        // --- ImplementsCategory (single to single) ---

        [Test]
        public void ImplementsCategory_SameInstance_ReturnsTrue()
        {
            Assert.IsTrue(_baseCategory.ImplementsCategory(_baseCategory));
        }

        [Test]
        public void ImplementsCategory_NullCategory_ReturnsFalse()
        {
            Assert.IsFalse(_baseCategory.ImplementsCategory((AnkleBreakerCategory)null));
        }

        // --- ImplementsCategory (collection) ---

        [Test]
        public void ImplementsCategory_Collection_ContainsMatch_ReturnsTrue()
        {
            var list = new List<AnkleBreakerCategory> { _baseCategory, _unrelatedCategory };
            Assert.IsTrue(list.ImplementsCategory(_childCategory, checkSubclass: true));
        }

        [Test]
        public void ImplementsCategory_Collection_NoMatch_ReturnsFalse()
        {
            var list = new List<AnkleBreakerCategory> { _childCategory };
            Assert.IsFalse(list.ImplementsCategory(_unrelatedCategory));
        }

        // --- MustContainsAllCategories ---

        [Test]
        public void ImplementsCategory_MustContainAll_AllPresent_ReturnsTrue()
        {
            var src = new List<AnkleBreakerCategory> { _baseCategory, _unrelatedCategory };
            var toCheck = new List<AnkleBreakerCategory> { _baseCategory, _unrelatedCategory };
            Assert.IsTrue(src.ImplementsCategory(toCheck, checkSubclass: false, mustContainsAllCategories: true));
        }

        [Test]
        public void ImplementsCategory_MustContainAll_OneMissing_ReturnsFalse()
        {
            var src = new List<AnkleBreakerCategory> { _baseCategory };
            var toCheck = new List<AnkleBreakerCategory> { _baseCategory, _unrelatedCategory };
            Assert.IsFalse(src.ImplementsCategory(toCheck, checkSubclass: false, mustContainsAllCategories: true));
        }

        [Test]
        public void ImplementsCategory_NullCollection_ReturnsFalse()
        {
            var src = new List<AnkleBreakerCategory> { _baseCategory };
            Assert.IsFalse(src.ImplementsCategory((IEnumerable<AnkleBreakerCategory>)null));
        }

        // --- ContainsCategory ---

        [Test]
        public void ContainsCategory_ExactMatch_ReturnsTrue()
        {
            var list = new List<AnkleBreakerCategory> { _baseCategory };
            Assert.IsTrue(list.ContainsCategory(_baseCategory));
        }

        [Test]
        public void ContainsCategory_SubclassDoesNotMatch()
        {
            // ContainsCategory uses checkSubclass=false
            var list = new List<AnkleBreakerCategory> { _baseCategory };
            Assert.IsFalse(list.ContainsCategory(_childCategory));
        }

        // --- GetInheritanceLevelsFrom ---

        [Test]
        public void GetInheritanceLevels_SameType_ReturnsZero()
        {
            Assert.AreEqual(0, _baseCategory.GetInheritanceLevelsFrom(_baseCategory));
        }

        [Test]
        public void GetInheritanceLevels_DirectChild_ReturnsOne()
        {
            Assert.AreEqual(1, _childCategory.GetInheritanceLevelsFrom(_baseCategory));
        }

        [Test]
        public void GetInheritanceLevels_GrandChild_ReturnsTwo()
        {
            Assert.AreEqual(2, _grandChildCategory.GetInheritanceLevelsFrom(_baseCategory));
        }

        [Test]
        public void GetInheritanceLevels_NotSubclass_ReturnsMinusOne()
        {
            Assert.AreEqual(-1, _unrelatedCategory.GetInheritanceLevelsFrom(_baseCategory));
        }

        [Test]
        public void GetInheritanceLevels_ParentFromChild_ReturnsMinusOne()
        {
            // Base is not a subclass of Child
            Assert.AreEqual(-1, _baseCategory.GetInheritanceLevelsFrom(_childCategory));
        }
    }
}
