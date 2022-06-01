using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApplication.Models
{
    
    public partial class Sponsor
    {
        public Sponsor()
        {
            ClubsSponsors = new HashSet<ClubsSponsor>();
        }
        [Index(nameof(Sponsor.Edrpou), IsUnique = true)]
        [Range(10000000, 100000000, ErrorMessage = "Значення {0} має бути восьмизначним.")]
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "ЄДРПОУ")]
        public int Edrpou { get; set; }

        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Назва")]
        public string? NameSponsor { get; set; }

        [Display(Name = "Сфера діяльності")]
        public string? SphereOfActivity { get; set; }
        public int? CountryId { get; set; }

        [Display(Name = "Країна")]
        public virtual CountriesDirectory? Country { get; set; }
        public virtual ICollection<ClubsSponsor> ClubsSponsors { get; set; }
    }
}
