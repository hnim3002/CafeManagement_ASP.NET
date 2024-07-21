using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class WorkSchedules
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public ShiftType Type { get; set; }
        public string? Note { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }
        public Guid CafeId { get; set; }
        [ForeignKey("CafeId")]
        public Cafe Cafe { get; set; }
    }
}

namespace CafeManagement.Models
{
    public enum ShiftType
    {
        Morning,
        Afternoon,
        Evening
    }
}