using System.IO;
using Backuper.Models.DAL;
using Backuper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backuper.NAS;
namespace Backuper.Models.BLL
  
{
public class BLL_Backup
{ 
public static int Add(Backup backup)
{
return DAL_Backup.Add(backup);
}
 public static void Update(int id, Backup backup)
{
 DAL_Backup.Update(id, backup);
}
 public static void Delete(int id)
{
 DAL_Backup.Delete(id);
}
public static Backup GetBackup(int id)
{
 return DAL_Backup.GetBackup(id);
}
public static List<Backup> GetAll()
{
return DAL_Backup.SelectAll();
}

         public static void BackupManuel()
        {
            
            Task.Run(async () =>
            {
                Backup backup = new Backup();
                string[] files = Directory.GetFiles("wwwroot/");
                foreach (string file in files)
                {
                    try
                    {
                        NAS_Operation.backupFile(file, NAS_Access.getBackupFolder());
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }

                
                backup.Etat = "Terminee";
                backup.Message = "Backup effectué avec succes";
                backup.DateBackup = "Date: " + DateTime.Now.ToString();
              backup.Id= DAL_Backup.Add(backup);
            });
            


        }
    }
}