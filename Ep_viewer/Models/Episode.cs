using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Ep_viewer
{
    public partial class Episode
    {
        public int Id { get; set; }

        [Display(Name = "Пациент")]
        public int PatientId { get; set; }


        [Display(Name = "Врач")]
        public int DoctorId { get; set; }

        [Display(Name = "Дата приёма")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public DateTime Date { get; set; }

        [Display(Name = "Стоимость приёма")]
        public decimal? Payment { get; set; }

        [Display(Name = "Описание случая")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Info { get; set; }

        [Display(Name = "Повторный приём?")]
        public bool? SecondVisit { get; set; }

        [Display(Name = "Статус приёма")]
        public string Status { get; set; }

        public int EpisodeDefinition { get; set; }

        [Display(Name = "Врач")]
        public virtual Doctor Doctor { get; set; }

        [Display(Name = "Тип приёма")]
        public virtual EpisodeDefinition EpisodeDefinitionNavigation { get; set; }

        [Display(Name = "Пациент")]
        public virtual Patient Patient { get; set; }
    }
}
