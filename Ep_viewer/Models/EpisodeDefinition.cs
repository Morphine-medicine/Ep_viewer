using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Ep_viewer
{
    public partial class EpisodeDefinition
    {
        public EpisodeDefinition()
        {
            Episodes = new HashSet<Episode>();
        }

        public int Id { get; set; }

        [Display(Name = "Название типа")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Definition { get; set; }

        [Display(Name = "Стандартная стоимость")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Cost { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
