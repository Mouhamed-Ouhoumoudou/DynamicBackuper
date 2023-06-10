using System.IO;
using Backuper.Models.DAL;
using Backuper.Models.Entities;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backuper.NAS;
namespace Backuper.Models.BLL
{
public class BLL_BackupConfig
{ 
public static int Add(BackupConfig backupconfig)
{
return DAL_BackupConfig.Add(backupconfig);
}
 public static void Update(int id, BackupConfig backupconfig)
{
 DAL_BackupConfig.Update(id, backupconfig);
}
 public static void Delete(int id)
{
 DAL_BackupConfig.Delete(id);
}
public static BackupConfig GetBackupConfig(int id)
{
 return DAL_BackupConfig.GetBackupConfig(id);
}
public static List<BackupConfig> GetAll()
{
return DAL_BackupConfig.SelectAll();
}

        public static void BackupPeriodique(Object source, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {

                    string[] files = Directory.GetFiles("wwwroot/");
                    foreach (string file in files)
                    {
                        NAS_Operation.backupFile(file, NAS_Access.getBackupFolder());
                    }
                    Backup backup = new Backup();
                    backup.Etat = "Terminee";
                    backup.Message = "Backup effectué avec succes";
                    backup.DateBackup = "Date: " + DateTime.Now.ToString();
                    backup.Id = BLL_Backup.Add(backup);

                }
                catch (Exception ex)
                {
                    Backup backup = new Backup();
                    backup.Etat = "Erreur";
                    backup.Message = ex.Message;
                    backup.DateBackup = "Date: " + DateTime.Now.ToString();
                    backup.Id = BLL_Backup.Add(backup);
                }
            });
           
        }
}
}