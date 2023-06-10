using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backuper.Models.BLL;
using Backuper.Models.Entities;
using Backuper.NAS;
using Backuper.TimeManagement;
namespace Backuper.Controllers{
[Route("api/[controller]")]
 [ApiController]
 public class BackupPlanifierController : Controller
{
// GET: api/<BackupPlanifierController>
[HttpGet]
 public JsonResult Get()
{
try
{
 List<BackupPlanifier> backupplanifiers = BLL_BackupPlanifier.GetAll();
return Json(new { success = true, message = "BackupPlanifiers trouvés", data = backupplanifiers });
 }
catch (Exception e)
{
return Json(new { success = false, message = e.Message });
}
}
// GET api/<BackupPlanifierController>/5
[HttpGet("{id}")]
public JsonResult Get(int id)
{
 try
 {
 BackupPlanifier backupplanifier = BLL_BackupPlanifier.GetBackupPlanifier(id);
 return Json(new { success = true, message = "BackupPlanifier trouvé", data = backupplanifier });
}
 catch (Exception e)
 {
return Json(new { success = false, message = e.Message });
}
}
// POST api/<BackupPlanifierController>
[HttpPost]
public JsonResult Post([FromForm] BackupPlanifier backupplanifier)
{
try
{
if(backupplanifier.Id == 0)
{
                    /*cette methode ajoute d'abord la date et heure de Backup (provenant du formulaire ) dans la Table "BackupPlanifier"*/

                    backupplanifier.Id = BLL_BackupPlanifier.Add(backupplanifier);
                    /* recuperation de date et heure d'execution que Timer va utiliser*/
                    string dateEx = BLL_BackupPlanifier.GetBackupPlanifier(backupplanifier.Id).DateExecution.ToString();
                    string TimeToExe= BLL_BackupPlanifier.GetBackupPlanifier(backupplanifier.Id).TimeToExecute.ToString();
                    /*ensuite elle ajoute un nouveau historique à l'etat Attente Dans la Table "Backup" */
                    Backup backup = new Backup();
                    backup.Etat = "Attente";
                    backup.Message = "Backup sera executé le " + dateEx + " à " + TimeToExe; ;
                    backup.DateBackup = "Date: " + dateEx + " "+TimeToExe; ;
                    backup.Id = BLL_Backup.Add(backup);
                    /* ensuite on lance le Timer en arriere plan */
                    Task.Run(async () =>
                    {
                        var startTimeSpan = TimeSpan.Zero;  
                    var periodTimeSpan = TimeSpan.FromMinutes(1);
                        
                    var timer = new System.Threading.Timer((e) =>
                    {
                        /* recuperation de date et heure actuelle ,il faut noter que dans cette version .NET 3.1 DateOnly et TimeOnly sont des  classes 
                          que nous avons nous meme developpé (Dans le dossier TimeManagement) , ces classe sont des classe predefinies dans la version .NET 6*/
                        string toDaye = DateOnly.FromDateTime(DateTime.Now).ToString();
                        string CurentTime = DateTime.Now.ToString("HH:mm");  // on peut aussi faire TimeOnly.FromDateTime(DateTime.Now).ToString();

                        /* verification de la date et heure courante par rapport à la date et heure planifié pour le backup */
                        if (toDaye == dateEx && CurentTime==TimeToExe  ) 
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
                                backup.Etat = "Terminee";
                                backup.Message = "Backup effectué avec succes";
                                backup.DateBackup = "Date: " + DateTime.Now.ToString()+" à "+TimeToExe;
                                BLL_Backup.Update(backup.Id, backup);

                            }
                            catch (Exception ex)
                            {
                                //le cas echeant on enreigistre l'historique d'erreur pour ce Backup Dans la BD
                                backup.Etat = "Erreur";
                                backup.Message = ex.Message;
                                backup.DateBackup = "Date: " + DateTime.Now.ToString();
                                BLL_Backup.Update(backup.Id, backup);
                            }
                        }

                    }, null, startTimeSpan, periodTimeSpan);
                    
                       
                });
                return Json(new { success = true, message = "Ajouté avec succès", data = backupplanifier });
}
else
{
 BLL_BackupPlanifier.Update(backupplanifier.Id, backupplanifier);
 return Json(new { success = true, message = "Modifié avec succès", data=backupplanifier });
 }
}
catch (Exception ex)
{
return Json(new { success = false, message = ex.Message });
}
}
// DELETE api/<BackupPlanifierController>/5
[HttpDelete("{id}")]
public JsonResult Delete(int id)
 {
try
{
BLL_BackupPlanifier.Delete(id);
return Json(new { success = true, message = "Supprimé avec succès" });
}
catch (Exception ex)
{
 return Json(new { success = false, message = ex.Message });
}
}
}
}