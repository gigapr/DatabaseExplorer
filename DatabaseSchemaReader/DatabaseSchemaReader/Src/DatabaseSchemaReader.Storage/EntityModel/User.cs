using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseSchemaReader.Storage.EntityModel
{
    public class User
    {
        [BsonId]
        public object Id            { get; set; }
        public string Email         { get; set; }
        public string Password      { get; set; }
        public DateTime DateCreated { get; set; }     
    }
}