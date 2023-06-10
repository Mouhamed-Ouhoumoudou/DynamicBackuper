using System.IO;
using Backuper.Models.DAL;
using Backuper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.BLL
{
public class BLL_BackupPlanifier
{ 
public static int Add(BackupPlanifier backupplanifier)
{
return DAL_BackupPlanifier.Add(backupplanifier);
}
 public static void Update(int id, BackupPlanifier backupplanifier)
{
 DAL_BackupPlanifier.Update(id, backupplanifier);
}
 public static void Delete(int id)
{
 DAL_BackupPlanifier.Delete(id);
}
public static BackupPlanifier GetBackupPlanifier(int id)
{
 return DAL_BackupPlanifier.GetBackupPlanifier(id);
}
public static List<BackupPlanifier> GetAll()
{
return DAL_BackupPlanifier.SelectAll();
}
}
}