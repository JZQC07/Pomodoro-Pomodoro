﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PomodoroLibrary;

namespace Pomodoro_Project
{

    class ToDoList
    {

        static void Main(string[] args)
        {
            PomodoroLibrary.TaskItem taskItem = new PomodoroLibrary.TaskItem();
            PomodoroLibrary.TaskList taskList = new PomodoroLibrary.TaskList();

            taskList = new TaskList();
            Menu myMenu = new Menu();
            myMenu.MainMenu();
        }

    }
    class Menu
    {
        TaskList task = new TaskList();
        TaskItem item = new TaskItem();
        public void MainMenu()
        {
            bool menu = true;

            while (menu == true)
            {
                Console.WriteLine("***POMODORO***\n\n1.[Visa ToDo-listan]\n2.[Lägg till i ToDo-listan]\n3.[Ta bort från ToDo-listan]\n4.[Lägg till avklarad aktivitet]\n5.[Visa avklarade aktiviteter]\n6.[Exit]");
                int answer = Int32.Parse(Console.ReadLine());

                switch (answer)
                {
                    case 1:
                        task.DisplayList();

                        break;
                    case 2:

                        bool subMenu = true;
                        while (subMenu == true)
                        {
                            try
                            {

                                Console.Write("\nPresets:\n1.[Städa]\n2.[Träna]\n3.[Studera]\n4.[Annat]\n5.[Tillbaka till huvudmenyn]");
                                int choice = Int32.Parse(Console.ReadLine());

                                switch (choice)
                                {

                                    case 1:

                                        item.Title = "Städa";
                                        task.Add(item);
                                        subMenu = false;
                                        break;

                                    case 2:
                                        item.Title = "Träna";
                                        task.Add(item);
                                        subMenu = false;
                                        break;

                                    case 3:
                                        item.Title = "Studera";
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
                                    default:
                                        Console.WriteLine("Mata in giltligt alternativ!");
                                        break;

                                }

                            }
                            catch
                            {
                                Console.WriteLine("Du måste skriva in någon av siffrorna.");
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                        break;
                    case 3:


                        if (task.Count < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Det finns inget inskrivet i listan.");
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

                        if (item.NumberOfTasks != 0)
                        {
                            task.DisplayList();
                            Console.Write("Skriv in nummer för vilken aktivitet som är avklarad: ");
                            int index = Int32.Parse(Console.ReadLine());

                            //.AddToFinished(index);

                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Finns inga inskrivna aktiviteter.");
                            Console.ReadKey();
                        }


                        break;
                    case 5:

                        //SKRIV UT FÄRDIGA TODOS

                        break;

                    case 6:

                        menu = false;

                        break;
                }


            }
        }
    }
}
