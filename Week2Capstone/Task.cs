using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Week2Capstone
{
    class Task
    {
        #region Properties
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime Due { get; set; }
        public bool Completion { get; set; }
        #endregion

        #region Constructors
        public Task(string tName, string tDesc, DateTime tDue, bool tComplete = false)
        {
            Name = tName;
            Desc = tDesc;
            Due = tDue;
            Completion = tComplete;
        }

        public Task() { }
        #endregion

        #region Display Menus
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(
                "What would you like to do?\n" +
                "\t1. List tasks\n" +
                "\t2. Add task\n" +
                "\t3. Edit task\n" +
                "\t4. Delete task\n" +
                "\t5. Mark task complete\n" +
                "\t6. Quit");
        }

        public static void PrintDisplayMenu()
        {
            Console.WriteLine("Would you like to:\n" +
                "\t1.Display all tasks\n" +
                "\t2.Display tasks for only one team member\n" +
                "\t3.Display tasks by due date\n" +
                "\t4.Return to the main menu");
        }

        public static void PrintEditMenu()
        {
            Console.WriteLine("Would you like to edit:\n" +
                "\t1. All of the information\n" +
                "\t2. The name\n" +
                "\t3. The due date\n" +
                "\t4. The task description\n" +
                "\t5. Return to main menu");
        }

        #endregion

        #region DisplayTasks Methods

        public static void DisplayTasks(List<Task> tList)
        {
            Console.Clear();
            Console.WriteLine("LIST TASKS");
            PrintDisplayMenu();
            int choice = Validator.ValidateTaskChoice(4);

            switch (choice)
            {
                case 1: PrintList(tList);                    
                    break;
                case 2: PrintListByName(tList);                    
                    break;
                case 3: PrintListByDate(tList);                    
                    break;
                case 4: //Return to the Main Menu
                    break;
                default:
                    break;
            }
            Pause();

        }

        public static void PrintHeader()
        {
            string header = string.Format("{0,-10}{1,-15}{2,-20}{3,-18}{4,-20}",
                "Task#", "Complete?", "Due Date", "Team Member", "Description");
            string underLine = string.Format("{0,-10}{1,-15}{2,-20}{3,-18}{4,-20}",
                "-----", "---------", "--------", "-----------", "-----------");
            Console.WriteLine("\n" + header + "\n" + underLine);
        }
                
        public static void PrintList(List<Task> tList)
        {
            PrintHeader();
            int count = 1;
            foreach (Task task in tList)
            {
                //write the first line
                Console.WriteLine(string.Format("{0, 3}{1,7}", $"{count}", " ") + task.ToString());
                //if there's more than one name or a last name
                if (task.Name.Split()[task.Name.Split().Length - 1] != null) 
                {
                    //print the other names on a new formatted line
                    for(int i = 1; i < task.Name.Split().Length; i++)
                    {
                        Console.WriteLine(string.Format("{0, 46}{1,-17}"," ", task.Name.Split()[i]));
                    }
                    
                }
                count++;
            }
        }

        public static void PrintListByName(List<Task> tList)
        {
            string input = Validator.ValidateTaskName("Please enter team member's name: ", "Names should contain only letters.");
            Console.WriteLine();
            PrintHeader();
            int count = 1;
            foreach (Task task in tList)
            {
                if (task.Name.ToLower().Contains(input.ToLower())) //only print names the user entered
                {
                    Console.WriteLine(string.Format("{0, 3}{1,7}", $"{count}", " ") + task.ToString());
                    if (task.Name.Split()[task.Name.Split().Length - 1] != null)
                    {
                        for (int i = 1; i < task.Name.Split().Length; i++)
                        {
                            Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", task.Name.Split()[i]));
                        }

                    }
                    count++;
                }
            }
        }

        public static void PrintListByDate(List<Task> tList)
        {
            DateTime input = Validator.GetDateTime();
            Console.WriteLine();
            PrintHeader();
            int count = 1;
            foreach (Task task in tList)
            {
                if (input >= task.Due) //only print dates up to the user input date
                {
                    Console.WriteLine(string.Format("{0, 3}{1,7}", $"{count}", " ") + task.ToString());
                    if (task.Name.Split()[task.Name.Split().Length - 1] != null)
                    {
                        for (int i = 1; i < task.Name.Split().Length; i++)
                        {
                            Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", task.Name.Split()[i]));
                        }

                    }
                    count++;
                }
            }
        }

        public override string ToString()
        {
            //0 = Completion Status, 1 = Due Date, 2 = Name, 3 = Description
            //4 = whitespace for formatting purposes
            return string.Format("{0,5}{4,9}{1,-19}{4,3}{2,-17}{3,-20}", 
                Completion ? "Yes" : "No ", $"{ Due: MM/dd/yyyy}", $"{Name.Split()[0]}", $"{Desc}", " ");
        }

        #endregion

        #region AddTask Methods

        public static void AddTask(List<Task> tList)
        {
            Console.Clear();
            Console.WriteLine("ADD TASK");
            string name = Validator.ValidateTaskName("Team Member Name: ", "Names should contain only letters.");
            name = Regex.Replace(name, @"\s+", " "); //Remove extra whitespace
            string desc = Validator.ValidateTaskDesc("Task Description: ", "Please enter a brief description.");
            DateTime due = Validator.GetDateTime();

            Task t = new Task(name, desc, due);
            tList.Add(t);

            tList.Sort((x, y) => x.Due.CompareTo(y.Due)); //Sort so we can display an accurate Task#
            PrintHeader();
            Console.WriteLine(string.Format("{0, 3}{1,7}", $"{tList.IndexOf(t) + 1}", " ") + t.ToString());
            if (t.Name.Split()[t.Name.Split().Length - 1] != null)
            {
                for (int i = 1; i < t.Name.Split().Length; i++)
                {
                    Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", t.Name.Split()[i]));
                }

            }
            Pause();
        }

        #endregion

        #region EditTask Methods

        public static void EditTask(List<Task> tList)
        {
            Console.Clear();
            Console.WriteLine("EDIT TASK");
            PrintList(tList);

            Console.WriteLine("\nWhich task would you like to edit?");
            int taskEdit = Validator.ValidateTaskChoice(tList.Count) - 1;
            Task t = tList[taskEdit]; //A place holder to find the new index

            Console.Clear();
            PrintHeader();
            Console.WriteLine(string.Format("{0, 3}{1,7}", $"{taskEdit + 1}", " ") + tList[taskEdit].ToString());
            if (tList[taskEdit].Name.Split()[tList[taskEdit].Name.Split().Length - 1] != null)
            {
                for (int i = 1; i < tList[taskEdit].Name.Split().Length; i++)
                {
                    Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", tList[taskEdit].Name.Split()[i]));
                }

            }
            PrintEditMenu();
            int choice = Validator.ValidateTaskChoice(5);

            switch (choice)
            {
                case 1: EditAllInfo(tList, taskEdit);
                    break;
                case 2: EditName(tList, taskEdit);
                    break;
                case 3: EditDueDate(tList, taskEdit);
                    break;
                case 4: EditDesc(tList, taskEdit);
                    break;
                case 5: //Return to the main menu
                    break;
                default: break;
            }

            Console.WriteLine();
            tList.Sort((x, y) => x.Due.CompareTo(y.Due));
            int index = tList.IndexOf(t); //find the index of the same task that was edited to display
            PrintHeader();
            Console.WriteLine(string.Format("{0, 3}{1,7}", $"{index + 1}", " ") + tList[index].ToString());
            if (tList[index].Name.Split()[tList[index].Name.Split().Length - 1] != null)
            {
                for (int i = 1; i < tList[index].Name.Split().Length; i++)
                {
                    Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", tList[index].Name.Split()[i]));
                }

            }
            Pause();
        }
        
        public static void EditAllInfo(List<Task> tList, int taskEdit)
        {
            tList[taskEdit].Name = Validator.ValidateTaskName("New Team Member Name: ", "Names should contain only letters.");
            tList[taskEdit].Desc = Validator.ValidateTaskDesc("New Task Description: ", "Please enter a brief (120 characters) description.");
            tList[taskEdit].Due = Validator.GetDateTime();
        }

        public static void EditName(List<Task> tList, int taskEdit)
        {
            tList[taskEdit].Name = Validator.ValidateTaskName("New Team Member Name: ", "Names should contain only letters.");
        }

        public static void EditDueDate(List<Task> tList, int taskEdit)
        {
            tList[taskEdit].Due = Validator.GetDateTime();
        }

        public static void EditDesc(List<Task> tList, int taskEdit)
        {
            tList[taskEdit].Desc = Validator.ValidateTaskDesc("New Task Description: ", "Please enter a brief (120 characters) description.");
        }

        #endregion

        #region DeleteTask Methods

        public static void DeleteTask(List<Task> tList)
        {
            Console.Clear();
            Console.WriteLine("DELETE TASK");
            PrintList(tList);
            Console.WriteLine("\nWhich task would you like to delete?");
            int choice = Validator.ValidateTaskChoice(tList.Count);
            Console.Clear();
            PrintHeader();
            Console.WriteLine(string.Format("{0, 3}{1,7}", $"{choice}", " ") + tList[choice - 1].ToString());            
            if (tList[choice - 1].Name.Split()[tList[choice - 1].Name.Split().Length - 1] != null)
            {
                for (int i = 1; i < tList[choice - 1].Name.Split().Length; i++)
                {
                    Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", tList[choice - 1].Name.Split()[i]));
                }

            }
            Console.Write("\nYou are about to delete this task permanently. Proceed? (y/n): ");
            bool proceed = Validator.GetUserChoice();
            if (proceed)
            {
                tList.RemoveAt(choice - 1);
                Console.WriteLine("The task has been deleted.");
            }
            Pause();
        }

        #endregion

        #region MarkTaskComplete Methods

        public static void MarkTaskComplete(List<Task> tList)
        {
            Console.Clear();
            Console.WriteLine("MARK TASK COMPLETE");
            PrintList(tList);
            Console.WriteLine("\nWhich task would you like to mark complete?");
            int choice = Validator.ValidateTaskChoice(tList.Count);

            Console.Clear();
            PrintHeader();
            Console.WriteLine(string.Format("{0, 3}{1,7}", $"{choice}", " ") + tList[choice - 1].ToString());
            if (tList[choice - 1].Name.Split()[tList[choice - 1].Name.Split().Length - 1] != null)
            {
                for (int i = 1; i < tList[choice - 1].Name.Split().Length; i++)
                {
                    Console.WriteLine(string.Format("{0, 46}{1,-17}", " ", tList[choice - 1].Name.Split()[i]));
                }

            }
            Console.Write("\nYou are about to mark this task as completed. Proceed? (y/n): ");
            bool proceed = Validator.GetUserChoice();

            if (proceed)
            {
                tList[choice - 1].Completion = true;
                Console.WriteLine("This task has been marked as completed.");
            }

            Pause();
        }

        #endregion

        #region Get & Write Data

        public static List<Task> GetDataFromFile()
        {
            try
            {
                List<Task> tList = new List<Task>();

                StreamReader file = new StreamReader(@"tList.bin");
                string text = file.ReadToEnd();

                foreach (string line in text.Split('\n'))
                {
                    if (line.Split('\t')[0] != string.Empty)
                    {
                        Task tAdd = new Task(
                            line.Split('\t')[0], 
                            line.Split('\t')[1], 
                            DateTime.Parse(line.Split('\t')[2]), 
                            bool.Parse(line.Split('\t')[3])
                            );

                        tList.Add(tAdd);
                    }

                }

                file.Close();

                return tList;
            }
            catch(IOException I)
            {
                Console.WriteLine(I.Message);
                List<Task> blank = new List<Task>();
                return blank;
            } 
        }

        public static void WriteData(List<Task> tList)
        {
            try
            {
                TextWriter tw = new StreamWriter(@"tList.bin");

                foreach (Task t in tList)
                {
                    tw.Write(t.Name);
                    tw.Write("\t");
                    tw.Write(t.Desc);
                    tw.Write("\t");
                    tw.Write(t.Due);
                    tw.Write("\t");
                    tw.Write(t.Completion);
                    tw.Write("\n");
                }

                tw.Close();

            }
            catch (IOException E)
            {
                Console.WriteLine(E.Message);
            }
            return;
        }

        #endregion
        
        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
        }
    } 
}
