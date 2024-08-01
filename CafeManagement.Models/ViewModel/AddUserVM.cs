using CafeManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.ViewModel
{
    public class AddUserVM
    {
        public String Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public Guid CafeId { get; set; }
        [ForeignKey("CafeId")]
        public Cafe Cafe { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }



        [ValidateNever]
        public IEnumerable<SelectListItem> CafeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Genders { get; set; }
        [ValidateNever]
        public string Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Roles { get; set; }


    }
}
