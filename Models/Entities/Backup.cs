
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.Entities
{
public class Backup
{
public int Id { get; set; }
public string Etat { get; set; }
public string Message { get; set; }
public string DateBackup { get; set; }
public Backup()
{
}
public Backup(string pEtat,string pMessage,string pDateBackup)
{

Etat= pEtat;
Message= pMessage;
DateBackup= pDateBackup;
}
}
}