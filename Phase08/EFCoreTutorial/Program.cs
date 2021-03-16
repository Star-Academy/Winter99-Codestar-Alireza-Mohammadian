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
                    Name = "Ken",
                };

                context.Students.Add(std);
                context.SaveChanges();

                var test = context.Students.Where(s => s.Name == "Ken").Single();
                System.Console.WriteLine(test.Name);
            }
        }
    }
}
