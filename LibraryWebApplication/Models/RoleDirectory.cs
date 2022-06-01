using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApplication.Models
{
    public partial class RoleDirectory
    {
        public RoleDirectory()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        [Index(nameof(RoleDirectory.Role), IsUnique = true)]
        [Required(ErrorMessage = "Поле не повино бути порожнім")]
        [Display(Name = "Роль")]
        public string? Role { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
