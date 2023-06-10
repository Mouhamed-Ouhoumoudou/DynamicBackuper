using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Renci.SshNet;
using Renci.SshNet.Sftp;
namespace Backuper.NAS
{
    /* cette classe a des methodes utilisant le protocol FTP et des methodes utilisant SFTP */
    public class NAS_Operation
    {
      
        /* Les Methodes utilisant FTP */
        public static void UploadFileToServer(IFormFile formFile)
        {
            try
            {
                /* Cette methode envoi le fichier au server NAS, Les informations d'acces comme l'adresse IP,nom du domaine ,username,passeword etc
                 sont definis dans une autre classe NAS_Access */

                /*ainsi on crée un fichier vide dans le repertoire racine de FTP coté NAS
                 * (si on veut envoyer dans des sous repertoire coté FTP on peut les specifier dans le chemin juste apres l'Ip ou nom du domaine) */
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(NAS_Access.getDomaine()+formFile.FileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(NAS_Access.getUsername(), NAS_Access.getPasseword());
                /*on recupere le contenue du fichier à envoyer depuis le dossier Tmp */
                FileStream fileStream = File.Open("Tmp/" + formFile.FileName, FileMode.Open, FileAccess.Read);
                byte[] fileContents=new byte[fileStream.Length];
                fileStream.Read(fileContents,0,fileContents.Length);
                fileStream.Close();
                request.ContentLength = fileContents.Length;
                /* on recupere le flux de notre fichier vide créé sur NAS */
                Stream requestStream = request.GetRequestStream();
                /*ensuite on le remplie  avec les octets du fichier provenant du dossier Tmp */
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                // Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }


            catch (Exception ex)
            {
                throw ex;
            }
}


public static byte[] DisplayFileFromServer(string file)
{
            /*cette methode telecharge le fichier par le nom donée apres avoir connecté et renvoi ses informations en octets */
    WebClient request = new WebClient();
    string url = NAS_Access.getDomaine() + file;
    request.Credentials = new NetworkCredential(NAS_Access.getUsername(), NAS_Access.getPasseword());

    try
    {
        byte[] newFileData = request.DownloadData(url);
        return newFileData;
    }
    catch (WebException e)
    {

        return null;
    }
}


public static void DeleteFileInServer(string file)
{
            try
            {
                /* supprime le fichier donné sur le serveur NAS apres avoir connecté*/
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(NAS_Access.getDomaine() + file);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(NAS_Access.getUsername(), NAS_Access.getPasseword());

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    // return response.StatusDescription;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
}


public static void updateFileInServer(string oldFile, IFormFile formFile)
{
            /* la modification c'est juste suprimer et rajouter un nouveau */
    DeleteFileInServer(oldFile);
    UploadFileToServer(formFile);
    Console.WriteLine("Fichier modifié");
}








/* Les methodes Utisant le protocol SFTP */

        // cette methode rettourne le client SFTP pour assurer la connexion au NAS
 public static SftpClient GetSftpClient()
   {
            return new SftpClient(NAS_Access.getSecureDomaine(), 22, NAS_Access.getUsername(), NAS_Access.getPasseword());
   }


        //cette methode connecte d'abord au NAS ensuite ouvre le flux d'un fichier par son nom dans le dossier Tmp en local
        //puis envoi les octet de ce flux au NAS en donnant un nom au fichier créé sur le NAS
        //et apres on Dispose la connexion et on ferme le flux qu'on a ouvert en local
public static void uploadWithSFTP(IFormFile formFile)
        {
            try
            {
                SftpClient client = GetSftpClient();
                client.Connect();
                FileStream fs = new FileStream("Tmp/" + formFile.FileName, FileMode.Open);
                client.UploadFile(fs, formFile.FileName);
                client.Dispose();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*  cette methode crée un flux en memoire(pour  eviter le stockage temporaire)
         *  puis elle se connecte au NAS et  telecharge le fichier donnée en le passant dans le flux créé,
         *  recupere les octets à partir de ce flux ensuite les retourner(ces octets seront renvoyé au JavaScripte par le Controller)
         ensuite la methode deconnecte du NAS*/
        public static byte[] DownloadWithSFTP(string file)
        {

            MemoryStream ms = new MemoryStream();
            using (var client = GetSftpClient()) 
            {
                client.Connect();

                client.DownloadFile(file, ms);
                byte[] data = ms.GetBuffer();
                return data;
                client.Disconnect();
            }
        }
        /* cette methode connecte au NAS puis elle suprime le fichier donné ensuite elle deconnecte de NAS 
         une exception sera declanchée en cas d'abscence du fichier ou probleme de connexion coté SFTP*/
        public static void DeleteWithSFTP(string file)
        {
            try
            {
                using (SftpClient client = GetSftpClient())
                {
                    client.Connect();
                    client.DeleteFile(file);
                    client.Disconnect();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        /*Celle là modifier le fichier donnée (  la modification c'est juste suprimer et rajouter un nouveau )*/
        public static void updateFileInServerWithSFTP(string oldFile, IFormFile formFile)
        {
           
            DeleteWithSFTP(oldFile);
            uploadWithSFTP(formFile);
            Console.WriteLine("Fichier modifié");
        }

        public static void backupFile(string filePath, string DestinationFoler)

        {
            /* cette methode permet de faire le Backup d'un fichier local vers le Repertoire de Backup distant
             * (Dans le NAS ,ce  repertoire porte le nom "Backup")
             * ainsi apres la connexion SFTP ,le client SFTP se deplace pour aller au  repertoir "Backup" puis il uplaad le fichier
             */
            try
            {
                SftpClient client = new SftpClient(NAS_Access.getSecureDomaine(), 22, NAS_Access.getUsername(), NAS_Access.getPasseword());
                client.Connect();
                FileStream fs = new FileStream(filePath, FileMode.Open);
                client.ChangeDirectory(DestinationFoler);
                client.UploadFile(fs, Path.GetFileName(filePath));
                client.Dispose();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
