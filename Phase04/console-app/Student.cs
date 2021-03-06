namespace console_app
{
    class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentNumber, string firstName, string lastName)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}