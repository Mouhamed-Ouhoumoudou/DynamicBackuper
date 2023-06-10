/*using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
//using DSSGBOAdmin.Models.Entities;
using DSSGBOAdmin.Models.BLL;
//using DSSGBOAdmin.Utilities;

namespace Backuper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : Controller
    {

        private static Timer timer;
        [HttpGet("/timer")]
        public IActionResult SetTimerBackup()
        {
            if (MyHelpers.CurrentSettingSuperAdmin == null)
                MyHelpers.CurrentSettingSuperAdmin = BLL_SettingSuperAdmin.SelectSettingSuperAdmin();

            *//*
             * 
             * On calcul l'heure et la minute à laquelle le backup se fera pour chaque
             * L'heure et la minute sont enregistrer dans la table SettingSuperAdmin pour le champ TimeExecutionBackup
             * C'est un string qui contient Heure : Minute
             *//*
            DateTime CurrentDateTime = DateTime.Now;
            var HourMinuteSettingSuperAdmin = MyHelpers.CurrentSettingSuperAdmin.TimeExecutionBackup.Split(":");
            DateTime dateTime = new DateTime(CurrentDateTime.Year, CurrentDateTime.Month, CurrentDateTime.Day, int.Parse(HourMinuteSettingSuperAdmin[0]), int.Parse(HourMinuteSettingSuperAdmin[1]), int.Parse(HourMinuteSettingSuperAdmin[2]));

            var startTimeSpan = dateTime.Subtract(DateTime.Now);

            //Cette Tache va s'effectuer Chaque Jour
            var periodTimeSpan = TimeSpan.FromDays(1);

            if (timer == null)
                timer = new Timer(async (ee) =>
                {
                    if (MyHelpers.CurrentSettingSuperAdmin == null)
                        MyHelpers.CurrentSettingSuperAdmin = BLL_SettingSuperAdmin.SelectSettingSuperAdmin();

                    await BLL_Backup.BackupsDateExectuionIsToday(MyHelpers.CurrentSettingSuperAdmin.PathGBORoot);

                }, null, startTimeSpan, periodTimeSpan);
            return Json(new { data = "Ok" });
        }


        [HttpGet("")]
        public JsonResult GetAllBackupsIndex()
        {
            try
            {
                string AllBackupsIndex = BLL_Backup.SelectAllBackupsIndex();
                return Json(new { success = true, message = "Success", data = AllBackupsIndex });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpGet("{IdOrganization}")]
        public JsonResult GetAllBackupsByOrg(long IdOrganization)
        {
            try
            {
                List<Backups> AllBackups = BLL_Backup.SelectBackupsByOrg(IdOrganization);
                return Json(new { success = true, message = "Success", data = AllBackups });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost("")]
        public JsonResult AdminBackUpData(ParamsBackupOrg ParamsBackupOrg)
        {
            var isSuccess = false;
            var messageResp = "";
            try
            {
                if (ParamsBackupOrg == null || ParamsBackupOrg.IdOrganization == 0 || string.IsNullOrWhiteSpace(ParamsBackupOrg.PrefixOrganization) == true)
                    throw new Exception("Veuillez vérifier les données de la sauvegarde.");

                Task.Run(async () =>
                {
                    if (MyHelpers.CurrentSettingSuperAdmin == null)
                        MyHelpers.CurrentSettingSuperAdmin = BLL_SettingSuperAdmin.SelectSettingSuperAdmin();

                    await BLL_Backup.BackupDataAdmin(ParamsBackupOrg, MyHelpers.CurrentSettingSuperAdmin.PathGBORoot);
                });
                isSuccess = true;
                messageResp = "Succès plannification backup de l'organisation";
            }
            catch (Exception ex)
            {
                isSuccess = false;
                messageResp = $"Erreur Backup de l'organisation. \n Message : {ex.Message} ";
            }
            return Json(new { success = isSuccess, message = messageResp });
        }

        [HttpPost("planningBackup")]
        public JsonResult AdminPlanningBackUpData(ParamsBackupPlanning paramsPlanning)
        {
            try
            {
                BLL_Backup.BackupPlanningListOrganization(paramsPlanning);
                return Json(new { success = true, message = $"Planning Succes. Les backups se feront chaque {paramsPlanning.NbrJourInterval} jour(s)." });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
*/