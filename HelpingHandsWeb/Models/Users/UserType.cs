using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models.Users
{
    public class UserType
    {
        [Key]
        [StringLength(1)]
        public string UserTypeId { get; set; }

        [StringLength(100)]
        public string UserTypeDesc { get; set; }


        public bool IsDeleted { get; set; }
    }
}
