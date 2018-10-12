using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;

namespace PW
{
    class Log
    {
        #region Log Const -----------------------------
        #region ini-File
        // .ini-File - Config
        public static string iniFileName = "ErrLogData.ini";
        public static string iniPath = Path.Combine(Const.iniFolderPath, iniFileName);
        // .ini-File - Section - Extras for File Section
        public const string fsX_logCnt = "Log-Count";
        public const string fsX_logCnt_def = "0";
        // .ini-File - Section - Log
        public const string logSec = "log-section";
        #endregion

        #region Log's
        public const string create      = "CREATE - ";
        public const string info        = "INFO   - ";
        public const string update      = "UPDATE - ";
        public const string delete      = "DELETE - ";
        public const string save        = "SAVE   - ";
        #endregion

        #region errors
        public const string error       = "ERROR  - ";
        #endregion

        #endregion

        public static void Create (string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(logNumber) + Convert.ToString(DateTime.Now), create + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Info(string i_logContent, bool logSwitch = false, string newIniPath = "")
        {
            INIFile logIni;
            if (logSwitch)
            {
                logIni = new INIFile(newIniPath);
            } else
            {
                logIni = new INIFile(iniPath);
            }
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(logNumber) + Convert.ToString(DateTime.Now), info + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Update(string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(logNumber) + Convert.ToString(DateTime.Now), update + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Delete (string i_logContent, bool logSwitch = false, string newIniPath = "")
        {
            INIFile logIni;
            if (logSwitch)
            {
                logIni = new INIFile(newIniPath);
            }
            else
            {
                logIni = new INIFile(iniPath);
            }
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(logNumber) + Convert.ToString(DateTime.Now), delete + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Save(string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(logNumber) + Convert.ToString(DateTime.Now), save + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Error(string i_errContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string errNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, AddBlankToNumber(errNumber) + Convert.ToString(DateTime.Now), error + i_errContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, errNumber);
        }

        private static string AddBlankToNumber (string i_number)
        {
            string retVal = "";
            for(int i = 0; i < 4 - i_number.Length; i++)
            {
                retVal += "0";                
            }
            retVal += i_number + " >> ";
            return retVal;
        }
    }
}
