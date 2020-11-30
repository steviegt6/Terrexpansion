using System;
using System.Collections.Generic;
using System.Reflection;

namespace Terrexpansion.Common.Utilities
{
    public static class ReflectionHelper
    {
        public const BindingFlags AllFlags = AnyAccessibility | AnyInstantiation;
        public const BindingFlags AnyAccessibility = BindingFlags.Public | BindingFlags.NonPublic;
        public const BindingFlags AnyInstantiation = BindingFlags.Static | BindingFlags.Instance;

        public static Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();
        public static Dictionary<string, FieldInfo> FieldInfoCache = new Dictionary<string, FieldInfo>();
        public static Dictionary<string, PropertyInfo> PropertyInfoCache = new Dictionary<string, PropertyInfo>();
        public static Dictionary<string, MethodInfo> MethodInfoCache = new Dictionary<string, MethodInfo>();

        public static FieldInfo GetStaticField(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                FieldInfo fieldInfo = type.GetField(name, AnyAccessibility | BindingFlags.Static);

                CacheField(fieldInfo, key);
            }

            return FieldInfoCache[key];
        }

        public static FieldInfo GetInstanceField(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                FieldInfo fieldInfo = type.GetField(name, AnyAccessibility | BindingFlags.Instance);

                CacheField(fieldInfo, key);
            }

            return FieldInfoCache[key];
        }

        public static FieldInfo GetAnyField(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                FieldInfo fieldInfo = type.GetField(name, AllFlags);

                CacheField(fieldInfo, key);
            }

            return FieldInfoCache[key];
        }

        public static PropertyInfo GetStaticProperty(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!PropertyInfoCache.ContainsKey(key))
            {
                PropertyInfo propertyInfo = type.GetProperty(name, AnyAccessibility | BindingFlags.Static);

                CacheProperty(propertyInfo, key);
            }

            return PropertyInfoCache[key];
        }

        public static PropertyInfo GetInstanceProperty(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!PropertyInfoCache.ContainsKey(key))
            {
                PropertyInfo propertyInfo = type.GetProperty(name, AnyAccessibility | BindingFlags.Instance);

                CacheProperty(propertyInfo, key);
            }

            return PropertyInfoCache[key];
        }

        public static PropertyInfo GetAnyProperty(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!PropertyInfoCache.ContainsKey(key))
            {
                PropertyInfo propertyInfo = type.GetProperty(name, AllFlags);

                CacheProperty(propertyInfo, key);
            }

            return PropertyInfoCache[key];
        }

        public static MethodInfo GetStaticMethod(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                MethodInfo methodInfo = type.GetMethod(name, AnyAccessibility | BindingFlags.Static);

                CacheMethod(methodInfo, key);
            }

            return MethodInfoCache[key];
        }

        public static MethodInfo GetInstanceMethod(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                MethodInfo methodInfo = type.GetMethod(name, AnyAccessibility | BindingFlags.Instance);

                CacheMethod(methodInfo, key);
            }

            return MethodInfoCache[key];
        }

        public static MethodInfo GetAnyMethod(this Type type, string name)
        {
            string key = $"{type.FullName}.{name}";

            if (!FieldInfoCache.ContainsKey(key))
            {
                MethodInfo methodInfo = type.GetMethod(name, AllFlags);

                CacheMethod(methodInfo, key);
            }

            return MethodInfoCache[key];
        }

        public static Type GetCachedType(this Assembly assembly, string name)
        {
            if (!TypeCache.ContainsKey(name))
            {
                Type type = assembly.GetType(name);

                CacheType(type, name);
            }

            return TypeCache[name];
        }

        public static FieldInfo CacheField(FieldInfo fieldInfo, string key) => FieldInfoCache[key] = fieldInfo;

        public static PropertyInfo CacheProperty(PropertyInfo propertyInfo, string key) => PropertyInfoCache[key] = propertyInfo;

        public static MethodInfo CacheMethod(MethodInfo methodInfo, string key) => MethodInfoCache[key] = methodInfo;

        public static Type CacheType(Type type, string key) => TypeCache[key] = type;
    }
}