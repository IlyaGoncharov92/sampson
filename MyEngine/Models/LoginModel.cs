using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyEngine.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Заполните поле Логина")]
        [Display(Name="Введите Логин")]
        public string Email{get;set;}

        [Required(ErrorMessage="Заполните поля пароля")]
        [DataType(DataType.Password)]
        [Display(Name="Введите пароль")]
        public string Password{get;set;}

        [Display(Name="Запомнить")]
        public bool RememberMe{get;set;}
    }
}