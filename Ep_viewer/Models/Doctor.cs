using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Ep_viewer
{
    public partial class Doctor
    {
        public Doctor()
        {
            Episodes = new HashSet<Episode>();
        }

        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не должно быть пустым")]
        public string Name { get; set; }

        [Display(Name = "Зарплата")]
        public double Salary { get; set; }

        [Display(Name = "Больница")]
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
