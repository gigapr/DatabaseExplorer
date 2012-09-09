using System.Collections.Generic;
using DatabaseSchemaReader.Contract.BusinessObjects;

namespace GigaWebSolution.DatabaseSchemaReader.Providers
{
    internal class ServerTypesProvider
    {
        internal static ICollection<TypeInfo> GetServerTypes()
        {
            var types = new List<TypeInfo>();

            // Add a Binary type.
            var info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = true,
                AllowNulls = true,
                IsBinary = true,
                IsVariableLength = false,
                Name = "Binary"
            };
            types.Add(info);

            // Add a Bit type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Bit"
            };
            types.Add(info);

            // Add a Byte type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Byte"
            };
            types.Add(info);

            // Add a Currency type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                HasPrecision = true,
                MaximumPrecision = 28,
                DefaultPrecision = 18,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Currency"
            };
            types.Add(info);

            // Add a DateTime type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "DateTime"
            };
            types.Add(info);

            // Add a DateTime type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "DBTimeStamp"
            };
            types.Add(info);
            

            // Add a Decimal type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                HasPrecision = true,
                MaximumPrecision = 28,
                DefaultPrecision = 18,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Decimal"
            };
            types.Add(info);

            // Add a Float type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Float"
            };
            types.Add(info);

            // Add an Integer type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Integer"
            };
            types.Add(info);

            // Add a Real type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "Real"
            };
            types.Add(info);

            // Add a SmallInt type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = false,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = false,
                Name = "SmallInt"
            };
            types.Add(info);

            // Add a Text type.
            info = new TypeInfo
            {
                AllowIdentity = false,
                AllowLength = true,
                AllowNulls = true,
                IsBinary = false,
                IsVariableLength = true,
                Name = "Text"
            };
            types.Add(info);

            // Add a UNIQUEIDENTIFIER type.
            info = new TypeInfo
            {
                AllowIdentity = true,
                AllowLength = false,
                AllowNulls = false,
                IsBinary = false,
                IsVariableLength = false,
                Name = "UniqueIdentifier"
            };
            types.Add(info);

            return types;
        }
    }
}
