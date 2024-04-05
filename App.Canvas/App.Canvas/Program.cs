using System;
using App.Canvas.Helpers;
using Library.Canvas.Models;
namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentHelper = new StudentHelper();
            Console.WriteLine("Choose an action: ");
            Console.WriteLine("1. Add a student enrollment");
            Console.WriteLine("2. Exit");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                while (result != 2)
                {
                    if (result == 1)
                    {
                        studentHelper.CreateStudentRecord();
                    }
                    input = Console.ReadLine();
                    int.TryParse(input, out result);
                }
            }
        }
    }

}