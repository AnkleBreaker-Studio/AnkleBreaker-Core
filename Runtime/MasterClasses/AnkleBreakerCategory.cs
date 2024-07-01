#if ODIN_INSPECTOR
using BaseScriptableObject = Sirenix.OdinInspector.SerializedScriptableObject;
#else
using BaseScriptableObject = UnityEngine.ScriptableObject;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnkleBreaker.Core.MasterClasses
{
    [Serializable]
    public abstract class AnkleBreakerCategory : BaseScriptableObject
    {
        public Sprite CategoryIcon;
    }

    public static class AnkleBreakerCategory_Extensions
    {
        public static bool ImplementsCategory(this IEnumerable<AnkleBreakerCategory> srcCategories, IEnumerable<AnkleBreakerCategory> categoriesToCheck,
            bool checkSubclass = true, bool mustContainsAllCategories = false)
        {
            if (categoriesToCheck == null) return false;

            if (mustContainsAllCategories)
            {
                foreach (AnkleBreakerCategory categoryToCheck in categoriesToCheck)
                    if (srcCategories.ImplementsCategory(categoryToCheck, checkSubclass) == false)
                        return false;
            }

            else
            {
                foreach (AnkleBreakerCategory categoryToCheck in categoriesToCheck)
                {
                    if (srcCategories.ImplementsCategory(categoryToCheck, checkSubclass))
                        return true;
                }
            }
            return false;
        }

        public static bool ImplementsCategory(this IEnumerable<AnkleBreakerCategory> srcCategories, AnkleBreakerCategory categoryToCheck, bool checkSubclass = true)
        {
            if (categoryToCheck is null) return false;
            return srcCategories.ImplementsType(categoryToCheck.GetType(), checkSubclass);
        }

        public static bool ImplementsCategory(this AnkleBreakerCategory srcCategory, AnkleBreakerCategory categoryToCheck, bool checkSubclass = true)
        {
            if (categoryToCheck is null) return false;
            return srcCategory.ImplementsType(categoryToCheck.GetType(), checkSubclass);
        }

        public static bool ImplementsType(this IEnumerable<AnkleBreakerCategory> srcCategories, Type typeToCheck, bool checkSubclass = true)
        {
            foreach (AnkleBreakerCategory categoryToCheckAgainst in srcCategories)
            {
                if (categoryToCheckAgainst.ImplementsType(typeToCheck, checkSubclass))
                    return true;
            }
            return false;
        }

        public static bool ImplementsType(this AnkleBreakerCategory srcCategory, Type typeToCheck, bool checkSubclass = true)
        {
            Type srcCategoryType = srcCategory.GetType();
            return srcCategoryType == typeToCheck || (checkSubclass && typeToCheck.IsSubclassOf(srcCategoryType));
        }

        public static bool ContainsCategory(this IEnumerable<AnkleBreakerCategory> srcCategories, IEnumerable<AnkleBreakerCategory> categoriesToCheck)
            => srcCategories.ImplementsCategory(categoriesToCheck, false);

        public static bool ContainsCategory(this IEnumerable<AnkleBreakerCategory> srcCategories, AnkleBreakerCategory categoryToCheck)
            => srcCategories.ImplementsCategory(categoryToCheck, false);

        public static int GetInheritanceLevelsFrom(this AnkleBreakerCategory categoryToTest, AnkleBreakerCategory categoryToCheckAgainst)
        {
            Type categoryToTestType = categoryToTest.GetType();
            Type categoryToCheckAgainstType = categoryToCheckAgainst.GetType();

            // Handle simple case first
            if (categoryToTestType.Name == categoryToCheckAgainstType.Name)
                return 0;
            if (!categoryToTestType.IsSubclassOf(categoryToCheckAgainstType))
                return -1;

            Type categoryToCompare = categoryToTestType;
            int counter = 0;
            while (categoryToCompare != categoryToCheckAgainstType)
            {
                counter++;
                categoryToCompare = categoryToCompare.BaseType;
            }

            return counter;
        }
    }
}
