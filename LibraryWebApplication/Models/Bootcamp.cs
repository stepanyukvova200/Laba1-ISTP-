using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public partial class Bootcamp
    {
        public int BootcampId { get; set; }

        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Місце знаходження")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Тип конструкції")]
        public string? ConstructionType { get; set; }

        public int? ClubId { get; set; }

        [Display(Name = "Клуб власник")]
        public virtual Club? Club { get; set; }
    }
}
