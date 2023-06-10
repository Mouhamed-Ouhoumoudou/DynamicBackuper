using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backuper.TimeManagement
{
    internal class TimeOnly
    {
        /* cette methode retourne le temps à partir de DateTime */
        public static string FromDateTime(DateTime dateTime)
        {
            string TimeString = "";
            //elimination de miliseconde
            TimeString= dateTime.TimeOfDay.ToString().Substring(0,5);
            return TimeString;
        }
    }
}
