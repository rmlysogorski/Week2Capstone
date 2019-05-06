using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Week2Capstone
{
    class Validator
    {
        #region Get Methods

        public static string GetString(string message, string error)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (input == string.Empty)
            {
                Console.WriteLine(error);
                return GetString(message, error);
            }
            else
            {
                return input;
            }
        }

        public static int GetInt(string message, string error)
        {
            Console.Write(message);
            if(int.TryParse(Console.ReadLine(), out int number))
            {
                return number;
            }
            else
            {
                Console.WriteLine(error);
                return GetInt(message, error);
            }
             
        }

        public static DateTime GetDateTime()
        {
            Console.Write("Due Date: ");
            string dueD = Console.ReadLine();
            
            if (dueD == string.Empty)
            {
                return DateTime.Now;
                
            }
            else if (!Regex.IsMatch(dueD, @"^\d{1,2}\/\d{1,2}\/\d{4}$"))
            {
                Console.WriteLine("Please enter the date in the format MM/DD/YYYY.");
                return GetDateTime();
            }
            else
            {
                try
                {
                    return DateTime.Parse(dueD);
                }
                catch(Exception)
                {
                    Console.WriteLine("Please enter a valid date.");
                    return GetDateTime();
                }                
            }
        }

        public static bool GetUserChoice(string opt1 = "y", string opt2 = "n")
        {
            string input = Console.ReadLine();
            if (input.ToLower() == opt1)
            {
                return true;
            }
            else if (input.ToLower() == opt2)
            {
                return false;
            }
            else
            {
                Console.Write($"Please enter {opt1} for yes or {opt2} for no: ");
                return GetUserChoice(opt1, opt2);
            }
        }

        #endregion

        #region Validate Methods

        public static int ValidateTaskChoice(int listLength, string message = "Enter your choice: ", string error = "Please enter a valid number.")
        {
            int number = GetInt(message, error);
            if(number < 1 || number > listLength)
            {
                Console.WriteLine(error);
                return ValidateTaskChoice(listLength, message, error);
            }
            else
            {
                return number;
            }            
        }
       
        public static string ValidateTaskName(string message, string error)
        {
            string input = GetString(message, error);
            if (Regex.IsMatch(input, @"((?i)[^a-z ])+"))
            {
                Console.WriteLine(error);
                return ValidateTaskName(message, error);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateTaskDesc(string message, string error)
        {
            string input = GetString(message, error);
            if (Regex.IsMatch(input, @"((?i)[^a-z0-9$#.-_?\\/ ]{1,120})"))
            {
                Console.WriteLine(error);
                return ValidateTaskDesc(message, error);
            }
            else
            {
                return input;
            }
        }

        #endregion
    }
}