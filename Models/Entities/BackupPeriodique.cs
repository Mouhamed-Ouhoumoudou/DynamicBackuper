
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.Entities
{
public class BackupPeriodique
{
public int Id { get; set; }
public int Interval { get; set; }
public string TypeInterval { get; set; }
public BackupPeriodique()
{
}
public BackupPeriodique(int pInterval,string pTypeInterval)
{

Interval= pInterval;
TypeInterval= pTypeInterval;
}
}
}