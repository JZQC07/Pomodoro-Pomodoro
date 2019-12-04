using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
namespace PomodoroLibrary
{
    //TODO: Print history over finished activities
    //TODO: I MENY KUNNA VÄLJA AKTIVITET ATT STARTA.
    //TODO: TIMER SKALL SÄTTAS FÖR DEN SPECIFIKA AKTIVITETEN.
    //TODO: TIMER SKALL STARTAS OCH STOPPAS. NÄR SLUT, TAS TILLBAKA TILL HUVUDMENYN.
    //TODO; OKRASCHBART


    /*-----------------------------------Stack Overflow------------------------------------*/

    public class TaskItem
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int NumberOfTasks {get; set;}

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
    }
    public class TaskList : List<TaskItem>
    {
        public TaskList()
        {

        }

        public void Add(string title, string desc)
        {
            
            int NumberOfTasks = this.Count();
            int number = NumberOfTasks++;
            

            this.Add(new TaskItem(number, title, desc));
        }

        public void DisplayList()
        {
            Console.WriteLine("\t Task List");
            Console.WriteLine();
            Console.WriteLine("Num |  Title  | Description  |");
            Console.WriteLine("------------------------------");
            foreach (TaskItem t in this)
            {
                Console.WriteLine("{0}       {1}\t{2}", t.Number.ToString(),
                                                     t.Title,
                                                     t.Description
                                                     );
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
            this.RemoveAt(removeChoice);
        }
        public void StartTask()
        {
            Console.WriteLine("Du startade timern för {0}", this);
        }

    /*-----------------------------------------------STACK OVERFLOW-------------------------------------------*/

    //Kunna anropa metoden från TodoList.cs
    }

    /// <summary>
    /// Timer Class
    /// </summary>
    public class PomodoroTimer
    {
        ///Adds delegate to refer to methods 
        public delegate void FocusEventDelegate(int focusMinutes, int focusSeconds);
        public delegate void BreakEventDelegate(int breakMinutes, int breakSeconds);

        // Create an objet for delegate
        public FocusEventDelegate addDelegateToFocus;
        public BreakEventDelegate addDelegateToBreak;

        //Creates timer objects
        Timer taskTimer;
        Timer breakTimer;

        //Properties
        private int setTaskMinutes { get; set; }
        private int setTaskSeconds { get; set; }
        private int setBreakMinutes { get; set; }
        private int setBreakSeconds { get; set; }

        // Constructor with in-parameters
        public PomodoroTimer(int setTaskMinutes, int setTaskSeconds, int setBreakMinutes, int setBreakSeconds)
        {
            this.setTaskMinutes = setTaskMinutes;
            this.setTaskSeconds = setTaskSeconds;
            this.setBreakMinutes = setBreakMinutes;
            this.setBreakSeconds = setBreakSeconds;
        }

        //Method that starts tasktimer, set to 1 sec for each elapse
        public void StartTaskTimer()
        {
            taskTimer = new Timer(1000);
            taskTimer.Elapsed += TaskEvent;
            taskTimer.Start();
        }

        //Stops tasktimer
        public void StopTaskTimer()
        {
            taskTimer.Stop();
        }

        //Method that is raised each time tasktimer elapses
        private void TaskEvent(object sender, EventArgs e)
        {
            addDelegateToFocus.Invoke(this.setTaskMinutes, this.setTaskSeconds);

            if (this.setTaskMinutes <= 0 && this.setTaskSeconds <= 0)
            {
                StopTaskTimer();
            }

            this.setTaskSeconds--;
            if (this.setTaskSeconds <= -1)
            {
                this.setTaskMinutes--;
                this.setTaskSeconds = 59;
            }
        }

        //Method that starts breaktimer, set to 1 sec for each elapse
        public void StartBreakTimer()
        {
            breakTimer = new Timer(1000);
            breakTimer.Elapsed += BreakEvent;
            breakTimer.Start();
        }

        //Stops break timer
        public void StopBreakTimer()
        {
            breakTimer.Stop();
        }

        //Method that is raised each time breaktimer elapses
        private void BreakEvent(object sender, EventArgs e)
        {
            
            addDelegateToBreak.Invoke(this.setBreakMinutes, this.setBreakSeconds);


            if (this.setBreakMinutes <= 0 && this.setBreakSeconds <= 0)
            {
                StopBreakTimer();
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


