using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PizzaShopping.Models.Enum;

namespace PizzaShopping.Models
{
    public class Account
    {
        [Key]
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public AccountType Type { get; set; }
    }
}
