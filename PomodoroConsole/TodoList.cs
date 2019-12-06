using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PomodoroLibrary;
using System.Timers;


namespace Pomodoro_Project
{

    class ToDoList
    {
        static void Main(string[] args)
        {
            Menu myMenu = new Menu();
            myMenu.MainMenu();
        }
    }
    class Menu
    {
        TaskList task = new TaskList();
        TaskList HistoryList = new TaskList();
        PomodoroLibrary.PomodoroTimer timer;

        public void MainMenu()
        {
            bool menu = true;

            while (menu == true)
            {
                Console.WriteLine("***POMODORO***\n\n1.[Show ToDo-list]\n2.[Add to ToDo-list]\n3.[Remove from ToDo-list]\n4.[Add to finished activity]\n5.[Show finsihed activities]\n6.[Start activity]\n7.[Exit]");
                string stringAnswer = Console.ReadLine();
                int intAnswer;
                if (int.TryParse(stringAnswer, out intAnswer)) //Kollar så att användaren skrivit in giltligt nummer.
                {
                    switch (intAnswer)
                    {
                        case 1:
                            if (task.Count < 1)
                            {
                                Console.WriteLine("There is nothing in your list!");
                            }
                            else
                            {
                                Console.Clear();
                                task.DisplayList();
                            }

                            break;
                        case 2:

                            bool subMenu = true;
                            while (subMenu == true)
                            {
                                try
                                {
                                    Console.Write("\nPresets:\n1.[Clean]\n2.[Workout]\n3.[Study]\n4.[Other]\n5.[Back to main menu]");
                                    int choice = Int32.Parse(Console.ReadLine());
                                    TaskItem newItem = new TaskItem();

                                    switch (choice)
                                    {

                                        case 1:
                                            newItem.Title = "Clean";
                                            task.NewTaskItem(newItem);
                                            subMenu = false;
                                            break;

                                        case 2:
                                            newItem.Title = "Workout";
                                            task.NewTaskItem(newItem);
                                            subMenu = false;
                                            break;

                                        case 3:
                                            newItem.Title = "Study";
                                            task.NewTaskItem(newItem);
                                            subMenu = false;
                                            break;

                                        case 4:
                                            task.NewTaskItem();
                                            subMenu = false;
                                            break;
                                        case 5:
                                            subMenu = false;
                                            break;
                                        default:
                                            Console.WriteLine("Please enter the correct alternative!");
                                            break;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("You have to enter a valid number");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;
                        case 3:

                            if (task.Count < 1)
                            {
                                Console.Clear();
                                Console.WriteLine("The list contains no values. Press a key to return to Main Menu.");
                                Console.ReadKey();
                                MainMenu();
                            }
                            else
                            {
                                try
                                {
                                    task.RemoveTaskItem();
                                    MainMenu();

                                }
                                catch
                                {
                                    Console.WriteLine("Choose a correct number for the activity you would like to remove.");
                                    Console.WriteLine("Press any key to return to Main Menu.");
                                    Console.ReadKey();
                                    MainMenu();
                                }
                            }

                            menu = false;

                            break;
                        case 4:

                            if (task.Count > 0)
                            {

                                task.DisplayList();
                                bool work = false;
                                int index;
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("Enter the number you would like to mark as finished: ");
                                        index = Int32.Parse(Console.ReadLine());

                                        HistoryList.AddToHistory(index, HistoryList, task);
                                        task.RemoveAt(index);
                                        //Tar bort aktiviteten från original listan.

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine("Enter a valid number!");
                                        work = true;
                                    }
                                }
                                while (work == true);
                            }
                            else
                            {
                                Console.Clear();
                                Console.Write("There are no activities to be shown.");
                                Console.WriteLine("Press any key to return to the Main Menu");
                                Console.ReadKey();
                                MainMenu();
                            }

                            break;
                        case 5:
                            Console.Clear();
                            if (HistoryList.Count > 0)
                            {
                                HistoryList.DisplayList();
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Write("There are no activities to be shown.");
                                Console.WriteLine("Press any key to return to the Main Menu"); ;
                                Console.ReadKey();
                                MainMenu();
                            }


                            break;

                        case 6:

                            if (task.Count > 0)
                            {
                                task.DisplayList();
                                bool work = false;
                                int index = 0;
                                do
                                {
                                    try
                                    {
                                        Console.Write("Enter the number you would like to start: ");
                                        index = Int32.Parse(Console.ReadLine());
                                    }
                                    catch (System.Exception)
                                    {

                                        Console.WriteLine("Enter a valid value!");
                                        work = true;

                                    }
                                }
                                while (work == true);
                            }
                            else
                            {
                                Console.Clear();
                                Console.Write("There are no activities in your list. Please add an activity before starting it.");
                                Console.ReadKey();
                                MainMenu();
                            }

                            try
                            {
                                Console.WriteLine("Would you like to add a timer to your task? [Y/N]");
                                string usingTimer = Console.ReadLine().ToUpper();

                                if (usingTimer == "Y")
                                {
                                    //Sätter värdet på timern
                                    int setTaskMinutes = 0;
                                    int setTaskSeconds = 0;
                                    int setBreakMinutes = 0;
                                    int setBreakSeconds = 0;
                                    Console.WriteLine("How many minutes do you want to focus on your activity?(Min 0, max 30)"); //
                                    setTaskMinutes = int.Parse(Console.ReadLine());
                                    //Ifsatser kontrollerar att värdet är ok
                                    if (setTaskMinutes < 0 || setTaskMinutes > 30)
                                    {
                                        setTaskMinutes = 0;
                                        Console.WriteLine("Wrong value! Minutes set to [0].");

                                    }
                                    Console.WriteLine("How many seconds do you want to focus on your activity?");
                                    setTaskSeconds = int.Parse(Console.ReadLine());
                                    if (setTaskSeconds < 0 || setTaskSeconds > 60)
                                    {
                                        setTaskSeconds = 0;
                                        Console.WriteLine("Wrong value! Seconds set to [0].");

                                    }
                                    Console.WriteLine("How many minutes do you want your brake to be?(Min 0, max 30");
                                    setBreakMinutes = int.Parse(Console.ReadLine());
                                    if (setBreakMinutes < 0 || setBreakMinutes > 30)
                                    {
                                        setBreakMinutes = 0;
                                        Console.WriteLine("Wrong value! Minutes set to [0].");

                                    }
                                    Console.WriteLine("How many seconds do you want your brake to be?(Min 0, max 30");
                                    setBreakSeconds = int.Parse(Console.ReadLine());
                                    if (setBreakSeconds < 0 || setBreakSeconds > 60)
                                    {
                                        setBreakSeconds = 0;
                                        Console.WriteLine("Wrong value! Seconds set to [0].");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Press any button to start your task");
                                    Console.ReadLine();
                                    //Skapar ett nytt objet av timerklassen Pomodoro samt anropar de metoder som krävs för att köra timern
                                    timer = new PomodoroLibrary.PomodoroTimer(setTaskMinutes, setTaskSeconds, setBreakMinutes, setBreakSeconds);
                                    //Två delegates som refereras från respektive start/break timer till metoden PrintTimer
                                    timer.addDelegateToFocus = PrintTimer;
                                    timer.addDelegateToBreak = PrintTimer;
                                    timer.StartTaskTimer();
                                    Console.WriteLine("Timer starts now! At any time enter [Q] to return to Main Menu.");
                                    Console.WriteLine("Or anything else to start your brake!");
                                    string cancel = Console.ReadLine().ToUpper();
                                    if (cancel == "Q")
                                    {
                                        MainMenu();
                                    }
                                    timer.StartBreakTimer();
                                    Console.WriteLine("Press any key to return to Main Menu.");
                                    Console.ReadLine();
                                    MainMenu();
                                    //Åter till huvudmenyn
                                }
                                else
                                {
                                    Console.WriteLine("You didnt set a timer. Press any key to return to Main Menu.");
                                    Console.ReadKey();
                                }

                            }
                            catch
                            {
                                Console.WriteLine("Enter a correct number! Try again!");
                            }
                            break;

                        case 7:

                            menu = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Choose a correct alternative! [1-7]");
                }
            }
        }

        public void PrintTimer(int TaskMinutes, int TaskSeconds)
        {
            Console.Clear();
            Console.WriteLine(TaskMinutes.ToString("00:") + TaskSeconds.ToString("00"));
            if (TaskMinutes <= 0 && TaskSeconds <= 0)
            {
                Console.Beep();
                Console.WriteLine("Congratulations! You have now completed your task!");
            }
        }
    }
}
