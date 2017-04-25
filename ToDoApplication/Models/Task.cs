using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApplication.Models
{
    public class Task
    {
        [Key]
        [Required]
        [RegularExpression("^[A-Z]{3,3}[0-9]{4,4}$")]
        public string TaskCode { get; set; }

        [Required]
        [DisplayName("Name")]
        public string TaskName { get; set; }

        [DisplayName("Description")]
        public string TaskDescription { get; set; }

        [DisplayName("Status")]
        public bool IsComplete { get; set; }

        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime EnteredDate { get; set; }

        [Required]
        public string AssignedTo { get; set; }

        [DisplayName("AssignedBy")]
        public string AssignedBy { get; set; } 
    }
}