using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Utility
{
    /// <summary>
    /// 通过反射转换数据行到实体对象【通用处理类】
    /// 最后更新 JHB: ON 2016-03-08
    /// </summary>
    public class ReaderConvert
    {
        private static object CheckType(object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            return Convert.ChangeType(value, conversionType);
        }

        private static bool IsNullOrDBNull(object obj)
        {
            return ((obj == null) || (obj is DBNull));
        }

        public static IList<T> ReaderToList<T>(IDataReader objReader) where T: new()
        {
            if (objReader != null)
            {
                List<T> list = new List<T>();
                Type type = typeof(T);
                while (objReader.Read())
                {
                    T local2 = default(T);
                    T local = (local2 == null) ? Activator.CreateInstance<T>() : (local2 = default(T));
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            PropertyInfo property = type.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (property != null)
                            {
                                Type propertyType = property.PropertyType;
                                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                {
                                    NullableConverter converter = new NullableConverter(propertyType);
                                    propertyType = converter.UnderlyingType;
                                }
                                if (property.PropertyType.IsEnum)
                                {
                                    object obj2 = Enum.ToObject(propertyType, objReader[i]);
                                    property.SetValue(local, obj2, null);
                                }
                                else
                                {
                                    property.SetValue(local, CheckType(objReader[i], propertyType), null);
                                }
                            }
                        }
                    }
                    list.Add(local);
                }
                return list;
            }
            return null;
        }

        public static T ReaderToModel<T>(IDataReader objReader) where T: new()
        {
            if ((objReader != null) && objReader.Read())
            {
                Type type = typeof(T);
                int fieldCount = objReader.FieldCount;
                T local = new T();
                for (int i = 0; i < fieldCount; i++)
                {
                    if (!IsNullOrDBNull(objReader[i]))
                    {//objReader.GetName(i).Replace("_", "")
                        PropertyInfo property = type.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (property != null)
                        {
                            Type propertyType = property.PropertyType;
                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                NullableConverter converter = new NullableConverter(propertyType);
                                propertyType = converter.UnderlyingType;
                            }
                            if (propertyType.IsEnum)
                            {
                                object obj2 = Enum.ToObject(propertyType, objReader[i]);
                                property.SetValue(local, obj2, null);
                            }
                            else
                            {
                                property.SetValue(local, CheckType(objReader[i], propertyType), null);
                            }
                        }
                    }
                }
                return local;
            }
            return default(T);
        }

        public static T DataRowToModel<T>(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            T model = (T)Activator.CreateInstance(typeof(T));

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
                else continue;
            }
            return model;
        }
    }
}

