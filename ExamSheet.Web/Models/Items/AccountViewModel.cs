using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    //TODO: ErrorMessage for all view models
    public class AccountViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsUniqueEmailAddress", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Повторіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "Тип акаунту")]
        [UIHint("AccountTypeView")]
        public AccountTypeView AccountType { get; set; }
        
        [Required]
        [Display(Name = "Посилання на викладача/деканат")]
        [UIHint("ReferenceId")]
        public string ReferenceId { get; set; }

        public List<TeacherViewModel> Teachers { get; set; }

        public List<DeaneryViewModel> Deaneries { get; set; }
    }

    public enum AccountTypeView
    {
        [Display(Name = "Викладач")]
        Teacher,
        [Display(Name = "Деканат")]
        Deanery
    }
}