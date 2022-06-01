using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public partial class ClubsSponsor
    {
        public int Edrpou { get; set; }
        public int ClubId { get; set; }
        public int ClubSponsorId { get; set; }

        [Display(Name = "Клуб")]
        public virtual Club Club { get; set; } = null!;

        [Display(Name = "Спонсор")]
        public virtual Sponsor EdrpouNavigation { get; set; } = null!;
    }
}
