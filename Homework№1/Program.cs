using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowAllStudentsAsync();
            Console.ReadLine();
        }

        private static async void ShowAllStudentsAsync()
        {
            List<StudentInfo> studentList = await Task.Run(() => GetAllStudents());

            foreach (StudentInfo info in studentList)
            {
                Console.WriteLine(info.FullName + " " + info.Group);
            }
        }

        private static List<StudentInfo> GetAllStudents()
        {
        using (UniversityDbContext context = new UniversityDbContext())
        {
            List<StudentInfo> studentList = (from student in context.Students
                                                  join grou in context.Groups
                                                  on student.Group.Id equals grou.Id
                                                  select new StudentInfo
                                                  {
                                                      FullName = student.FullName
                                                      ,
                                                      Group = grou.Name
                                                  }).ToList();

            return studentList;
            }
        }
    }

    class StudentInfo
    {
        public string FullName { get; set; }

        public string Group { get; set; }
    }
}
