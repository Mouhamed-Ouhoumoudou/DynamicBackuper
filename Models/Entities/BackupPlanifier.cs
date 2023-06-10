
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.Entities
{
public class BackupPlanifier
{
public int Id { get; set; }
public string DatePlanification { get; set; }
public string DateExecution { get; set; }
        public string TimeToExecute { get; set; }
public BackupPlanifier()
{
}
public BackupPlanifier(string pDatePlanification,string pDateExecution)
{

DatePlanification= pDatePlanification;
DateExecution= pDateExecution;
}
}
}