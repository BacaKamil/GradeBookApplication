using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.2);

            List<double> grades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();

            int rank = grades.FindIndex(g => g == averageGrade);
            if (rank < 0)
            {
                rank = grades.FindIndex(g => g < averageGrade);
            }

            if (rank < threshold)
            {
                return 'A';
            }
            else if (rank < threshold * 2)
            {
                return 'B';
            }
            else if (rank < threshold * 3)
            {
                return 'C';
            }
            else if (rank < threshold * 4)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }

    }
}

