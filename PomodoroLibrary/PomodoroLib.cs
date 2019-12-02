using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
namespace PomodoroLibrary
{
    //TODO  Metod för user
    //TODO: Färdiga Presets (Intervaller, studera, städa osvosv)
    //TODO: Menu
    //TODO: History over finished activities

    /*-----------------------------------Stack Overflow------------------------------------*/
    public class ToDoListMethods
    {
        public class TaskItem
        {
            public int Number { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; }
            public double Timer { get; set; }

            public TaskItem()
            {

            }
            public TaskItem(string title)
            {
                Title = title;
            }

            public TaskItem(string title, string desc)
            {
                Title = title;
                Description = desc;
                IsCompleted = false;
            }
            public TaskItem(int number, string title, string desc)
            {
                Number = number;
                Title = title;
                Description = desc;
                IsCompleted = false;
            }
            public TaskItem(int number, string title, string desc, double timer)
            {
                Number = number;
                Title = title;
                Description = desc;
                Timer = timer;
                IsCompleted = false;
            }
        }

        public class TaskList : List<TaskItem>
        {
            public TaskList()
            {

            }

            public void Add(string title, string desc)
            {
                int numberOfTasks = this.Count();
                int number = numberOfTasks++;

                this.Add(new TaskItem(number, title, desc));
            }

            public void DisplayList()
            {
                //Console.CLear();
                //Console.WriteLine("\t Task List");
                Console.WriteLine();
                Console.WriteLine("Num |  Title  | Description  | Timer |");
                Console.WriteLine("--------------------------------------");
                foreach (var t in this)
                {
                    Console.WriteLine("{0}     {1}\t{2}\t{3}", t.Number.ToString(),
                                                         t.Title,
                                                         t.Description,
                                                         t.Timer);
                }
            }

            public void NewTaskItem()
            {
                string title = String.Empty;
                string desc = String.Empty;

                Console.Write("Enter new task Title: ");
                title = Console.ReadLine().Trim();
                Console.WriteLine("{0}\n", title);

                Console.Write("Enter new task Description: ");
                desc = Console.ReadLine();
                Console.WriteLine("\"{0}\"", desc);

                this.Add(title, desc);
            }
            public void RemoveTaskItem()
            {
                Console.WriteLine("Mata in nummer för vilken task du vill ta bort från din lista: ");
                DisplayList();
                int removeChoice = Int32.Parse(Console.ReadLine());
                this.RemoveAt(removeChoice - 1);        
            }
        }

        /*-----------------------------------------------STACK OVERFLOW-------------------------------------------*/

        //Kunna anropa metoden från TodoList.cs
    }

    /// <summary>
    /// Timer Class
    /// </summary>
    public class Pomodoro
    {
        ///Adds delegate to refer to methods 
        public delegate void focusEventDelegate(int focusMinutes, int focusSeconds);
        public delegate void breakEventDelegate(int breakMinutes, int breakSeconds);

        // Create an objet for delegate
        public focusEventDelegate onFocus;
        public breakEventDelegate onBreak;

        //Creates timer objects
        Timer taskTimer;
        Timer breakTimer;

        //Properties
        private int setTaskMinutes { get; set; }
        private int setTaskSeconds { get; set; }
        private int setBreakMinutes { get; set; }
        private int setBreakSeconds { get; set; }

        // Constructor with in-parameters
        public Pomodoro(int setTaskMinutes, int setTaskSeconds, int setBreakMinutes, int setBreakSeconds)
        {
            this.setTaskMinutes = setTaskMinutes;
            this.setTaskSeconds = setTaskSeconds;
            this.setBreakMinutes = setBreakMinutes;
            this.setBreakSeconds = setBreakSeconds;
        }

        //Method that starts tasktimer, set to 1 sec for each elapse
        public void startTaskTimer()
        {
            taskTimer = new Timer(1000);
            taskTimer.Elapsed += taskEvent;
            taskTimer.Start();
        }

        //Stops tasktimer
        public void stopTaskTimer()
        {
            taskTimer.Stop();
        }

        //Method that is raised each time tasktimer elapses
        private void taskEvent(object sender, EventArgs e)
        {
            Console.Clear();
            onFocus.Invoke(this.setTaskMinutes, this.setTaskSeconds);

            if (this.setTaskMinutes <= 0 && this.setTaskSeconds <= 0)
            {
                stopTaskTimer();
            }

            this.setTaskSeconds--;
            if (this.setTaskSeconds <= -1)
            {
                this.setTaskMinutes--;
                this.setTaskSeconds = 59;
            }
        }

        //Method that starts breaktimer, set to 1 sec for each elapse
        public void startBreakTimer()
        {
            breakTimer = new Timer(1000);
            breakTimer.Elapsed += breakEvent;
            breakTimer.Start();
        }

        //Stops break timer
        public void stopBreakTimer()
        {
            breakTimer.Stop();
        }

        //Method that is raised each time breaktimer elapses
        private void breakEvent(object sender, EventArgs e)
        {
            Console.Clear();
            onBreak.Invoke(this.setBreakMinutes, this.setBreakSeconds);


            if (this.setBreakMinutes <= 0 && this.setBreakSeconds <= 0)
            {
                stopBreakTimer();
            }

            this.setBreakSeconds--;
            if (this.setBreakSeconds <= -1)
            {
                this.setBreakMinutes--;
                this.setBreakSeconds = 59;
            }
        }
    }
}
