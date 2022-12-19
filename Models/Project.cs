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
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Project Description is required")]
        public string ProjectDescription { get; set; } = string.Empty;
        [Required(ErrorMessage = "Business Type is required")]
        public string BusinessType { get; set; } = string.Empty;
        [Required(ErrorMessage = "Created By is required")]
        public string CreatedBy { get; set; } = string.Empty;
        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; } = string.Empty;
        [Required(ErrorMessage = "Users is required")]
        public ICollection<User> Users { get; set; }


    }
}