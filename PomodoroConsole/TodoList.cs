using System;
using System.Collections.Generic;
using System.Linq;
using Pomodoro_Project;

namespace Pomodoro_Project
{
    class Program
    {

        static void Main(string[] args)
        {

            ToDo Pomodoro = new ToDo();

            ToDoItem toDoObject = new ToDoItem();

            bool menu = true;

            while (menu == true)
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

                            Pomodoro.PrintToDoList();
                            Console.ReadKey();

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
                                            toDoObject.item = "Städa";
                                            Pomodoro.AddToDoList(toDoObject.item);
                                            subMenu = false;
                                            break;

                                        case 2:
                                            toDoObject.item = "Träna";
                                            Pomodoro.AddToDoList(toDoObject.item);
                                            subMenu = false;
                                            break;

                                        case 3:
                                            toDoObject.item = "Studera";
                                            Pomodoro.AddToDoList(toDoObject.item);
                                            subMenu = false;
                                            break;

                                        case 4:
                                            Console.Write("-----------------\nSkriv in aktivitet: ");
                                            toDo = Console.ReadLine();
                                            Pomodoro.AddToDoList(toDo);
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


                            if (Pomodoro.ToDoList.Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Det finns inget inskrivet i listan.");
                                Console.ReadKey();

                            }
                            else
                            {
                                Pomodoro.PrintToDoList();
                                Console.Write("----------------\nSkriv in numret framför den aktivitet du vill ta bort från listan: ");
                                int delete = Int32.Parse(Console.ReadLine());
                                Pomodoro.DeleteFromList(delete);

                            }

                            menu = false;

                            break;
                        case 4:

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
    }



    public class ToDoItem
    {
        public string item;

    }
    public class ToDo : ToDoItem
    {

        public List<string> ToDoList = new List<string>();
        public List<string> HistoryList = new List<string>();

        public void AddToDoList(string item)
        {

            ToDoList.Add(item);

        }
        public void DeleteFromList(int delete)
        {

            //Subtraherar med 1 för att få indexet att hamna i rätt position i listan
            ToDoList.RemoveAt(delete - 1);
            HistoryList.RemoveAt(delete - 1);



        }

        public void PrintToDoList()
        {

            if (ToDoList.Count != 0)
            {

                int nr = 0;

                foreach (string elements in ToDoList)
                {
                    nr++;
                    Console.Write(nr + ". " + elements + "\n");
                }

            }
            else
            {

                Console.Write("Finns inget i listan.\n");
            }

        }

        public void AddToFinished(int index)
        {

            //Subtraherar indexet med 1 för att hamna på rätt index i listan

            //Sätter in aktiviteten från ToDoList i HistoryList vid det bestämda indexet            
            HistoryList.Add(ToDoList[index - 1]);

            //Tar bort aktiviteten från ToDoList
            ToDoList.RemoveAt(index - 1);


        }

        public void ShowHistoryList()
        {


            int nr = 0;

            if (ToDoList.Equals(HistoryList) == false && HistoryList.Count != 0)
            {

                foreach (string elements in HistoryList)
                {

                    nr++;
                    Console.Write(nr + ". " + elements + "\n");


                }

                Console.ReadKey();
            }
            else
            {

                Console.Write("Finns inga avklarade aktiviteter i listan.");
                Console.ReadKey();

            }

        }

    }

}
