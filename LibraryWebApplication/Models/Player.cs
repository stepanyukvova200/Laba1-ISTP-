using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public partial class Player
    {
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Нік")]
        public string Nickname { get; set; } = null!;
        public int CountryId { get; set; }
        public int RoleId { get; set; }
        [Range(0, 200, ErrorMessage = "Значення {0} має бути між {1} та {2}.")]
        [Display(Name = "Статистика (200)")]
        public int? Statistic { get; set; }
        [Display(Name = "Нагороди")]
        public string? Awards { get; set; }
        public int? ClubId { get; set; }

        [Display(Name = "Клуб гравця")]
        public virtual Club? Club { get; set; }

        [Display(Name = "Країна")]
        public virtual CountriesDirectory Country { get; set; } = null!;

        [Display(Name = "Роль")]
        public virtual RoleDirectory Role { get; set; } = null!;
    }
}
