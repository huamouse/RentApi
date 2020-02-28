using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CPTech.CustomORM.Mapping
{
    public static class DBMappingExtend
    {
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(BaseMappingAttribute), true))
            {
                var attribute = t.GetCustomAttribute<BaseMappingAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return t.Name;
            }
        }


        public static string GetMappingTableName(this Type type)
        {
            if (type.IsDefined(typeof(TableAttribute), true))
            {
                var attribute = type.GetCustomAttribute<TableAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return type.Name;
            }
        }
        public static string GetMappingPropertyName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnAttribute), true))
            {
                var attribute = prop.GetCustomAttribute<ColumnAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
