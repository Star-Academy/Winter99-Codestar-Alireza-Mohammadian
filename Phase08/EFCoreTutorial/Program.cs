using System;
using System.Linq;

namespace EFCoreTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext()) {

                var std = new Student()
                {
                     Name = "T",
                };

                context.Students.Add(std);
                context.SaveChanges();

                var test = context.Students.FirstOrDefault();
                System.Console.WriteLine(test.Name);
            }
        }
    }
}
