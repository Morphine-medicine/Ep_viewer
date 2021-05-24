using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Ep_viewer
{
    public partial class Insurance
    {
        public int Id { get; set; }

        [Display(Name = "Пациент")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int PatientId { get; set; }

        [Display(Name = "Страховой полис")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Policy { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
