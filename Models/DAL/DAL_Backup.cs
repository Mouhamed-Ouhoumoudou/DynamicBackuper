
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
public class DAL_Backup
{
 // Method Add Backup
public static int Add(Backup backup)
{
 using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "INSERT INTO BackupJournal (Etat,Message,DateBackup)output INSERTED.Id VALUES (@Etat,@Message,@DateBackup)";
SqlCommand command = new SqlCommand(StrSQL, con);

command.Parameters.AddWithValue("@Etat",backup.Etat?? (object)DBNull.Value);
command.Parameters.AddWithValue("@Message",backup.Message?? (object)DBNull.Value);
command.Parameters.AddWithValue("@DateBackup",backup.DateBackup?? (object)DBNull.Value);
return Convert.ToInt32(DataBaseAccessUtilities.ScalarRequest(command));
}
}
// Method Update Backup
 public static void Update(int id, Backup backup)
{
using (SqlConnection con = DBConnection.GetConnection())
{
 string StrSQL = "UPDATE BackupJournal SET Etat=@Etat,Message=@Message,DateBackup=@DateBackup WHERE Id = @Id";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@Id", id);
command.Parameters.AddWithValue("@Etat",backup.Etat?? (object)DBNull.Value);
command.Parameters.AddWithValue("@Message",backup.Message?? (object)DBNull.Value);
command.Parameters.AddWithValue("@DateBackup",backup.DateBackup?? (object)DBNull.Value);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Method Delete Backup
public static void Delete(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
string StrSQL = "DELETE FROM BackupJournal WHERE Id=@EntityKey";
SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@EntityKey", EntityKey);
DataBaseAccessUtilities.NonQueryRequest(command);
}
}
 // Convert Object DataRow in Backup
private static Backup GetEntityFromDataRow(DataRow dataRow)
{ 
Backup backup = new Backup();
backup.Id = Convert.ToInt32(dataRow["Id"]);
backup.Etat = dataRow["Etat"].ToString();
backup.Message = dataRow["Message"].ToString();
backup.DateBackup = dataRow["DateBackup"].ToString();

return backup;
}
// Fill List Of Backup With DataTable
 private static List<Backup> GetListFromDataTable(DataTable dt)
{
 List<Backup> list = new List<Backup>();
if (dt != null)
{
foreach (DataRow dr in dt.Rows)
list.Add(GetEntityFromDataRow(dr));
}
return list;
}
// Get Backup By EntityKey
public static Backup GetBackup(int EntityKey)
{
using (SqlConnection con = DBConnection.GetConnection())
{
con.Open();
string StrSQL = "SELECT * FROM BackupJournal WHERE Id = @id";
 SqlCommand command = new SqlCommand(StrSQL, con);
command.Parameters.AddWithValue("@id", EntityKey);
DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
if (dt != null && dt.Rows.Count != 0)
return GetEntityFromDataRow(dt.Rows[0]);
else
return null;
}
}
// Get ALL Backup
public static List<Backup> SelectAll()
{
DataTable dataTable;
using (SqlConnection con = DBConnection.GetConnection())
{
 con.Open();
string StrSQL = "SELECT * FROM BackupJournal"; 
SqlCommand command = new SqlCommand(StrSQL, con);
dataTable = DataBaseAccessUtilities.SelectRequest(command);
}
return GetListFromDataTable(dataTable);
}
}
}