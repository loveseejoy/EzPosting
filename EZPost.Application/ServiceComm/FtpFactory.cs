using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EZPost.ServiceComm
{
    public class FtpFactory
        {
            private string ftpServerIP;
            private string ftpRemotePath; //xia zai wen jian de lu jin
            private string ftpOutPutPath; //shang chuang wen jian de lu jin
            private string ftpUserID;
            private string ftpPassword;
            private string ftpURI;
            //string proxyName;
            //string proxyPass;
            private WebProxy _proxy = null;
            private NetworkCredential networkCredential = null;

            public WebProxy proxy
            {
                get
                {
                    return _proxy;
                }
                set
                {
                    _proxy = value;
                }
            }

            /// <summary>
            /// 连接FTP
            /// </summary>
            /// <param name="FtpServerIP">FTP连接地址</param>
            /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
            /// <param name="FtpUserID">用户名</param>
            /// <param name="FtpPassword">密码</param>
            public FtpFactory(string FtpServerIP, string FtpRemotePath, string FtpOutPutPath, string FtpUserID, string FtpPassword)
            {
                ftpServerIP = FtpServerIP;
                ftpRemotePath = FtpRemotePath;
                ftpOutPutPath = FtpOutPutPath;
                ftpUserID = FtpUserID;
                ftpPassword = FtpPassword;
                ftpURI = "ftp://" + ftpServerIP + "/";
                //this._proxy = objProxy;
                //objProxy.Credentials = new NetworkCredential(this.proxyName,this.proxyPass);
            }

            /// <summary>
            /// 上传
            /// </summary>
            /// <param name="filename"></param>
            public void PutSingleFiles(string filename)
            {
                FileInfo fileInf = new FileInfo(filename);
                string uri = ChDir("", false) + fileInf.Name;
                //string uri = ftpURI + fileInf.Name;
                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = networkCredential == null ? new NetworkCredential(ftpUserID, ftpPassword) : networkCredential;
                reqFTP.EnableSsl = true;
                reqFTP.KeepAlive = true;
                reqFTP.Timeout = 300000;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                //reqFTP.Credentials = new NetworkCredential(this.proxyName,this.proxyPass);
                if (this._proxy != null)
                {
                    reqFTP.Proxy = this._proxy;
                }
                reqFTP.ContentLength = fileInf.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                FileStream fs = fileInf.OpenRead();
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertificatePolicy;
                    Stream strm = reqFTP.GetRequestStream();
                    contentLen = fs.Read(buff, 0, buffLength);
                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
                    strm.Close();
                    fs.Close();
                }
                catch (WebException e)
                {
                    String status = ((FtpWebResponse)e.Response).StatusDescription;
                }
            }
            public bool AcceptAllCertificatePolicy(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            /// <summary>
            /// 下载
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="fileName"></param>
            public void GetSingleFiles(string filePath, string fileName)
            {
                FtpWebRequest reqFTP;
                try
                {
                    string uri = ChDir(ftpRemotePath, true) + fileName;
                    FileStream outputStream = new FileStream(filePath + fileName, FileMode.Create);
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = networkCredential == null ? new NetworkCredential(ftpUserID, ftpPassword) : networkCredential;
                    if (this._proxy != null)
                    {
                        reqFTP.Proxy = this._proxy;
                    }
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }

                    ftpStream.Close();
                    outputStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("FtpWeb,Download Error -->" + ex.Message);
                }
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="fileName"></param>
            public void Delete(string fileName)
            {
                try
                {
                    string uri = ChDir(ftpRemotePath, true) + "/" + fileName;
                    //string uri = ftpURI + fileName;
                    FtpWebRequest reqFTP;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                    reqFTP.Credentials = networkCredential == null ? new NetworkCredential(ftpUserID, ftpPassword) : networkCredential;
                    //reqFTP.EnableSsl = true;//用户名加密
                    reqFTP.KeepAlive = false;
                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                    string result = String.Empty;
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    long size = response.ContentLength;
                    Stream datastream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(datastream);
                    result = sr.ReadToEnd();
                    sr.Close();
                    datastream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("FtpWeb,Delete Error -->" + ex.Message);
                }
            }

            /// <summary>
            /// 获取当前目录下明细(包含文件和文件夹)
            /// </summary>
            /// <returns></returns>
            public string[] GetFilesDetailList()
            {
                //string[] downloadFiles;
                try
                {
                    StringBuilder result = new StringBuilder();
                    FtpWebRequest ftp;
                    //ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                    ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                    ftp.Credentials = networkCredential == null ? new NetworkCredential(ftpUserID, ftpPassword) : networkCredential;
                    ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    WebResponse response = ftp.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    result.Remove(result.ToString().LastIndexOf("\n"), 1);
                    reader.Close();
                    response.Close();
                    return result.ToString().Split('\n');
                }
                catch (Exception ex)
                {
                    throw new Exception("FtpWeb,GetFilesDetailList Error -->" + ex.Message);
                }
            }
            /// <summary>
            /// 获取文件
            /// </summary>
            /// <returns></returns>

            public string[] Dir()
            {
                //string[] downloadFiles;
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;
                string uri = ChDir(ftpRemotePath, true);
                try
                {
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = networkCredential == null ? new NetworkCredential(ftpUserID, ftpPassword) : networkCredential;
                    reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                    WebResponse response = reqFTP.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    // to remove the trailing 'n'
                    if (result.Length != 0)
                    {
                        result.Remove(result.ToString().LastIndexOf('\n'), 1);//提醒
                    }
                    reader.Close();
                    response.Close();
                    if (result.Length != 0)
                    {
                        return result.ToString().Split('\n');
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("FtpWeb,GetFileList Error -->" + ex.Message);
                }
            }

            /// <summary>
            /// 获取当前目录下所有的文件夹列表(仅文件夹)
            /// </summary>
            /// <returns></returns>
            public string[] GetDirectoryList()
            {
                string[] drectory = GetFilesDetailList();
                string m = string.Empty;
                foreach (string str in drectory)
                {
                    int dirPos = str.IndexOf("<DIR>");
                    if (dirPos > 0)
                    {
                        /*判断 Windows 风格*/
                        m += str.Substring(dirPos + 5).Trim() + "\n";
                    }
                    else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                    {
                        /*判断 Unix 风格*/
                        string dir = str.Substring(54).Trim();
                        if (dir != "." && dir != "..")
                        {
                            m += dir + "\n";
                        }
                    }
                }

                char[] n = new char[] { '\n' };
                return m.Split(n);
            }

            /// <summary>
            /// 判断当前目录下指定的文件是否存在
            /// </summary>
            /// <param name="RemoteFileName">远程文件名</param>
            public bool FileExist(string RemoteFileName)
            {
                string[] fileList = GetDirectoryList();
                foreach (string str in fileList)
                {
                    if (str.Trim() == RemoteFileName.Trim())
                    {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// 切换当前目录
            /// </summary>
            /// <param name="DirectoryName"></param>
            /// <param name="IsRoot">true 绝对路径   false 相对路径</param>
            public string ChDir(string DirectoryName, bool IsRoot)
            {
                if (IsRoot)
                {
                    ftpRemotePath = DirectoryName;
                }
                else
                {
                    ftpRemotePath += DirectoryName + "/";
                }
                ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath;
                return ftpURI;
            }
        }
}