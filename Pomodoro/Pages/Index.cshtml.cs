using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Timers;


namespace Pomodoro.Pages
{
    public class IndexModel : PageModel
    {

    private static string StaticMessage;
    public string Message {get { return StaticMessage; }}
    static PomodoroLibrary.PomodoroTimer timer;
    public IndexModel()
    {
        
    }

        
        public void OnGet()
        {
            
            if(timer == null)
            {
                timer = new PomodoroLibrary.PomodoroTimer(25,0,5,0);
                timer.StartTaskTimer();
            }
            
            timer.addDelegateToFocus = TimerCount;            
        }

        public void TimerCount(int minutes, int seconds)
        {
            
             StaticMessage = minutes.ToString("00:") + seconds.ToString("00");
        }
    }
}
