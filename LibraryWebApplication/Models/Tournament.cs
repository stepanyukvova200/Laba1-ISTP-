using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            ClubsTournaments = new HashSet<ClubsTournament>();
        }

        public int TournamentId { get; set; }
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Місце проведення")]
        public string? Location { get; set; }
        [Display(Name = "Нагорода")]
        public string? Awards { get; set; }
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Правила проведення")]
        public string? Regulations { get; set; }

        public virtual ICollection<ClubsTournament> ClubsTournaments { get; set; }
    }
}
