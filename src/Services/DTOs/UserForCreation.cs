using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Services.DTOs
{
    public class UserForCreation
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required, MinLength(5),NotNull]
        public string Login { get; set; }

        [Required,MinLength(6),NotNull]
        public string Password { get; set; }
        public IFormFile File { get; set; }
    }
}
