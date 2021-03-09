using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Models.DomainModels
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(25)]
        public string? Alias { get; set; }
       
        public string Gender { get; set; }
        public string Picture { get; set; }

        //Relationships
        public ICollection<Movie> Movies { get; set; }
    }
}
