using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealSurfClub.Models.DBModel
{
    public class Post
    {
        [Key]
        /// <summary>
        /// ID записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст записи
        /// </summary>
        [Display(Name = "Введите текст"), MaxLength(4095, ErrorMessage = "Максимальное число символов - 4095")]
        public string Text { get; set; }

        /// <summary>
        /// Фото профиля
        /// </summary>
        [Display(Name = "Прикрепить изображение")]
        public Guid Photo { get; set; }

        /// <summary>
        ///  Дата публикации
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Автор поста
        /// </summary>
        public virtual User Author { get; set; }
    }
}