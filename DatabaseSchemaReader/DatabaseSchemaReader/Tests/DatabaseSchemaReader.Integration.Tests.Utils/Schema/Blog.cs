using System.Collections.Generic;

namespace DatabaseSchemaReader.Integration.Tests.Utils.Schema
{
    public class Blog
    {
        public int Id                           { get; set; }
        public string Title                     { get; set; }
        public string BloggerName               { get; set; }
        public virtual ICollection<Post> Posts  { get; set; }
    }
}