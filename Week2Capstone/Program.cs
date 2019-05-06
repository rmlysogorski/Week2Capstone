using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tList = new List<Task>();
            int menuItemsNum = 6;
            bool runAgain = true;

            while (runAgain)
            {                
                Task.DisplayMenu();
                tList = Task.GetDataFromFile();
                tList.Sort((x, y) => x.Due.CompareTo(y.Due));
                string menuMessage = "Enter your choice: ", menuError = $"Please enter a number 1-{menuItemsNum}";
                int choice = Validator.ValidateTaskChoice(menuItemsNum, menuMessage, menuError);                
                switch (choice)
                {
                    case 1:
                        Task.DisplayTasks(tList);
                        break;
                    case 2:
                        Task.AddTask(tList);
                        break;
                    case 3:
                        Task.EditTask(tList);
                        break;
                    case 4:
                        Task.DeleteTask(tList);
                        break;
                    case 5:
                        Task.MarkTaskComplete(tList);
                        break;
                    case 6: runAgain = false;
                        Console.Write("Are you sure you want to quit? (y/n): ");
                        runAgain = !Validator.GetUserChoice();
                        break;
                    default: break;
                }                

                Task.WriteData(tList);
                Console.WriteLine("Have a great day!\n");
            }            
        }
    }
}
