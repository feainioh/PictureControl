using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    class MyFunctions
    {
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);




        /// <summary>
        ///     写正常日志
        /// </summary>
        /// <param name="Logstr" type="string">
        ///     <para>
        ///         日志内容
        ///     </para>
        /// </param>
        public void writeCommLog(string Logstr)
        {
            try
            {
                string dir_Path = Application.StartupPath + @"\log\Common\" + DateTime.Now.ToString("yyyy-MM-dd");
                string file_Path = dir_Path + "\\" + DateTime.Now.ToString("yyyyMMdd_HH") + ".txt";
                if (!Directory.Exists(dir_Path))
                {
                    Directory.CreateDirectory(dir_Path);
                }
                if (!File.Exists(file_Path))
                {
                    File.Create(file_Path);
                }
                StreamWriter sw = new StreamWriter(file_Path, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\t\t" + Logstr);
                sw.Close();
                deleteLogFiles(dir_Path);
            }
            catch { }
        }

        /// <summary>
        ///     写异常日志
        /// </summary>
        /// <param name="Logstr" type="string">
        ///     <para>
        ///         日志内容
        ///     </para>
        /// </param>
        public void writeErrorLog(string Logstr)
        {
            try
            {
                string dir_Path = Application.StartupPath + @"\log\Error\";
                string file_Path = dir_Path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!Directory.Exists(dir_Path))
                {
                    Directory.CreateDirectory(dir_Path);
                }
                if (!File.Exists(file_Path))
                {
                    File.Create(file_Path);
                }
                StreamWriter sw = new StreamWriter(file_Path, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\t\t" + Logstr);
                sw.Close();
                deleteLogFiles(dir_Path);
            }
            catch { }
        }

        /// <summary>
        ///     删除过期日志
        /// </summary>
        /// <param name="dir_Path" type="string">
        ///     <para>
        ///         日志目录
        ///     </para>
        /// </param>
        private void deleteLogFiles(string dir_Path)
        {
            DirectoryInfo dir = new DirectoryInfo(dir_Path);
            FileInfo[] files = dir.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                if ((DateTime.Now.Subtract(file.LastWriteTime)).Days > 10)
                {
                    file.Delete();
                }
            }
        }

       

        /// <summary>
        /// 读取指定路径下的测试项
        /// </summary>
        /// <param name="path_TestVal">存储路径</param>
        /// <param name="aVI_TestItems">测试项字典</param>
        internal void getTestItem(string path_TestVal, Dictionary<string, string[]> aVI_TestItems)
        {
            string[] arr_files = Directory.GetFiles(path_TestVal, "*.csv");
            foreach(string path in arr_files)
            {
                // string conn_str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                //+ path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
                string conn_str = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source="
               + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                string cmdStr = "select Left,Top,Right,Buttom,检测点 from [Sheet4$]"; 
                var conn = new System.Data.OleDb.OleDbConnection(conn_str);
                var retData = new System.Data.DataTable();
                try
                {
                    conn.Open();
                    var adapter = new System.Data.OleDb.OleDbDataAdapter(cmdStr, conn);
                    adapter.Fill(retData);
                    for(int i = 0; i < retData.Rows.Count-1; i++)
                    {
                        if (retData.Rows[i][4].ToString() != "")
                        {
                        string[] point = { retData.Rows[i][0].ToString(), retData.Rows[i][1].ToString(), retData.Rows[i][2].ToString(), retData.Rows[i][3].ToString()};
                        string name = retData.Rows[i][4].ToString();
                        if (!aVI_TestItems.ContainsKey(name))
                            aVI_TestItems.Add(name,point);
                        }
                    }
                }
                catch (OleDbException exception)
                {
                    writeErrorLog("查询测试项异常:" + exception.Message);
                    string filePath = path.Substring(0, path.LastIndexOf('\\'));
                    string fileName = path.Substring(path.LastIndexOf('\\') + 1, path.Length - path.LastIndexOf('\\') - 1);
                    conn_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                        + filePath + ";Extended Properties='text;HDR=YES;FMT=Delimited';";        
                        string cmd_Str = "select * from [" + fileName + "]" ;
                     conn = new OleDbConnection(conn_str);
                     retData = new DataTable();
                    try
                    {
                        conn.Open();
                        var adapter = new OleDbDataAdapter(cmd_Str, conn);
                        adapter.Fill(retData);
                        for (int i = 0; i < retData.Rows.Count; i++)
                        {
                            if (retData.Rows[i][6].ToString() != "")
                            {
                                string[] point = { retData.Rows[i][1].ToString(), retData.Rows[i][2].ToString(), retData.Rows[i][3].ToString(), retData.Rows[i][4].ToString() };
                                string name = retData.Rows[i][6].ToString();
                                if (!aVI_TestItems.ContainsKey(name))
                                    aVI_TestItems.Add(name, point);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        writeErrorLog("查询测试项异常:" + ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    writeErrorLog("查询测试项异常:"+ex.ToString());
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }
        internal void searchImage()
        {
            List<string> pic_S1 = new List<string>();
            List<string> pic_S2 = new List<string>();
            List<string> pic_S3 = new List<string>();
            SearchFiles search = new SearchFiles();
            pic_S1.AddRange(search.FindImage("主站" + GlobalVar.gl_Barcode));
            pic_S2.AddRange(search.FindImage("从站" + GlobalVar.gl_Barcode));
            foreach (string path in pic_S2)
            {
                if (path.Contains(GlobalVar.gl_val_Machine1_S3_TestResult) || path.Contains(GlobalVar.gl_val_Machine2_S3_TestResult) || path.Contains(GlobalVar.gl_val_Machine3_S3_TestResult))
                {
                    pic_S3.Add(path);
                }
            }
            foreach (string path in pic_S3)
            {
                if (pic_S2.Contains(path)) pic_S2.Remove(path);
            }
            if (pic_S1.Count > 1)
            {
                for (int j = 0; j < pic_S1.Count - 1; j++)
                {
                    if (pic_S1[j + 1] != "" && pic_S1[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S1[j]);
                        FileInfo file_1 = new FileInfo(pic_S1[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S1[j];
                            pic_S1[j] = pic_S1[j + 1];
                            pic_S1[j + 1] = temp;
                        }
                    }
                }
                if (pic_S1[pic_S1.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    GlobalVar.Pic_S1_fileName = pic_S1[pic_S1.Count - 1];
            }
            else if (pic_S1.Count == 1)
            {
                if (pic_S1[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    GlobalVar.Pic_S1_fileName = pic_S1[0];
            }
            if (pic_S2.Count > 1)
            {
                for (int j = 0; j < pic_S2.Count - 1; j++)
                {
                    if (pic_S2[j + 1] != "" && pic_S2[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S2[j]);
                        FileInfo file_1 = new FileInfo(pic_S2[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S2[j];
                            pic_S2[j] = pic_S2[j + 1];
                            pic_S2[j + 1] = temp;
                        }
                    }
                }
                if (pic_S2[pic_S2.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    GlobalVar.Pic_S2_fileName = pic_S2[pic_S2.Count - 1];
            }
            else if (pic_S2.Count == 1)
            {
                if (pic_S2[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    GlobalVar.Pic_S2_fileName = pic_S2[0];
            }
            if (pic_S3.Count > 1)
            {
                for (int j = 0; j < pic_S3.Count - 1; j++)
                {
                    if (pic_S3[j + 1] != "" && pic_S3[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S3[j]);
                        FileInfo file_1 = new FileInfo(pic_S3[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S3[j];
                            pic_S3[j] = pic_S3[j + 1];
                            pic_S3[j + 1] = temp;
                        }
                    }
                }
                if (pic_S3[pic_S3.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    GlobalVar.Pic_S3_fileName = pic_S3[pic_S3.Count - 1];
            }
            else if (pic_S3.Count == 1)
            {
                if (pic_S3[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    GlobalVar.Pic_S3_fileName = pic_S3[0];
            }
        }

        internal string[] searchImageByBarcode(string barcode)
        {
            string[] paths = new string[] {"","","" };
            List<string> pic_S1 = new List<string>();
            List<string> pic_S2 = new List<string>();
            List<string> pic_S3 = new List<string>();
            SearchFiles search = new SearchFiles();
            pic_S1.AddRange(search.FindImage("主站" + barcode));
            pic_S2.AddRange(search.FindImage("从站" + barcode));
            foreach (string path in pic_S2)
            {
                if (path.Contains(GlobalVar.gl_key_Machine1_S3_TestResult) || path.Contains(GlobalVar.gl_key_Machine2_S3_TestResult) || path.Contains(GlobalVar.gl_key_Machine3_S3_TestResult))
                {
                    pic_S3.Add(path);
                }
            }
            foreach (string path in pic_S3)
            {
                if (pic_S2.Contains(path)) pic_S2.Remove(path);
            }
            if (pic_S1.Count > 1)
            {
                for (int j = 0; j < pic_S1.Count - 1; j++)
                {
                    if (pic_S1[j + 1] != "" && pic_S1[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S1[j]);
                        FileInfo file_1 = new FileInfo(pic_S1[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S1[j];
                            pic_S1[j] = pic_S1[j + 1];
                            pic_S1[j + 1] = temp;
                        }
                    }
                }
                if (pic_S1[pic_S1.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    paths[0] = pic_S1[pic_S1.Count - 1];
            }
            else if (pic_S1.Count == 1)
            {
                if (pic_S1[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    paths[0] = pic_S1[0];
            }
            if (pic_S2.Count > 1)
            {
                for (int j = 0; j < pic_S2.Count - 1; j++)
                {
                    if (pic_S2[j + 1] != "" && pic_S2[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S2[j]);
                        FileInfo file_1 = new FileInfo(pic_S2[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S2[j];
                            pic_S2[j] = pic_S2[j + 1];
                            pic_S2[j + 1] = temp;
                        }
                    }
                }
                if (pic_S2[pic_S2.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    paths[1] = pic_S2[pic_S2.Count - 1];
            }
            else if (pic_S2.Count == 1)
            {
                if (pic_S2[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    paths[1] = pic_S2[0];
            }
            if (pic_S3.Count > 1)
            {
                for (int j = 0; j < pic_S3.Count - 1; j++)
                {
                    if (pic_S3[j + 1] != "" && pic_S3[j] != "")
                    {
                        string temp;
                        FileInfo file = new FileInfo(pic_S3[j]);
                        FileInfo file_1 = new FileInfo(pic_S3[j + 1]);
                        if ((file.CreationTime - file_1.CreationTime).TotalDays > 0)
                        {
                            temp = pic_S3[j];
                            pic_S3[j] = pic_S3[j + 1];
                            pic_S3[j + 1] = temp;
                        }
                    }
                }
                if (pic_S3[pic_S3.Count - 1].Contains(DateTime.Now.ToString("yyyyMMdd")))//只查当天
                    paths[2] = pic_S3[pic_S3.Count - 1];
            }
            else if (pic_S3.Count == 1)
            {
                if (pic_S3[0].Contains(DateTime.Now.ToString("yyyyMMdd")))
                    paths[2] = pic_S3[0];
            }
            return paths;
        }
        /// <summary>
        /// 查找结果
        /// </summary>
        internal List<string> SearchReult(string fullpath)
        {
            List<string> result = new List<string>();
            DataTable result_Table = searchResult(fullpath,GlobalVar.gl_Barcode);
            for(int i = 0; i < result_Table.Rows.Count ; i++)
            {
                result.Add(result_Table.Rows[i][0]+"|"+ result_Table.Rows[i][1] + "|"+ result_Table.Rows[i][2] + "|" + result_Table.Rows[i][3] + "|" + result_Table.Rows[i][4] + "|" + result_Table.Rows[i][5] + "|" + result_Table.Rows[i][6]);
            }
            return result;
        }

        public List<string> SearchResult(string fullpath)
        {
            List<string> result = new List<string>();
            DataTable result_Table = searchResult(fullpath);
            for (int i = 0; i < result_Table.Rows.Count - 1; i++)
            {
                result.Add(result_Table.Rows[i][0] + "|" + result_Table.Rows[i][1] + "|" + result_Table.Rows[i][2] + "|" + result_Table.Rows[i][3] + "|" + result_Table.Rows[i][4] + "|" + result_Table.Rows[i][5] + "|" + result_Table.Rows[i][6] + "|" + result_Table.Rows[i][7] + "|" + result_Table.Rows[i][8]);
            }
            return result;
        }

        private DataTable searchResult(string fullPath)
        {
            string filePath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            string fileName = fullPath.Substring(fullPath.LastIndexOf('\\') + 1, fullPath.Length - fullPath.LastIndexOf('\\') - 1);
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
               + filePath + ";Extended Properties='Text;HDR=YES;FMT=Delimited;IMEX=1;'";

            string cmdStr = "select 检测时间,NG项,结果,工站号,主站主,主站副,从站主,从站副,BarCode from[" + fileName + "]";
            var conn = new System.Data.OleDb.OleDbConnection(connStr);
            var retData = new System.Data.DataTable();
            try
            {
                conn.Open();
                var adapter = new System.Data.OleDb.OleDbDataAdapter(cmdStr, conn);
                adapter.Fill(retData);
            }
            catch (System.Exception ex)
            {
                writeErrorLog("查询异常" + ex.ToString());
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return retData;
        }


        /// <summary>
        /// 查找csv文件
        /// </summary>
        /// <returns></returns>
        public List<string> FindCSVByDate()
        {
            UInt32 i;
            const int bufsize = 260;
            StringBuilder buf = new StringBuilder(bufsize);
            List<string> paths = new List<string>();

            string date = DateTime.Now.ToString("yyyyMMdd");
            SearchFiles.Everything_SetSearch(date+"*.csv");
            // request name and size
            SearchFiles.Everything_SetRequestFlags(SearchFiles.EVERYTHING_REQUEST_FILE_NAME | SearchFiles.EVERYTHING_REQUEST_PATH | SearchFiles.EVERYTHING_REQUEST_DATE_MODIFIED);

            SearchFiles.Everything_SetSort(13);
            // execute the query
            SearchFiles.Everything_Query(true);
            // loop through the results, adding each result to the listbox. 
            for (i = 0; i < SearchFiles.Everything_GetNumResults(); i++)
            {
                long date_modified;
                long size;
                SearchFiles.Everything_GetResultDateModified(i, out date_modified);
                SearchFiles.Everything_GetResultFullPathName(i, buf, bufsize);
                SearchFiles.Everything_GetResultSize(i, out size);
                paths.Add(buf.ToString());
            }
            return paths;

        }

        /// <summary>
        /// 根据barcode查找测试结果
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private  DataTable searchResult(string fullPath, string barcode)
        {
            string filePath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            string fileName = fullPath.Substring(fullPath.LastIndexOf('\\') + 1, fullPath.Length - fullPath.LastIndexOf('\\') - 1);
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
               + filePath + ";Extended Properties='Text;HDR=YES;FMT=Delimited;IMEX=1;'";

            string cmdStr = "select 检测时间,结果,工站号,主站主,主站副,从站主,从站副 from[" + fileName + "] where BarCode='" + barcode + "'";
            var conn = new System.Data.OleDb.OleDbConnection(connStr);
            var retData = new System.Data.DataTable();
            try
            {
                conn.Open();
                var adapter = new System.Data.OleDb.OleDbDataAdapter(cmdStr, conn);
                adapter.Fill(retData);
            }
            catch (System.Exception ex)
            {
                writeErrorLog("查询异常"+ex.ToString());
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return retData;
        }



    }
}
