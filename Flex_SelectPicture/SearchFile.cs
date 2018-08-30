using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Flex_SelectPicture
{
    class SearchFiles
    {
        const int EVERYTHING_OK = 0;
        const int EVERYTHING_ERROR_MEMORY = 1;
        const int EVERYTHING_ERROR_IPC = 2;
        const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        const int EVERYTHING_ERROR_CREATEWINDOW = 4;
        const int EVERYTHING_ERROR_CREATETHREAD = 5;
        const int EVERYTHING_ERROR_INVALIDINDEX = 6;
        const int EVERYTHING_ERROR_INVALIDCALL = 7;

       public  const int EVERYTHING_REQUEST_FILE_NAME = 0x00000001;
       public const int EVERYTHING_REQUEST_PATH = 0x00000002;
        const int EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 0x00000004;
        const int EVERYTHING_REQUEST_EXTENSION = 0x00000008;
        const int EVERYTHING_REQUEST_SIZE = 0x00000010;
        const int EVERYTHING_REQUEST_DATE_CREATED = 0x00000020;
        public const int EVERYTHING_REQUEST_DATE_MODIFIED = 0x00000040;
        const int EVERYTHING_REQUEST_DATE_ACCESSED = 0x00000080;
        const int EVERYTHING_REQUEST_ATTRIBUTES = 0x00000100;
        const int EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 0x00000200;
        const int EVERYTHING_REQUEST_RUN_COUNT = 0x00000400;
        const int EVERYTHING_REQUEST_DATE_RUN = 0x00000800;
        const int EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 0x00001000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 0x00002000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 0x00004000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000;

        const int EVERYTHING_SORT_NAME_ASCENDING = 1;
        const int EVERYTHING_SORT_NAME_DESCENDING = 2;
        const int EVERYTHING_SORT_PATH_ASCENDING = 3;
        const int EVERYTHING_SORT_PATH_DESCENDING = 4;
        const int EVERYTHING_SORT_SIZE_ASCENDING = 5;
        const int EVERYTHING_SORT_SIZE_DESCENDING = 6;
        const int EVERYTHING_SORT_EXTENSION_ASCENDING = 7;
        const int EVERYTHING_SORT_EXTENSION_DESCENDING = 8;
        const int EVERYTHING_SORT_TYPE_NAME_ASCENDING = 9;
        const int EVERYTHING_SORT_TYPE_NAME_DESCENDING = 10;
        const int EVERYTHING_SORT_DATE_CREATED_ASCENDING = 11;
        const int EVERYTHING_SORT_DATE_CREATED_DESCENDING = 12;
        const int EVERYTHING_SORT_DATE_MODIFIED_ASCENDING = 13;
        const int EVERYTHING_SORT_DATE_MODIFIED_DESCENDING = 14;
        const int EVERYTHING_SORT_ATTRIBUTES_ASCENDING = 15;
        const int EVERYTHING_SORT_ATTRIBUTES_DESCENDING = 16;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING = 17;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING = 18;
        const int EVERYTHING_SORT_RUN_COUNT_ASCENDING = 19;
        const int EVERYTHING_SORT_RUN_COUNT_DESCENDING = 20;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_ASCENDING = 21;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_DESCENDING = 22;
        const int EVERYTHING_SORT_DATE_ACCESSED_ASCENDING = 23;
        const int EVERYTHING_SORT_DATE_ACCESSED_DESCENDING = 24;
        const int EVERYTHING_SORT_DATE_RUN_ASCENDING = 25;
        const int EVERYTHING_SORT_DATE_RUN_DESCENDING = 26;

        const int EVERYTHING_TARGET_MACHINE_X86 = 1;
        const int EVERYTHING_TARGET_MACHINE_X64 = 2;
        const int EVERYTHING_TARGET_MACHINE_ARM = 3;

        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern int Everything_SetSearch(string lpSearchString);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetRegex(bool bEnable);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMax(UInt32 dwMax);
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetOffset(UInt32 dwOffset);

        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchPath();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchCase();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchWholeWord();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetRegex();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetMax();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetOffset();
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetSearch();
        [DllImport("Everything32.dll")]
        public static extern int Everything_GetLastError();

        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern bool Everything_Query(bool bWait);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetNumFileResults();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetNumFolderResults();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetNumResults();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetTotFileResults();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetTotFolderResults();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetTotResults();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsVolumeResult(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsFolderResult(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsFileResult(UInt32 nIndex);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathName(UInt32 nIndex, StringBuilder lpString, UInt32 nMaxCount);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultPath(UInt32 nIndex);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultFileName(UInt32 nIndex);

        [DllImport("Everything32.dll")]
        public static extern void Everything_Reset();
        [DllImport("Everything32.dll")]
        public static extern void Everything_CleanUp();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetMajorVersion();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetMinorVersion();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetRevision();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetBuildNumber();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_Exit();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsDBLoaded();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsAdmin();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsAppData();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_RebuildDB();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_UpdateAllFolderIndexes();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_SaveDB();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_SaveRunHistory();
        [DllImport("Everything32.dll")]
        public static extern bool Everything_DeleteRunHistory();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetTargetMachine();

        // Everything 1.4
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetSort(UInt32 dwSortType);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetSort();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetResultListSort();
        [DllImport("Everything32.dll")]
        public static extern void Everything_SetRequestFlags(UInt32 dwRequestFlags);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetRequestFlags();
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetResultListRequestFlags();
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultExtension(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultSize(UInt32 nIndex, out long lpFileSize);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultDateCreated(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultDateModified(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultDateAccessed(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetResultAttributes(UInt32 nIndex);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultFileListFileName(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetResultRunCount(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultDateRun(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetResultDateRecentlyChanged(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultHighlightedFileName(UInt32 nIndex);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultHighlightedPath(UInt32 nIndex);
        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern string Everything_GetResultHighlightedFullPathAndFileName(UInt32 nIndex);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_GetRunCountFromFileName(string lpFileName);
        [DllImport("Everything32.dll")]
        public static extern bool Everything_SetRunCountFromFileName(string lpFileName, UInt32 dwRunCount);
        [DllImport("Everything32.dll")]
        public static extern UInt32 Everything_IncRunCountFromFileName(string lpFileName);

        /// <summary>
        /// 查找图片
        /// </summary>
        /// <param name="gl_Barcode"></param>
        public List<string> FindImage(string gl_Barcode)
        {
            UInt32 i;
            const int bufsize = 260;
            StringBuilder buf = new StringBuilder(bufsize);
            List<string> paths = new List<string>();
            // set the search
            Everything_SetSearch(gl_Barcode);
            // request name and size
            Everything_SetRequestFlags(EVERYTHING_REQUEST_FILE_NAME | EVERYTHING_REQUEST_PATH | EVERYTHING_REQUEST_DATE_MODIFIED);

            Everything_SetSort(13);
            // execute the query
            SearchFiles.Everything_Query(true);
            // loop through the results, adding each result to the listbox. 
            for (i = 0; i < Everything_GetNumResults(); i++)
            {
                long date_modified;
                long size;
                Everything_GetResultFullPathName(i, buf, bufsize);
                Everything_GetResultSize(i, out size);
                paths.Add(buf.ToString());
            }
            return paths;
        }
    }


    /// <summary>
    ///     查找类
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    class MySearch
    {
        public MySearch(DateTime searchdate,string avi)
        {
            search_date = searchdate;
            AVI = avi;
        }
        public  DateTime search_date;
        string AVI = string.Empty;
        string gl_NGLog = string.Empty;
        Dictionary<string, Thread> dic_SearchThread = new Dictionary<string, Thread>();//用于存储查找线程
        Dictionary<string, string> dic_ThreadsDone = new Dictionary<string, string>();//判断线程是否完成
        int thread_Done = 0;
        List<string> dirs = new List<string>();
        public  bool complete = false;

        public void SearchStrUseTh()
        {
            if (AVI.Contains("1")) { gl_NGLog = GlobalVar.gl_val_Machine1_S1_TestResult; GlobalVar.yeild_check_AVI1_result.Clear(); }
            if (AVI.Contains("2")) { gl_NGLog = GlobalVar.gl_val_Machine2_S1_TestResult; GlobalVar.yeild_check_AVI2_result.Clear(); }
            if (AVI.Contains("3")) { gl_NGLog = GlobalVar.gl_val_Machine3_S1_TestResult; GlobalVar.yeild_check_AVI3_result.Clear(); }
            gl_NGLog += @"\" + search_date.ToString("yyyy-MM-dd");
            dirs.Add(gl_NGLog);
            Thread loadFloder_th = new Thread(search);
            loadFloder_th.IsBackground = true;
            loadFloder_th.Start();
            Thread.Sleep(2000);

        }

        private void loadFloder()
        {
            if (dirs.Count > 0)
            {
                dic_SearchThread.Clear();
                dic_ThreadsDone.Clear();
                foreach (string dir in dirs)
                {
                    Thread searchTh = new Thread(search);
                    searchTh.IsBackground = true;
                    searchTh.Name = dir;
                    searchTh.Start(dir);
                    if (!dic_SearchThread.ContainsKey(dir))
                    {
                        dic_SearchThread.Add(dir, searchTh);
                    }
                    if (!dic_ThreadsDone.ContainsKey(dir))
                        dic_ThreadsDone.Add(dir, "work");
                }
            }
            while (true)
            {
                foreach (string item in dirs)
                {
                    if (dic_SearchThread.ContainsKey(item) && dic_ThreadsDone[item] == "done")
                    {
                        dic_SearchThread[item].Abort();
                        dic_SearchThread.Remove(item);
                    }
                }
                if (dic_SearchThread.Count == 0)
                {
                    dic_ThreadsDone.Clear();

                    break;//结束循环

                }
                Thread.Sleep(10);
            }
        }

        private void search(object dir)
        {
            try
            {
                MyFunctions myf = new MyFunctions();
                string paths = dir as string;
                string[] filepath = Directory.GetFiles(gl_NGLog, search_date.ToString("yyyyMMdd") + "*.csv");
                foreach (string path in filepath)
                {
                    if (AVI.Contains("1")) GlobalVar.yeild_check_AVI1_result.AddRange(myf.SearchResult(path));
                    if (AVI.Contains("2")) GlobalVar.yeild_check_AVI2_result.AddRange(myf.SearchResult(path));
                    if (AVI.Contains("3")) GlobalVar.yeild_check_AVI3_result.AddRange(myf.SearchResult(path));
                }
                complete = true;
            } catch (Exception ex)
            {
                MessageBox.Show("未找到数据源，请重启程序！");
                System.Environment.Exit(0);
            }
        }
        private void searchDir(object dir)
        {
            string path = dir as string;
            List<string> list = new List<string>();
            string commend = "findstr /s  /i /V NoBarC " + path + @"\*.csv";//查找
            Process process = new Process();//创建进程对象
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C" + commend;//执行完就退出
            startInfo.UseShellExecute = false;//不使用shell启动进程
            startInfo.RedirectStandardInput = false;//不重定向输入
            startInfo.RedirectStandardOutput = true;//重定向输出
            startInfo.CreateNoWindow = true;//不创建窗口
            process.StartInfo = startInfo;
            try
            {
                if (process.Start())
                {
                    string temp = process.StandardOutput.ReadToEnd();
                    string[] filePaths = Regex.Split(temp, "\r\n", RegexOptions.IgnoreCase);//最后一个为空
                    list.AddRange(filePaths.ToList());
                }
                foreach (string rs in list)
                {
                    string[] arrayStr = rs.Split(',');
                    if (!rs.Contains("检测时间") && arrayStr.Length > 5 && arrayStr[1] != null && arrayStr[1] != "\\" && arrayStr[1] != "NoBarC")
                    {
                        string test = arrayStr[1] + "|" + arrayStr[2] + "|" + arrayStr[3] + "|" + arrayStr[4] + "|" + arrayStr[5];//增加工位
                        GlobalVar.gl_TestResult.Add(test);
                    }
                }
                thread_Done++;
                dic_ThreadsDone[path] = "done";
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }
            }
        }



    }


}
