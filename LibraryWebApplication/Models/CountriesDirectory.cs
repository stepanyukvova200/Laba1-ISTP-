using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApplication.Models
{
    public partial class CountriesDirectory
    {
        public CountriesDirectory()
        {
            Clubs = new HashSet<Club>();
            Players = new HashSet<Player>();
            Sponsors = new HashSet<Sponsor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Країна")]
        [Index(nameof(CountriesDirectory.Country), IsUnique = true)]
        public string? Country { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
