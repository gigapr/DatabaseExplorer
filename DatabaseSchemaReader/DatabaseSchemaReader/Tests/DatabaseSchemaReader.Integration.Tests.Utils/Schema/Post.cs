using System;
using System.Collections.Generic;

namespace DatabaseSchemaReader.Integration.Tests.Utils.Schema
{
    public class Post
    {
        public int Id                        { get; set; }
        public string Title                  { get; set; }
        public DateTime DateCreated          { get; set; }
        public string Content                { get; set; }
        public int BlogId                    { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Author Author                 { get; set; }
    }
}