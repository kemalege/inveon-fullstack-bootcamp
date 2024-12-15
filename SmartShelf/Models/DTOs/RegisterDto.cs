﻿namespace SmartShelf.Models.DTOs
{
    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string City { get; set; }
    }
    
}