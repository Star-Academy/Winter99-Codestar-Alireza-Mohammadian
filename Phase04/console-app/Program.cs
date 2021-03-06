﻿using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace console_app
{
    class Program
    {
        private const string StudentsPath = "Data/Students.json";
        private const string ScoresPath = "Data/Scores.json";

        static void Main(string[] args)
        {
            var students = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(StudentsPath));
            var grades = JsonSerializer.Deserialize<List<Grade>>(File.ReadAllText(ScoresPath));
            var groups = (from grade in grades
                          group grade by grade.StudentNumber into gr
                          select new { StudentNumber = gr.Key, StudentAverage = gr.Average(p => p.Score) } into t
                          join student in students on t.StudentNumber equals student.StudentNumber
                          orderby t.StudentAverage descending
                          select new { FirstName = student.FirstName, LastName = student.LastName, avg = t.StudentAverage }).Take(3);

            foreach (var s in groups)
            {
                Console.WriteLine(s.FirstName + " " + s.LastName + " : " + s.avg);
            }
        }
    }
}

