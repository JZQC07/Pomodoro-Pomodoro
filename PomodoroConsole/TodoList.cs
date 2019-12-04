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
    TaskItem item = new TaskItem();
    TaskList HistoryList = new TaskList();
    PomodoroLibrary.PomodoroTimer timer;

    public void MainMenu()
    {
      bool menu = true;

      while (menu == true)
      {
        Console.WriteLine("***POMODORO***\n\n1.[Show ToDo-list]\n2.[Add to ToDo-list]\n3.[Remove from ToDo-list]\n4.[Add to finished activity]\n5.[Show finsihed activities]\n6.[Start activity]\n7.[Exit]");
        int answer = Int32.Parse(Console.ReadLine());

        switch (answer)
        {
          case 1:
            if (task.Count < 1)
            {
              Console.WriteLine("Finns inget i listan!");
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

                Console.Write("\nPresets:\n1.[Clean]\n2.[Train]\n3.[Study]\n4.[Other]\n5.[Back to main menu]");
                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {

                  case 1:

                    item.Title = "Clean";
                    task.Add(item);
                    subMenu = false;
                    break;

                  case 2:
                    item.Title = "Workout";
                    task.Add(item);
                    subMenu = false;
                    break;

                  case 3:
                    item.Title = "Study";
                    task.Add(item);
                    subMenu = false;
                    break;

                  case 4:
                    task.NewTaskItem();
                    subMenu = false;
                    break;
                  case 5:
                    subMenu = false;
                    break;
                  case 6:

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
              Console.WriteLine("The list contains no values");
              Console.ReadKey();

            }
            else
            {
              task.RemoveTaskItem(); //Skickas till removetaskitem metoden
              MainMenu();

            }

            menu = false;

            break;
          case 4:         /*-----------------------SKALL BÖRJA EDITA HÄR--------------------------*/

            if (item.NumberOfTasks < 1)
            {
              task.DisplayList();
              bool work = false;
              int index = 0;
              do
              {
                try
                {
                  Console.Write("Enter the number you would mark as finished: ");
                  index = Int32.Parse(Console.ReadLine());
                }
                catch (System.Exception)
                {

                  Console.WriteLine("Enter a valid number!");
                  work = true;

                }

                HistoryList.Add(task[index]);
                task.RemoveAt(index);
              }
              while (work == true);
            }
            else
            {
              Console.Clear();
              Console.Write("There are no activities to be shown");
              Console.ReadKey();
            }

            break;
          case 5:
            Console.Clear();
            HistoryList.DisplayList();
            Console.ReadKey();

            break;

          case 6:

            if (item.NumberOfTasks < 1)
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
              Console.Write("Finns inga inskrivna aktiviteter.");
              Console.ReadKey();
            }

            Console.WriteLine("Would you like to add a timer to your task? [Y/N]");
            string usingTimer = Console.ReadLine().ToUpper();

            if (usingTimer == "Y")
            {
              //Sätter värdet på timern
              int setTaskMinutes = 0;
              int setTaskSeconds = 0;
              int setBreakMinutes = 0;
              int setBreakSeconds = 0;
              Console.WriteLine("Hur många minuter vill du fokusera på din aktivitet(Min 0 minuter max 30)?");
              setTaskMinutes = int.Parse(Console.ReadLine());
              //Ifsatser kontrollerar att värdet är ok
              if (setTaskMinutes < 0 || setTaskMinutes > 30)
              {
                setTaskMinutes = 0;
                Console.WriteLine("Felaktigt värde, värdet sätts till 0");

              }
              Console.WriteLine("Hur många sekunder vill du fokusera på din aktivitet?");
              setTaskSeconds = int.Parse(Console.ReadLine());
              if (setTaskSeconds < 0 || setTaskSeconds > 60)
              {
                setTaskSeconds = 0;
                Console.WriteLine("Felaktigt värde, värdet sätts till 0");

              }
              Console.WriteLine("Hur många minuter vill du sätta din rast till?(Min 0 minuter max 30)");
              setBreakMinutes = int.Parse(Console.ReadLine());
              if (setBreakMinutes < 0 || setBreakMinutes > 30)
              {
                setBreakMinutes = 0;
                Console.WriteLine("Felaktigt värde, värdet sätts till 0");

              }
              Console.WriteLine("Hur många sekunder vill du sätta din rast till?");
              setBreakSeconds = int.Parse(Console.ReadLine());
              if (setBreakSeconds < 0 || setBreakSeconds > 60)
              {
                setBreakSeconds = 0;
                Console.WriteLine("Felaktigt värde, värdet sätts till 0");
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
              Console.WriteLine("Timer starts now");
              Console.ReadLine();
              timer.StartBreakTimer();
              Console.ReadLine();
              //Åter till huvudmenyn
              MainMenu();

            }
            else
            {
              menu = false;
            }

            break;

          case 7:

            menu = false;
            break;
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
        Console.WriteLine("Du har utfört uppgiften! Tryck på valfri tangent för att fortsätta..");
      }
    }
  }
}
