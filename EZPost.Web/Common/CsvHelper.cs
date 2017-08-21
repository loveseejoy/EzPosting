using System;
using System.Data;
using System.IO;

namespace EZPost.Web.Common
{
    /// <summary>
    /// CSV格式文件的操作
    /// </summary>
    public class CsvHelper
    {
        #region 将CSV文件的数据读取到DataTable中+ static DataTable OpenCSV(string filePath)
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {

                //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                StreamReader sr = new StreamReader(fs, encoding);
                //string fileContent = sr.ReadToEnd();
                //encoding = sr.CurrentEncoding;
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] DataLine = null;
                string[] tableHead = null;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (IsFirst == true)
                    {
                        tableHead = strLine.Split(';');

                        IsFirst = false;
                        columnCount = tableHead.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            tableHead[i] = DelStr(tableHead[i]);
                            DataColumn dc = new DataColumn(tableHead[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        if (strLine.Split(';') != null)
                        {
                            DataLine = strLine.Split(';');
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < DataLine.Length; j++)
                            {

                                dr[j] = DelStr(DataLine[j]);


                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if (DataLine != null && DataLine.Length > 0)
                {
                    dt.DefaultView.Sort = tableHead[0] + " " + "asc";
                }

                sr.Close();
                fs.Close();
                return dt;
            }
        }
        /// <summary>
        /// 去除字符串中的空格，换行，引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelStr(string str)
        {
            string data = str;
            string strold = "\""; string strnew = "";
            data = str.Replace(strold, strnew);
            strold = " "; strnew = "";
            data = data.Replace(strold, strnew);
            strold = "\n"; strnew = "";
            data = data.Replace(strold, strnew);
            return data;
        }
        #endregion

        #region DataTable中数据写入到CSV文件中+static void SaveCSV(DataTable dt, string fullPath, FileMode openMode)
        /// <summary>
        ///将DataTable中数据写入到CSV文件中 
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fullPath">CSV的文件路径</param>
        /// <param name="openMode">文件打开方式</param>
        public static void SaveCSV(DataTable dt, string fullPath, FileMode openMode)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            using (FileStream fs = new FileStream(fullPath, openMode, System.IO.FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";
                //写出列名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                //写出各行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        data += str;
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
            }
        }
        #endregion

        #region 将DataRow中数据写入到CSV文件中+static void SaveCSVRow(DataRow dt, string fullPath)
        /// <summary>
        /// 将DataRow中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataRow</param>
        /// <param name="fullPath">CSV的文件路径</param>
        public static void SaveCSVRow(DataRow dt, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            using (FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";
                for (int j = 0; j < dt.ItemArray.Length; j++)
                {
                    string str = dt[j].ToString();
                    data += str;
                    if (j < dt.ItemArray.Length - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
        }
        #endregion

        #region 将DataGridViewRow数据条存储到CSV文件中+static void SaveDGRow(DataGridViewRow dt, string fullPath)
        /// <summary>
        /// 将DataGridViewRow数据条存储到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataGridViewRow</param>
        /// <param name="fullPath">CSV的文件路径</param>
        //public static void SaveDGRow(DataGridViewRow dt, string fullPath)
        //{
        // FileInfo fi = new FileInfo(fullPath);
        // if (!fi.Directory.Exists)
        // {
        // fi.Directory.Create();
        // }
        // using (FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
        // {
        // StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        // string data = "";
        // for (int j = 1; j < dt.Cells.Count; j++)
        // {
        // string str = dt.Cells[j].Value.ToString();
        // data += str;
        // if (j < dt.Cells.Count - 1)
        // {
        // data += ",";
        // }
        // }
        // sw.WriteLine(data);
        // }
        //} 
        #endregion

        #region 将CSV文件的数据读取判断是否存在数据中+static bool ChargeStr(string filePath, string str)
        /// <summary>
        /// 将CSV文件的数据读取判断是否存在数据中
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="str">对比的数据</param>
        /// <returns>是否存在数据</returns>
        public static bool ChargeStr(string filePath, string str)
        {

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            using (FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, encoding);
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] DataLine = null;
                bool flag = false;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (strLine.Split(',') != null)
                    {
                        DataLine = strLine.Split(',');

                        for (int j = 0; j < DataLine.Length; j++)
                        {

                            if (str == DelStr(DataLine[j]))
                            {
                                flag = true;
                            }

                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }
                return flag;
            }
        }
        #endregion

        #region 将CSV文件中需要的数据转化为字符串+static string CSVToStr(string filePath, string indexl)
        /// <summary>
        /// 将CSV文件中需要的数据转化为字符串
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="indexl">需要的字段索引，如“235”要的是2,3,5列数据，如果为空默认为全部</param>
        /// <returns>返回读取了CSV数据的string字符串</returns>
        public static string CSVToStr(string filePath, string indexl)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            string str = "";
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            using (FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, encoding);
                int columnCount = 0;

                string strLine = "";
                //记录每行记录中的各字段内容
                string[] DataLine = null;
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (strLine.Split(',') != null)
                    {
                        DataLine = strLine.Split(',');
                        columnCount = DataLine.Length;
                        for (int j = 0; j < columnCount; j++)
                        {
                            if (indexl == null)
                            {
                                str += DelStr(DataLine[j]);
                                if (j == columnCount - 1)
                                {
                                    str += "|";
                                }
                                else
                                {
                                    str += ",";
                                }


                            }
                            else
                            {
                                for (int i = 1; i < indexl.Length; i++)
                                {
                                    int inx = Convert.ToInt32(indexl.Substring(i - 1, 1));
                                    str += DelStr(DataLine[inx]);
                                    str += ",";
                                }
                            }

                        }

                    }
                }
            }
            return str;
        }
        #endregion
    }
}