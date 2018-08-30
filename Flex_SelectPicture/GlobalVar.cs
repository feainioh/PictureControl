using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_SelectPicture
{
    class GlobalVar
    {
        ////////////////////////////////////////////CONFIG////////////////////////////////////////////////////////////
        public static bool gl_b_selectOne = true;
        public static string gl_section_Global = "Global";

        public static string gl_key_Machine1_Check = "SelectMachine1";//查询机台一测试结果
        public static string gl_key_Machine2_Check = "SelectMachine2";//查询机台二测试结果
        public static string gl_key_Machine3_Check = "SelectMachine3";//查询机台三测试结果
        public static string gl_val_Machine1_Check = "1";//查询机台一 1:查询 0:不查询
        public static string gl_val_Machine2_Check = "0";//查询机台一 1:查询 0:不查询
        public static string gl_val_Machine3_Check = "0";//查询机台一 1:查询 0:不查询

        public static string gl_key_Machine1_S1_TestVal = "Machine1_S1_TestVal";//机台一 主站测试项地址
        public static string gl_key_Machine2_S1_TestVal = "Machine2_S1_TestVal"; //机台二 主站测试项地址
        public static string gl_key_Machine3_S1_TestVal = "Machine3_S1_TestVal"; //机台三 主站测试项地址
        public static string gl_val_Machine1_S1_TestVal = "";//机台一 主站测试项存储地址
        public static string gl_val_Machine2_S1_TestVal = "";//机台二 主站测试项存储地址
        public static string gl_val_Machine3_S1_TestVal = "";//机台三 主站测试项存储地址
        public static string gl_key_Machine1_S2_TestVal = "Machine1_S2_TestVal";//机台一 从站测试项地址
        public static string gl_key_Machine2_S2_TestVal = "Machine2_S2_TestVal"; //机台二 从站测试项地址
        public static string gl_key_Machine3_S2_TestVal = "Machine3_S2_TestVal"; //机台三 从站测试项地址
        public static string gl_val_Machine1_S2_TestVal = "";//机台一 从站测试项存储地址
        public static string gl_val_Machine2_S2_TestVal = "";//机台二 从站测试项存储地址
        public static string gl_val_Machine3_S2_TestVal = "";//机台三 从站测试项存储地址
        public static string gl_key_Machine1_S3_TestVal = "Machine1_S3_TestVal";//机台一 从站2测试项地址
        public static string gl_key_Machine2_S3_TestVal = "Machine2_S3_TestVal"; //机台二 从站2测试项地址
        public static string gl_key_Machine3_S3_TestVal = "Machine3_S3_TestVal"; //机台三 从站2测试项地址
        public static string gl_val_Machine1_S3_TestVal = "";//机台一 从站2测试项存储地址
        public static string gl_val_Machine2_S3_TestVal = "";//机台二 从站2测试项存储地址
        public static string gl_val_Machine3_S3_TestVal = "";//机台三 从站2测试项存储地址
        public static string gl_key_Machine1_S1_TestResult = "Machine1_S1_TestReslut";//机台一 主站测试结果地址
        public static string gl_key_Machine2_S1_TestResult = "Machine1_S1_TestReslut";//机台二 主站测试结果地址
        public static string gl_key_Machine3_S1_TestResult = "Machine1_S1_TestReslut";//机台三 主站测试结果地址
        public static string gl_val_Machine1_S1_TestResult = "";//机台一 主站测试结果存储地址
        public static string gl_val_Machine2_S1_TestResult = "";//机台二 主站测试结果存储地址
        public static string gl_val_Machine3_S1_TestResult = "";//机台三 主站测试结果存储地址
        public static string gl_key_Machine1_S2_TestResult = "Machine1_S2_TestReslut";//机台一 从站测试结果地址
        public static string gl_key_Machine2_S2_TestResult = "Machine1_S2_TestReslut";//机台二 从站测试结果地址
        public static string gl_key_Machine3_S2_TestResult = "Machine1_S2_TestReslut";//机台三 从站测试结果地址
        public static string gl_val_Machine1_S2_TestResult = "";//机台一 从站测试结果存储地址
        public static string gl_val_Machine2_S2_TestResult = "";//机台二 从站测试结果存储地址
        public static string gl_val_Machine3_S2_TestResult = "";//机台三 从站测试结果存储地址
        public static string gl_key_Machine1_S3_TestResult = "Machine1_S3_TestReslut";//机台一 从站2测试结果地址
        public static string gl_key_Machine2_S3_TestResult = "Machine1_S3_TestReslut";//机台二 从站2测试结果地址
        public static string gl_key_Machine3_S3_TestResult = "Machine1_S3_TestReslut";//机台三 从站2测试结果地址
        public static string gl_val_Machine1_S3_TestResult = "";//机台一 从站2测试结果存储地址
        public static string gl_val_Machine2_S3_TestResult = "";//机台二 从站2测试结果存储地址
        public static string gl_val_Machine3_S3_TestResult = "";//机台三 从站2测试结果存储地址

        public static string gl_section_Yeild = "Yeild";
        public static string gl_key_check_AVI1 = "CheckAVI-1";//分析AVI1良率
        public static string gl_key_check_AVI2 = "CheckAVI-2";//分析AVI2良率
        public static string gl_key_check_AVI3 = "CheckAVI-3";//分析AVI3良率
        public static string gl_key_check_stations = "CheckStations";//分析工站
        public static string gl_key_check_stationscount = "CheckStationsCount";//分析的工站数量
        public static string gl_val_check_AVI1 = "0";//确认分析AVI1良率 0：不分析 1：分析
        public static string gl_val_check_AVI2 = "0";//确认分析AVI2良率 0：不分析 1：分析
        public static string gl_val_check_AVI3 = "0";//确认分析AVI3良率 0：不分析 1：分析
        public static string gl_val_check_stations = "0";//确认分析工站 0：不分析 1：分析
        public static string gl_val_check_stationscount = "0";//分析工站数量  maximum:8


        ////////////////////////////////////////////GLOBAL////////////////////////////////////////////////////////////
        public static Dictionary<string, string[]> AVI1_S1_TestItems = new Dictionary<string, string[]>();//机台一 主站测试项
        public static Dictionary<string, string[]> AVI1_S2_TestItems = new Dictionary<string, string[]>();//机台一 从站测试项
        public static Dictionary<string, string[]> AVI1_S3_TestItems = new Dictionary<string, string[]>();//机台一 从站2测试项
        public static Dictionary<string, string[]> AVI2_S1_TestItems = new Dictionary<string, string[]>();//机台二 主站测试项
        public static Dictionary<string, string[]> AVI2_S2_TestItems = new Dictionary<string, string[]>();//机台二 从站测试项
        public static Dictionary<string, string[]> AVI2_S3_TestItems = new Dictionary<string, string[]>();//机台二 从站2测试项
        public static Dictionary<string, string[]> AVI3_S1_TestItems = new Dictionary<string, string[]>();//机台三 主站测试项
        public static Dictionary<string, string[]> AVI3_S2_TestItems = new Dictionary<string, string[]>();//机台三 从站测试项
        public static Dictionary<string, string[]> AVI3_S3_TestItems = new Dictionary<string, string[]>();//机台三 从站测试项

        public static List<string> gl_TestResult = new List<string>();//测试结果

        public static List<string> gl_S1_1_Result = new List<string>();//主站主结果
        public static List<string> gl_S1_2_Result = new List<string>();//主站副结果
        public static List<string> gl_S2_1_Result = new List<string>();//从站主结果
        public static List<string> gl_S2_2_Result = new List<string>();//从站副结果


        public static string gl_ResultFrom="0";

        public static string Pic_S1_fileName = "";//主站图片
        public static string Pic_S2_fileName = "";//从站图片
        public static string Pic_S3_fileName = "";//从站2图片
        public static string gl_Barcode = "";//查询条码
        public static int gl_BarcodeLength = 20;//条码长度
        public static DateTime gl_testDate = DateTime.Now;//测试时间，默认是当前时间

        public static bool gl_appIsRunning = false;







        ////////////////////////////////////////////YEILD////////////////////////////////////////////////////////////
        public static bool yeild_check_AVI1 = false;//是否分析AVI1的良率
        public static bool yeild_check_AVI2 = false;//是否分析AVI2的良率
        public static bool yeild_check_AVI3 = false;//是否分析AVI3的良率
        public static int yeild_AVI_stationCount = 0;//AVI的工站数量
        public static bool yeild_check_station = false;//是否分析各个工站的良率
        internal static DateTime gl_check_checkDate = DateTime.Now;//分析良率的日期
        public static List<string> yeild_check_AVI1_result = new List<string>();
        public static List<string> yeild_check_AVI2_result = new List<string>();
        public static List<string> yeild_check_AVI3_result = new List<string>();

    }
}
