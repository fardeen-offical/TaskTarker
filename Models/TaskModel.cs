using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class TaskModel
    {
        // Har task ki unique ID
        public int Id { get; set; }

        [Required]
        [Display(Name = "Task ka Naam")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Status")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Kab tak khatam karna hai?")]
        public DateTime DueDate { get; set; }
    }
}