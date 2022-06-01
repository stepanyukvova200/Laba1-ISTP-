using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApplication.Models
{
    public partial class Club
    {
        public Club()
        {
            Bootcamps = new HashSet<Bootcamp>();
            ClubsSponsors = new HashSet<ClubsSponsor>();
            ClubsTournaments = new HashSet<ClubsTournament>();
            Players = new HashSet<Player>();
        }

        public int ClubId { get; set; }
        [Index(nameof(NameClub), IsUnique = true)]
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Назва клубу")]
        public string? NameClub { get; set; } //?
        public int? CountryId { get; set; }

        [Display(Name = "Країна")]
        public virtual CountriesDirectory? Country { get; set; }
        public virtual ICollection<Bootcamp> Bootcamps { get; set; }
        public virtual ICollection<ClubsSponsor> ClubsSponsors { get; set; }
        public virtual ICollection<ClubsTournament> ClubsTournaments { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
