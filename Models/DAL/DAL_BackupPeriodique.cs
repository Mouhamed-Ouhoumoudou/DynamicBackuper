
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
public class DAL_BackupPeriodique
{
 // Method Add BackupPeriodique
public static int Add(BackupPeriodique backupperiodique)
{
 using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "INSERT INTO BackupPeriodique (Interval,TypeInterval)output INSERTED.Id VALUES (@Interval,@TypeInterval)";
SqlCommand command = new SqlCommand(StrSQL, con);

command.Parameters.AddWithValue("@Interval",backupperiodique.Interval);
command.Parameters.AddWithValue("@TypeInterval",backupperiodique.TypeInterval?? (object)DBNull.Value);
return Convert.ToInt32(DataBaseAccessUtilities.ScalarRequest(command));
}
}
// Method Update BackupPeriodique
 public static void Update(int id, BackupPeriodique backupperiodique)
{
using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "UPDATE BackupPeriodique SET Interval=@Interval,TypeInterval=@TypeInterval WHERE Id = @Id";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@Id", id);
command.Parameters.AddWithValue("@Interval",backupperiodique.Interval);
command.Parameters.AddWithValue("@TypeInterval",backupperiodique.TypeInterval?? (object)DBNull.Value);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Method Delete BackupPeriodique
public static void Delete(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
string StrSQL = "DELETE FROM BackupPeriodique WHERE Id=@EntityKey";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@EntityKey", EntityKey);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Convert Object DataRow in BackupPeriodique
private static BackupPeriodique GetEntityFromDataRow(DataRow dataRow)
{ 
BackupPeriodique backupperiodique = new BackupPeriodique();
backupperiodique.Id = Convert.ToInt32(dataRow["Id"]);
backupperiodique.Interval = Convert.ToInt32(dataRow["Interval"]);
backupperiodique.TypeInterval = dataRow["TypeInterval"].ToString();

return backupperiodique;
}
// Fill List Of BackupPeriodique With DataTable
 private static List<BackupPeriodique> GetListFromDataTable(DataTable dt)
{
 List<BackupPeriodique> list = new List<BackupPeriodique>();
if (dt != null)
{
foreach (DataRow dr in dt.Rows)
list.Add(GetEntityFromDataRow(dr));
}
return list;
}
// Get BackupPeriodique By EntityKey
public static BackupPeriodique GetBackupPeriodique(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
con.Open();
string StrSQL = "SELECT * FROM BackupPeriodique WHERE Id = @id";
 SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@id", EntityKey);
DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
if (dt != null && dt.Rows.Count != 0)
return GetEntityFromDataRow(dt.Rows[0]);
else
return null;
}
}
// Get ALL BackupPeriodique
public static List<BackupPeriodique> SelectAll()
{
DataTable dataTable;
using (SqlConnection con = DBConnection.GetConnection())
{
 con.Open();
string StrSQL = "SELECT * FROM BackupPeriodique"; 
SqlCommand command = new SqlCommand(StrSQL, con);
dataTable = DataBaseAccessUtilities.SelectRequest(command);
}
return GetListFromDataTable(dataTable);
}
}
}