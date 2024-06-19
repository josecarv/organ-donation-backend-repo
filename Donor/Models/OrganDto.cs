using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Donor.Entities;

namespace Donor.Models
{
    public class OrganDto
    {
        public int? Id { get; set; }
        
        public string name { get; set; } = string.Empty;




        
    }

}
