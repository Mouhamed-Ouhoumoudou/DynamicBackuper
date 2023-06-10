
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
public class DAL_BackupConfig
{
 // Method Add BackupConfig
public static int Add(BackupConfig backupconfig)
{
 using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "INSERT INTO BackupConfig (Interval,Heure)output INSERTED.Id VALUES (@Interval,@Heure)";
SqlCommand command = new SqlCommand(StrSQL, con);

command.Parameters.AddWithValue("@Interval",backupconfig.Interval);
command.Parameters.AddWithValue("@Heure",backupconfig.Heure);
return Convert.ToInt32(DataBaseAccessUtilities.ScalarRequest(command));
}
}
// Method Update BackupConfig
 public static void Update(int id, BackupConfig backupconfig)
{
using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "UPDATE BackupConfig SET Interval=@Interval,Heure=@Heure WHERE Id = @Id";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@Id", id);
command.Parameters.AddWithValue("@Interval",backupconfig.Interval);
command.Parameters.AddWithValue("@Heure",backupconfig.Heure);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Method Delete BackupConfig
public static void Delete(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
string StrSQL = "DELETE FROM BackupConfig WHERE Id=@EntityKey";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@EntityKey", EntityKey);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Convert Object DataRow in BackupConfig
private static BackupConfig GetEntityFromDataRow(DataRow dataRow)
{ 
BackupConfig backupconfig = new BackupConfig();
backupconfig.Id = Convert.ToInt32(dataRow["Id"]);
backupconfig.Interval = Convert.ToInt32(dataRow["Interval"]);
backupconfig.Heure = Convert.ToInt32(dataRow["Heure"]);

return backupconfig;
}
// Fill List Of BackupConfig With DataTable
 private static List<BackupConfig> GetListFromDataTable(DataTable dt)
{
 List<BackupConfig> list = new List<BackupConfig>();
if (dt != null)
{
foreach (DataRow dr in dt.Rows)
list.Add(GetEntityFromDataRow(dr));
}
return list;
}
// Get BackupConfig By EntityKey
public static BackupConfig GetBackupConfig(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
con.Open();
string StrSQL = "SELECT * FROM BackupConfig WHERE Id = @id";
 SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@id", EntityKey);
DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
if (dt != null && dt.Rows.Count != 0)
return GetEntityFromDataRow(dt.Rows[0]);
else
return null;
}
}
// Get ALL BackupConfig
public static List<BackupConfig> SelectAll()
{
DataTable dataTable;
using (SqlConnection con = DBConnection.GetConnection())
{
 con.Open();
string StrSQL = "SELECT * FROM BackupConfig"; 
SqlCommand command = new SqlCommand(StrSQL, con);
dataTable = DataBaseAccessUtilities.SelectRequest(command);
}
return GetListFromDataTable(dataTable);
}
}
}