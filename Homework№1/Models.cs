using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    public class Students
    {
        [Key]
        [DisplayName("Индекс")]
        public int Id { get; set; }

        [Required]
        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Группа")]
        virtual public Groups Group { get;set;}
    }

    public class Groups
    {
        [Key]
        [DisplayName("Индекс")]
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
    }

    public class UniversityDbContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<Groups> Groups { get; set; }

        public UniversityDbContext()
            : base("UniversityDb")
        {

        }

        static UniversityDbContext()
        {
            Database.SetInitializer(new UniversityContextInitializer());
        }
    }

    class UniversityContextInitializer : DropCreateDatabaseAlways<UniversityDbContext>
    {
        protected override void Seed(UniversityDbContext db)
        {
            Groups group1 = new Groups { Name = "151-18ck-1" };
            Groups group2 = new Groups { Name = "E-14 1/9" };

            var students = new List<Students>
            {
                new Students() { FullName = "Karpov Aleksandr Vladimirovich", Group = group1}
                , new Students() { FullName = "Petrov Petr Petrovovich", Group = group2}
                , new Students() { FullName = "Vaskin Vasiliy Vasilievich", Group = group1}
                , new Students() { FullName = "Vovkow Vowa Vowovich", Group = group2 }
            };

            db.Groups.AddRange(new List<Groups>(){ group1, group2 });
            db.Students.AddRange(students);

            db.SaveChanges();
        }
    }
}
