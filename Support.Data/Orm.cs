using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Platform.Support.Data.Attributes;
using System.Data;

namespace Platform.Support.Data
{
    public static class Orm
    {
        public const string ImplicitPkName = "Id";
        public const string ImplicitIndexSuffix = "Id";

        public static string SqlDecl(IDbConnection conn, TableMapping.Column p, bool storeDateTimeAsTicks) //, IDictionary<Type, string> extraTypeMappings)
        {
            string decl = null;

            switch (conn.GetEngine())
            {
                case Engines.Sql:
                case Engines.SqlCE:
                    decl = "[" + p.Name + "] ";
                    break;
                default:
                    decl = "'" + p.Name + "' ";
                    break;
            }

            decl += SqlType(conn, p, storeDateTimeAsTicks) + " "; //, extraTypeMappings) + " ";

            if (p.IsPK)
                decl += "PRIMARY KEY ";

            switch (conn.GetEngine())
            {
                case Engines.Sql:
                case Engines.SqlCE:
                    if (p.IsAutoInc)
                        decl += "IDENTITY(1,1) ";
                    break;
                default:
                    if (p.IsAutoInc)
                        decl += "AUTOINCREMENT ";
                    break;
            }

            if (!string.IsNullOrEmpty(p.Collation))
                decl += "COLLATE " + p.Collation + " ";
            if (!p.IsNullable)
                decl += "NOT NULL ";

            return decl;

        }
        public static string SqlType(IDbConnection conn, TableMapping.Column p, bool storeDateTimeAsTicks) //, IDictionary<Type, string> extraTypeMappings)
        {
            Type clrType = p.ColumnType;
            object interfaces = clrType.GetInterfaces().ToList();

            switch (conn.GetEngine())
            {
                case Engines.Sql:
                case Engines.SqlCE:
                    if (object.ReferenceEquals(clrType, typeof(Boolean)))
                        return "BIT";
                    if (object.ReferenceEquals(clrType, typeof(Double)))
                        return "REAL";
                    if (object.ReferenceEquals(clrType, typeof(Decimal)))
                        return "DECIMAL";
                    break;
                default:
                    if (object.ReferenceEquals(clrType, typeof(Boolean)))
                        return "INTEGER";
                    if (object.ReferenceEquals(clrType, typeof(Double)) | object.ReferenceEquals(clrType, typeof(Decimal)))
                        return "FLOAT";
                    break;
            }
            if (object.ReferenceEquals(clrType, typeof(Byte)) || object.ReferenceEquals(clrType, typeof(UInt16)) || object.ReferenceEquals(clrType, typeof(SByte)) || object.ReferenceEquals(clrType, typeof(Int16)) || object.ReferenceEquals(clrType, typeof(Int32)))
                return "INTEGER";
            if (object.ReferenceEquals(clrType, typeof(UInt32)) || object.ReferenceEquals(clrType, typeof(Int64)))

                if (conn.GetEngine() == Engines.SQLite && p.IsAutoInc)
                {
                    return "INTEGER";
                }
                else
                {
                    return "BIGINT";
                }

                
            if (object.ReferenceEquals(clrType, typeof(Single)))
                return "FLOAT";
            if (object.ReferenceEquals(clrType, typeof(String)))
            {
                System.Nullable<int> len = p.MaxStringLength;
                switch (conn.GetEngine())
                {
                    case Engines.Sql:
                    case Engines.SqlCE:
                        return "VARCHAR(200)";
                    default:
                        if (len.HasValue)
                        {
                            return "VARCHAR(" + len.Value + ")";
                        }

                        return "VARCHAR";
                }

            }
            if (object.ReferenceEquals(clrType, typeof(TimeSpan)))
                return "BIGINT";
            if (object.ReferenceEquals(clrType, typeof(DateTime)))
                return storeDateTimeAsTicks ? "BIGINT" : "DATETIME";
            if (object.ReferenceEquals(clrType, typeof(DateTimeOffset)))
                return "BIGINT";
            if (clrType.IsEnum)
                return "INTEGER";
            if (object.ReferenceEquals(clrType, typeof(byte[])))
            {
                switch (conn.GetEngine())
                {
                    case Engines.Sql:
                    case Engines.SqlCE:
                        System.Nullable<int> len = p.MaxStringLength;
                        if (len.HasValue)
                        {
                            return "VARBINARY(" + len.Value + ")";
                        }
                        return "VARBINARY(MAX)";
                    default:
                        return "BLOB";
                }
            }
            //OrElse interfaces.Contains(GetType(ISerializable(Of Guid))) Then
            if (object.ReferenceEquals(clrType, typeof(Guid)))
            {
                switch (conn.GetEngine())
                {
                    case Engines.Sql:
                    case Engines.SqlCE:
                        return "UNIQUEIDENTIFIER";
                    default:
                        return "VARCHAR(36)";
                }
            }

            if (object.ReferenceEquals(clrType, typeof(Version)))
            {
                switch (conn.GetEngine())
                {
                    case Engines.Sql:
                    case Engines.SqlCE:
                        return "NVARCHAR(400)";
                    default:
                        return "VARCHAR(200)";
                }
            }
            //If serializer IsNot Nothing AndAlso serializer.CanDeserialize(clrType) Then
            //    Return "blob"
            //End If
            throw new NotSupportedException("Don't know about " + clrType.ToString());
        }

