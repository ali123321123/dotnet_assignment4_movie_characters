using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharacters.Models.DomainModels
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(15)]
        public string Genre { get; set; }
        [Range(1, 9999)]
        public int ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }
        [MaxLength(50)]
        public string Picture { get; set; }
        [MaxLength(50)]
        public string Trailer { get; set; }
        public int? FranchiseId { get; set; }
        public string Franchise { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
