using System;
using System.Timers;
namespace PomodoroLibrary
{
    //TODO  Metod för user
    //TODO: Add to TODO list
    //TODO: Print TODO list
    //TODO: Färdiga Presets (Intervaller, studera, städa osvosv)
    //TODO: Menu
    //TODO: Pin TODO as done
    //TODO: Remove activity from TODO when finished
    //TODO: History over finished activities
    public class ToDoList
    {
        //Lagring för konstruktor som tar in en task. 

        //Metod som lägger till i listan

        //Metod som tar bort

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
        private int setTaskMinutes {get;set;}
        private int setTaskSeconds {get;set;}
        private int setBreakMinutes {get;set;}
        private int setBreakSeconds {get;set;}

        // Constructor with in-parameters
        public Pomodoro (int setTaskMinutes, int setTaskSeconds, int setBreakMinutes, int setBreakSeconds)
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
            onFocus.Invoke(this.setTaskMinutes,this.setTaskSeconds);
                         
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
            onBreak.Invoke(this.setBreakMinutes,this.setBreakSeconds);

                
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
