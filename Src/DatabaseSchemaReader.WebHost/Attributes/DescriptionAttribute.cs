using System;

namespace DatabaseSchemaReader.WebHost.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }
    }
}
