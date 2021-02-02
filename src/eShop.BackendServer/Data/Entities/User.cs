using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace eShop.BackendServer.Data.Entities
{
    public class User : IdentityUser, IDateTracking
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public int? NumberOfVotes { get; set; }
        public int? NumberOfReports { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
