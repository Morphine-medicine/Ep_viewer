using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Ep_viewer
{
    public partial class Hospital
    {
        public Hospital()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }

        [Display(Name = "Локация")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Location { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
