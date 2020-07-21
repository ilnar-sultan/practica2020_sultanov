using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealSurfClub.Models.DBModel
{
    public class User
    {
        [Key]
        // ID пользователя
        public int Id { get; set; }

        // никнэйм
        [Display(Name ="Псевдоним*")]
        [Required(ErrorMessage = "Ошибка в псевдониме"), MinLength(3), MaxLength(20)]
        public string Nickname { get; set; }

        // почта
        [Display(Name = "Почта*")]
        [Required(ErrorMessage = "Указание электронной почты обязательно")]
        [EmailAddress(ErrorMessage = "Неверно указан электронный адрес")]
        public string Email { get; set; }

        // пароль
        [Display(Name = "Пароль*")]
        [Required(ErrorMessage = "Ошибка в пароле"), MinLength(6), MaxLength(20)]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль*")]
        [NotMapped]
        [Required(ErrorMessage = "Ошибка в пароле"), MinLength(6), MaxLength(20)]
        public string PasswordСonfirm { get; set; }

        // фамилия
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        // имя
        [Display(Name = "Имя")]
        public string Name { get; set; }

        // о себе
        [Display(Name = "О себе")]
        public string About { get; set; }

        // достижения
        [Display(Name = "Достижения")]
        public string Achivements { get; set; }

        // Контакты
        [Display(Name = "Контактная информация")]
        public string ContactInfo { get; set; }

        // Аватар
        [Display(Name = "Выберите фото")]
        public Guid Photo { get; set; }
    }
}