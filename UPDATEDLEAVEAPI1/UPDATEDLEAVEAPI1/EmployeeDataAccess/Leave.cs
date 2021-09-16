using LEAVETRACKER.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDataAccess
{
    public class LEAVE
    {
        public int employeeId { set; get; }
        public string name { set; get; }
        public int managerId { set; get; }

        [Required(ErrorMessage = "Title is required.")]
        public string title { set; get; }

        [Required]
        public string description { set; get; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime startDate { set; get; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime endDate { set; get; }

        public int id { get; internal set; }

        public string status { set; get; } = "Open";


    }
}