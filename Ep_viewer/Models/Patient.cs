using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Ep_viewer
{
    public partial class Patient
    {
        public Patient()
        {
            Episodes = new HashSet<Episode>();
            Insurances = new HashSet<Insurance>();
        }

        public int Id { get; set; }

        [Display(Name = "Имя пациента")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Name { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
    }
}
