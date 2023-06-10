using System.IO;
using Backuper.Models.DAL;
using Backuper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backuper.NAS;
using System.Timers;
namespace Backuper.Models.BLL
{
public class BLL_BackupPeriodique
{
        

public static int Add(BackupPeriodique backupperiodique)
{
return DAL_BackupPeriodique.Add(backupperiodique);
}
 public static void Update(int id, BackupPeriodique backupperiodique)
{
 DAL_BackupPeriodique.Update(id, backupperiodique);
}
 public static void Delete(int id)
{
 DAL_BackupPeriodique.Delete(id);
}
public static BackupPeriodique GetBackupPeriodique(int id)
{
 return DAL_BackupPeriodique.GetBackupPeriodique(id);
}
public static List<BackupPeriodique> GetAll()
{
return DAL_BackupPeriodique.SelectAll();
}

        public static void RunBackup(Object source, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    /* recuperation de la liste des tous les fichier qu'on va faire leur Backup */
                    string[] files = Directory.GetFiles("wwwroot/");
                    //on fait le Backup des fichier un à un vers le NAS
                    foreach (string file in files)
                    {
                        NAS_Operation.backupFile(file, NAS_Access.getBackupFolder());
                    }
                    //apres avoir Terminé on enreigistre l'historique de ce Backup Dans la BD
                    Backup backup = new Backup();
                    backup.Etat = "Terminee";
                    backup.Message = "Backup effectué avec succes";
                    backup.DateBackup = "Date: " + DateTime.Now.ToString();
                    backup.Id = BLL_Backup.Add(backup);

                }
                catch (Exception ex)
                {
                    //le cas echeant on enreigistre l'historique d'erreur pour ce Backup Dans la BD
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