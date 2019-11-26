using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть e-mail адресу")]
        [Display(Name = "е-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
