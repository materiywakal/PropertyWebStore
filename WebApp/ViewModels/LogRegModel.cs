using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите электронный адрес.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите имя пользователя.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите электронный адрес.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный электронный адрес")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Введите пароль.")]
        [StringLength(100, ErrorMessage = "Пароль должен состоять минимум из 6 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = " ")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}