
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.Entities
{
public class BackupConfig
{
public int Id { get; set; }
public int Interval { get; set; }
public int Heure { get; set; }
        //public string typeInterval { get; set; }
public BackupConfig()
{
}
public BackupConfig(int pInterval,int pHeure)
{

Interval= pInterval;
Heure= pHeure;
}
}
}