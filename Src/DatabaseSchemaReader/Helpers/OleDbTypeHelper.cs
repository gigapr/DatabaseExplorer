using System.Data.OleDb;

namespace GigaWebSolution.DatabaseSchemaReader.Helpers
{
    public class OleDbTypeHelper
    {
        internal static string OleDbTypeToString(OleDbType type)
        {
            switch (type)
            {
                case OleDbType.Binary:
                    return "Binary";
                case OleDbType.Boolean:
                    return "Bit";
                case OleDbType.UnsignedTinyInt:
                    return "Byte";
                case OleDbType.TinyInt:
                    return "TinyInt";
                case OleDbType.Integer:
                    return "Integer";
                case OleDbType.Currency:
                    return "Currency";
                case OleDbType.Date:
                case OleDbType.DBDate:
                    return "DateTime";
                case OleDbType.Double:
                    return "Float";
                case OleDbType.Guid:
                    return "UniqueIdentifier";
                case OleDbType.Char:
                case OleDbType.WChar:
                    return "Text";
                case OleDbType.Single:
                    return "Real";
                case OleDbType.SmallInt:
                    return "SmallInt";
                case OleDbType.Numeric:
                case OleDbType.Decimal:
                    return "Decimal";
                default:
                    return "Unknown";
            }
        }
    }
}