using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backuper.Models.BLL;
using Backuper.Models.Entities;
using Backuper.NAS;
namespace Backuper.Controllers{
[Route("api/[controller]")]
 [ApiController]
 public class BackupConfigController : Controller
{
// GET: api/<BackupConfigController>
[HttpGet]
 public JsonResult Get()
{
try
{
                List<BackupConfig> backupconfigs = BLL_BackupConfig.GetAll();
return Json(new { success = true, message = "BackupConfigs trouvés", data = backupconfigs });
 }
catch (Exception e)
{
return Json(new { success = false, message = e.Message });
}
}
// GET api/<BackupConfigController>/5
[HttpGet("{id}")]
public JsonResult Get(int id)
{
 try
 {
 BackupConfig backupconfig = BLL_BackupConfig.GetBackupConfig(id);
 return Json(new { success = true, message = "BackupConfig trouvé", data = backupconfig });
}
 catch (Exception e)
 {
return Json(new { success = false, message = e.Message });
}
}
// POST api/<BackupConfigController>
[HttpPost]
public JsonResult Post([FromForm] BackupConfig backupconfig)
{
try
{
if(backupconfig.Id == 0)
{
 backupconfig.Id = BLL_BackupConfig.Add(backupconfig);
 return Json(new { success = true, message = "Ajouté avec succès", data = backupconfig });
}
else
{
 BLL_BackupConfig.Update(backupconfig.Id, backupconfig);


                    System.Timers.Timer aTimer = new System.Timers.Timer(60000);
                    aTimer.Elapsed += BLL_BackupConfig.BackupPeriodique;
                    aTimer.AutoReset = true;
                    aTimer.Enabled = true;

                 

                    /*Task.Run(async () =>
                    {

                        var startTimeSpan = TimeSpan.Zero;
                        *//*var minuteDebut = BLL_BackupConfig.GetBackupConfig(1).Heure;
                        startTimeSpan.Add(new TimeSpan(2,minuteDebut , 0));*//*
                        var periodTimeSpan = TimeSpan.FromMinutes(BLL_BackupConfig.GetBackupConfig(1).Interval);
                        var timer = new System.Threading.Timer((e) =>
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
                        }, null, startTimeSpan, periodTimeSpan);
                    });*/
                    return Json(new { success = true, message = "Modifié avec succès", data=backupconfig });
 }
}
catch (Exception ex)
{
return Json(new { success = false, message = ex.Message });
}
}
// DELETE api/<BackupConfigController>/5
[HttpDelete("{id}")]
public JsonResult Delete(int id)
 {
try
{
BLL_BackupConfig.Delete(id);
return Json(new { success = true, message = "Supprimé avec succès" });
}
catch (Exception ex)
{
 return Json(new { success = false, message = ex.Message });
}
}


        /*public JsonResult SetTimeOfBackup([FromForm] int heure)
        {
            try
            {
                BackupConfig backupConfig = new BackupConfig();
                backupConfig.Heure = heure;
                return Json(new { success = true, message = "Time  changé" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        public JsonResult BackupManuel()
        {
            Task.Run(async () =>
            {
                string[] files = Directory.GetFiles("wwwroot/");
                foreach (string file in files)
                {
                    try
                    {
                        NAS_Operation.backupFile(file, NAS_Access.getBackupFolder());
                    }
                    catch
                    {

                    }
                }

                Backup backup = new Backup();
                backup.Etat = "Terminee";
                backup.Message = "Backup effectué avec succes";
                backup.DateBackup = "Date: " + DateTime.Now.ToString();
                //BLL_Backup.Add(backup);
            });

            return Json(new { success = true, message = "Backups Terminee" });
        }
        public JsonResult BackupPlanifier([FromForm] int Interval)
        {
            try
            {

                BackupConfig backupConfig = new BackupConfig();
                backupConfig.Interval = Interval;
                backupConfig.Heure = 00;
                BLL_BackupConfig.Update(1, backupConfig);

                Task.Run(async () =>
                {
                    var startTimeSpan = TimeSpan.Zero;
                    var periodTimeSpan = TimeSpan.FromMinutes(BLL_BackupConfig.GetBackupConfig(1).Interval);
                    var timer = new System.Threading.Timer((e) =>
                    {
                        try
                        {
                            string[] files = Directory.GetFiles("wwwroot/");
                            foreach (string file in files)
                            {
                                try
                                {
                                    NAS_Operation.backupFile(file, NAS_Access.getBackupFolder());
                                }
                                catch
                                {

                                }
                            }

                            Backup backup = new Backup();
                            backup.Etat = "Terminee";
                            backup.Message = "Backup effectué avec succes";
                            backup.DateBackup = "Date: " + DateTime.Now.ToString();
                            BLL_Backup.Add(backup);
                        }
                        catch (Exception ex)
                        {


                            Backup backup = new Backup();
                            backup.Etat = "Erreur";
                            backup.Message = ex.Message;
                            backup.DateBackup = "Date: " + DateTime.Now.ToString();
                            BLL_Backup.Add(backup);



                        }

                    }, null, startTimeSpan, periodTimeSpan);
                });

                return Json(new { success = true, message = "Backup planifié" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }*/
    }
}