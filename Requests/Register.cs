﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class Register:BaseRequest
    {
        public string FirstName { get; set; }
   
        public string LastName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int? ChiefId { get; set; }

        
        
    }
}
