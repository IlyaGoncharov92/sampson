using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MyEngine.Models
{
    public class UserContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<RelatedDeclaration> RelatedDeclarations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ManagePhotoUser> ManagePhotoUsers { get; set; }
        public DbSet<LikedDeclaration> LikedDeclarations { get; set; }
    }

    public class ManagePhotoUser
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Declaration> Declarations { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Declaration
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните поле Заголовок")]
        [Display(Name = "Заголовок")]
        [StringLength(50, ErrorMessage = "Заголовок должен быть минимум 1 максимум 50 символов", MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Модель")]
        public string Description { get; set; }

        [Display(Name = "Дополнительное описание")]
        public string ExtraDescription { get; set; }

        [Display(Name="Укажите имя")]
        public string Name { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Display(Name = "Состав")]
        public string Сonsist { get; set; }

        [Display(Name="Укажите цену")]
        public int? Coast { get; set; }

        [Display(Name = "Артикул")]
        public int? Article { get; set; }

        [Display(Name = "Размер")]
        public int? Size { get; set; }
        public int? Rating { set; get; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime PublicDate { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string DeclarationType { get; set; }
    }
    
    public class RelatedDeclaration
    {
        public int Id { get; set; }
        public int IdParent { get; set; }
        public int IdChild { get; set; }
    }
    
    public class LikedDeclaration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DeclarationId { get; set; }
        public Declaration Declaration { get; set; }
    }
    
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Название категории")]
        public string Name { get; set; }
        public string IdTitle { get; set; }
        public int? SectionId { get; set; }
        public Section Section { get; set; }
        public IEnumerable<Declaration> Declarations { get; set; }
    }

    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdTitle { get; set; }
        public ICollection<Category> Categories { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int ImageOrder { get; set; }
        public int ImageType { get; set; }
        public int? DeclarationId { get; set; }
        public Declaration Declaration { get; set; }
        public IEnumerable<Declaration> Declarations { get; set; }
    }
}