using System;

namespace DatabaseSchemaReader.WebHost.Attributes
{
    public class ParameterDescriptionAttribute : Attribute
    {
        public string Description { get; set; } 
    }
}