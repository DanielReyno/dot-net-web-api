﻿using System.ComponentModel.DataAnnotations;

namespace WebAPITesting.Dtos.User
{
    public class UserAccountDto : UserLoginDto
    {
        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
    }

}

