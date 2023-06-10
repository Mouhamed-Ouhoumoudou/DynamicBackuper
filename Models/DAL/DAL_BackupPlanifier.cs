
using Backuper.Models.Entities;
using Backuper.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace Backuper.Models.DAL
{
public class DAL_BackupPlanifier
{
 // Method Add BackupPlanifier
public static int Add(BackupPlanifier backupplanifier)
{
 using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "INSERT INTO BackupPlanifier (DatePlanification,DateExecution,TimeToExecute)output INSERTED.Id VALUES (@DatePlanification,@DateExecution,@TimeToExecute)";
SqlCommand command = new SqlCommand(StrSQL, con);

command.Parameters.AddWithValue("@DatePlanification",backupplanifier.DatePlanification?? (object)DBNull.Value);
command.Parameters.AddWithValue("@DateExecution",backupplanifier.DateExecution?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TimeToExecute", backupplanifier.TimeToExecute ?? (object)DBNull.Value);
                return Convert.ToInt32(DataBaseAccessUtilities.ScalarRequest(command));
}
}
// Method Update BackupPlanifier
 public static void Update(int id, BackupPlanifier backupplanifier)
{
using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "UPDATE BackupPlanifier SET DatePlanification=@DatePlanification,DateExecution=@DateExecution ,TimeToExecute=@TimeToExecute WHERE Id = @Id";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@Id", id);
command.Parameters.AddWithValue("@DatePlanification",backupplanifier.DatePlanification?? (object)DBNull.Value);
command.Parameters.AddWithValue("@DateExecution",backupplanifier.DateExecution?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TimeToExecute", backupplanifier.TimeToExecute ?? (object)DBNull.Value);
                DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Method Delete BackupPlanifier
public static void Delete(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
string StrSQL = "DELETE FROM BackupPlanifier WHERE Id=@EntityKey";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@EntityKey", EntityKey);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Convert Object DataRow in BackupPlanifier
private static BackupPlanifier GetEntityFromDataRow(DataRow dataRow)
{ 
BackupPlanifier backupplanifier = new BackupPlanifier();
backupplanifier.Id = Convert.ToInt32(dataRow["Id"]);
backupplanifier.DatePlanification = dataRow["DatePlanification"].ToString();
backupplanifier.DateExecution = dataRow["DateExecution"].ToString(); 
backupplanifier.TimeToExecute = dataRow["TimeToExecute"].ToString();
            return backupplanifier;
}
// Fill List Of BackupPlanifier With DataTable
 private static List<BackupPlanifier> GetListFromDataTable(DataTable dt)
{
 List<BackupPlanifier> list = new List<BackupPlanifier>();
if (dt != null)
{
foreach (DataRow dr in dt.Rows)
list.Add(GetEntityFromDataRow(dr));
}
return list;
}
// Get BackupPlanifier By EntityKey
public static BackupPlanifier GetBackupPlanifier(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
con.Open();
string StrSQL = "SELECT * FROM BackupPlanifier WHERE Id = @id";
 SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@id", EntityKey);
DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
if (dt != null && dt.Rows.Count != 0)
return GetEntityFromDataRow(dt.Rows[0]);
else
return null;
}
}
// Get ALL BackupPlanifier
public static List<BackupPlanifier> SelectAll()
{
DataTable dataTable;
using (SqlConnection con = DBConnection.GetConnection())
{
 con.Open();
string StrSQL = "SELECT * FROM BackupPlanifier"; 
SqlCommand command = new SqlCommand(StrSQL, con);
dataTable = DataBaseAccessUtilities.SelectRequest(command);
}
return GetListFromDataTable(dataTable);
}
}
}