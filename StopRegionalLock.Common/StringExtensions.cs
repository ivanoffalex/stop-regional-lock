using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace StopRegionalLock.Common
{
    public static class StringExtensions
    {
        public static string GetQualifiedTypeName(this Type value)
        {
            return string.Format("{0},{1}", value.FullName, value.Assembly.GetName().Name);
        }

        public static T ToType<T>(this object o, T typeToCastTo)
        {
            return (T)o;
        }

        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo[] mi = type.GetMember(value.ToString());
            if (mi != null && mi.Length > 0)
            {
                object[] attrs = mi[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return value.ToString();
        }

        public static string ToJson(this object obj)
        {
            return ToJson(obj, false);
        }

        public static string ToJson(this object obj, bool isIndent)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = isIndent ? Formatting.Indented : Formatting.None;
                    new JsonSerializer().Serialize(jw, obj);
                    return sw.ToString();
                }
            }
        }

        public static T ToJsonObject<T>(this string json, T typeToCastTo)
        {
            using (StringReader sr = new StringReader(json))
            {
                object obj = new JsonSerializer().Deserialize(sr, typeToCastTo.GetType());
                return (T)obj;
            }
        }

        public static T ToJsonObject<T>(this string json, Type type)
        {
            using (StringReader sr = new StringReader(json))
            {
                object obj = new JsonSerializer().Deserialize(sr, type);
                return (T)obj;
            }
        }

        public static string Join(this IList collection, string glue)
        {
            int cnt = collection != null ? collection.Count : 0;
            StringBuilder sb = new StringBuilder();
            if (cnt > 0)
            {
                sb.Append(collection[0]);
            }
            for (int i = 1; i < cnt; i++)
            {
                sb.Append(glue);
                sb.Append(collection[i]);
            }
            return sb.ToString();
        }

    }
}
