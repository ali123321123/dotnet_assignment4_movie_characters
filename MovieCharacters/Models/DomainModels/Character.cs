﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Models.DomainModels
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
