using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backuper.TimeManagement
{
    internal class DateOnly
    {
        /* methode qui retourne une date a partir dateTime */
        public static string FromDateTime(DateTime dateTime)
        {
            string dateString = "";
            dateString =  dateTime.Date.ToString().Substring(0,10);
            return dateString;
        }

    }
    
}
