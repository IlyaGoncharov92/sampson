using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyEngine.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="Заполните  поле Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Заполните поле пароля")]
        [StringLength(100,ErrorMessage="Пароль должен иметь от 6 до 100 символов",MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name="Введите пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}