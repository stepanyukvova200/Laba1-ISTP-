using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public partial class ClubsTournament
    {
        public int TournamentId { get; set; }
        public int ClubId { get; set; }
        public int ClubTournamentId { get; set; }

        [Display(Name = "Клуб")]
        public virtual Club Club { get; set; } = null!;

        [Display(Name = "Місце проведення")]
        public virtual Tournament Tournament { get; set; } = null!;
    }
}
