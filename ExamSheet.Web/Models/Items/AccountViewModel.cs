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

        [Required(ErrorMessage = "Введіть e-mail адресу")]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsUniqueEmailAddress", controller: "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторіть пароль")]
        [Display(Name = "Повторіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Виберіть тип акаунту")]
        [Display(Name = "Тип акаунту")]
        [UIHint("AccountTypeView")]
        public AccountTypeView AccountType { get; set; }
        
        [Required(ErrorMessage = "Виберіть посилання на викладача/деканат")]
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