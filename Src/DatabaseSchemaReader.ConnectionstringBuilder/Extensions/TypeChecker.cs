namespace DatabaseSchemaReader.ConnectionstringBuilder.Extensions
{
    public static class TypeChecker
    {
        public static bool IsA<T>(this object obj)
        {
            return obj is T;
        }
    }
}
