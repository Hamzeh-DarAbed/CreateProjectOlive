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
        public string Id { get; set; }
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Project Description is required")]
        public string ProjectDescription { get; set; }
        [Required(ErrorMessage = "Business Type is required")]
        public string BusinessType { get; set; }
        [Required(ErrorMessage = "Created By is required")]
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; }


    }
}