        public static bool IsPK(MemberInfo p)
        {
            IEnumerable<PrimaryKeyAttribute> _return = (IEnumerable<PrimaryKeyAttribute>)p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            return _return.Any<PrimaryKeyAttribute>();
        }
        public static string Collation(MemberInfo p)
        {

            foreach (CustomAttributeData attribute in p.GetCustomAttributesData().Where(a => object.ReferenceEquals(a.GetType(), typeof(CollationAttribute))))
            {
                return (string)attribute.ConstructorArguments[0].Value;
            }
            return string.Empty;
        }
        public static bool IsAutoInc(MemberInfo p)
        {
            IEnumerable<AutoIncrementAttribute> _return = (IEnumerable<AutoIncrementAttribute>)p.GetCustomAttributes(typeof(AutoIncrementAttribute), true);
            return _return.Any<AutoIncrementAttribute>();
        }
        public static IEnumerable<IndexedAttribute> GetIndices(MemberInfo p)
        {
            IEnumerable<IndexedAttribute> _return = (IEnumerable<IndexedAttribute>)p.GetCustomAttributes(typeof(IndexedAttribute), true);
            return _return;
        }

        public static int? MaxStringLength(PropertyInfo p)
        {
            foreach (CustomAttributeData attribute in p.GetCustomAttributesData().Where(a => object.ReferenceEquals(a.GetType(), typeof(MaxLengthAttribute))))
            {
                return (int) attribute.ConstructorArguments[0].Value;
            }
            return null;
        }

        public static object GetDefaultValue(PropertyInfo p)
        {
            foreach (CustomAttributeData attribute in p.GetCustomAttributesData().Where(a => object.ReferenceEquals(a.GetType(), typeof(DefaultAttribute))))
            {
                try
                {
                    object result;
                    if (!(bool)attribute.ConstructorArguments[0].Value)
                    {
                        result = Convert.ChangeType(attribute.ConstructorArguments[0].Value, p.PropertyType);
                        return result;
                    }
                    object obj = Activator.CreateInstance(p.DeclaringType);
                    result = p.GetValue(obj, null);
                    return result;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to convert " + attribute.ConstructorArguments[0].Value + " to type " + p.PropertyType, exception);
                }
            }
            return null;

            //IEnumerable<CustomAttributeData> arg_25_0 = p.CustomAttributes;
            //Func<CustomAttributeData, bool> arg_25_1;
            //if ((arg_25_1 = Orm.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate6) == null)
            //{
            //    arg_25_1 = (Orm.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate6 = new Func<CustomAttributeData, bool>(Orm.<>c__DisplayClass0.CS$<>9__inst.<GetDefaultValue>b__5));
            //}
            //foreach (CustomAttributeData current in arg_25_0.Where(arg_25_1))
            //{
            //    try
            //    {
            //        object result;
            //        if (!(bool)current.ConstructorArguments[0].Value)
            //        {
            //            result = Convert.ChangeType(current.ConstructorArguments[0].Value, p.PropertyType);
            //            return result;
            //        }
            //        object obj = Activator.CreateInstance(p.DeclaringType);
            //        result = p.GetValue(obj);
            //        return result;
            //    }
            //    catch (Exception innerException)
            //    {
            //        throw new Exception(string.Concat(new object[]
            //        {
            //            "Unable to convert ",
            //            current.ConstructorArguments[0].Value,
            //            " to type ",
            //            p.PropertyType
            //        }), innerException);
            //    }
            //}
            //return null;
        }

        public static bool IsMarkedNotNull(MemberInfo p)
        {
            IEnumerable<NotNullAttribute> _return = (IEnumerable<NotNullAttribute>)p.GetCustomAttributes(typeof(NotNullAttribute), true);
            return _return.Any<NotNullAttribute>();
        }

    }
}