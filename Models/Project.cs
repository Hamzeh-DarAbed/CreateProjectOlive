using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CreateProjectOlive.Models
{
    public record Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }= string.Empty;
        [Required]
        public string ProjectName { get; set; }= string.Empty;
        [Required]
        public string ProjectDescription { get; set; }= string.Empty;
        [Required]
        public string BusinessType { get; set; }= string.Empty;
        [Required]
        public string CreatedBy { get; set; }= string.Empty;
        [Required]
        public string Domain { get; set; }= string.Empty;

       
    }
}