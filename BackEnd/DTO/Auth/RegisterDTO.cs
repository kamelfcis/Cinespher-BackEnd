using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO.Auth
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
