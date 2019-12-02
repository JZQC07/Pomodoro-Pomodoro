using System;
using System.Collections.Generic;
using System.Linq;

namespace Pomodoro_Project
{
    class ToDoList
    {

        static void Main(string[] args)
        {
            PomodoroLibrary.PomodoroLib pomoLib = new PomodoroLibrary.PomodoroLib();

            pomoLib.TaskItem.Add("0", "Plugga", "C# For experts", 25);
            
        }

    }
    class Menu
    {
        bool menu = true;

            while(menu == true)
            {

                try
                {

                    Console.Clear();
                    Console.WriteLine("***POMODORO***\n\n1.[Visa ToDo-listan]\n2.[Lägg till i ToDo-listan]\n3.[Ta bort från ToDo-listan]\n4.[Lägg till avklarad aktivitet]\n5.[Visa avklarade aktiviteter]\n6.[Exit]");
                    int answer = Int32.Parse(Console.ReadLine());
                    Console.Clear();

                    string toDo;
                    

                    switch (answer)
                    {
                        case 1:

                            pomoLib.DisplayList();
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
                                            TaskItem.item = "Städa";
                                            pomolib.AddToDoList(TaskItem.item);
                                            subMenu = false;
                                            break;

                                        case 2:
                                            TaskItem.item = "Träna";
                                            pomolib.AddToDoList(TaskItem.item);
                                            subMenu = false;
                                            break;

                                        case 3:
                                            toDoObject.item = "Studera";
                                            pomolib.AddToDoList(TaskItem.item);
                                            subMenu = false;
                                            break;

                                        case 4:
                                            Console.Write("-----------------\nSkriv in aktivitet: ");
                                            toDo = Console.ReadLine();
                                            pomolib.AddToDoList(toDo);
                                            subMenu = false;
                                            break;
                                        case 5:
                                            subMenu = false;
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


                            if (pomoLib.antaliindexilistan == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Det finns inget inskrivet i listan.");
                                Console.ReadKey();

                            }
                            else
                            {
                                pomoLib.RemoveTaskItem(); //Skickas till removetaskitem metoden

                            }

                            menu = false;

                            break;
                        case 4:         /*-----------------------SKALL BÖRJA EDITA HÄR--------------------------*/

                            if (Pomodoro.ToDoList.Count != 0)
                            {
                                Pomodoro.PrintToDoList();
                                Console.Write("Skriv in vilken aktivitet som är avklarad: ");
                                int index = Int32.Parse(Console.ReadLine());

                                Pomodoro.AddToFinished(index);

                            }
                            else
                            {
                                Console.Clear();
                                Console.Write("Finns inga inskrivna aktiviteter.");
                                Console.ReadKey();
                            }


                            break;
                        case 5:

                            Pomodoro.ShowHistoryList();

                            break;

                        case 6:

                            menu = false;

                            break;
                    }

                }
                catch
                {

                    Console.WriteLine("----------------\nDu måste skriva in någon av siffrorna i menyn.");
                    Console.ReadKey();

                }




            }      
   
    }   
