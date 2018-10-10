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
        public static string iniPath = Path.Combine(Const.iniFolderPath, "ErrLogData.ini");
        // .ini-File - Section - Extras for File Section
        public const string fsX_logCnt = "Log-Count";
        public const string fsX_logCnt_def = "0";
        public const string fsX_errCnt = "Err-Count";
        public const string fsX_errCnt_def = "0";
        // .ini-File - Section - Log
        public const string logSec = "log-section";
        // .ini-File - Section - Error
        public const string errSec = "err-section";
        #endregion

        #region Log's
        public const string create      = "CREATE - ";
        public const string info        = "INFO   -";
        public const string update      = "UPDATE -";
        #endregion

        #region errors
        public const string error = "ERROR - ";
        #endregion

        #endregion

        public static void CreateLog (string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, logNumber + "->" + Convert.ToString(DateTime.Now), create + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void InfoLog(string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, logNumber + "->" + Convert.ToString(DateTime.Now), info + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void UpdateLog(string i_logContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string logNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_logCnt)) + 1);
            logIni.SetValue(logSec, logNumber + "->" + Convert.ToString(DateTime.Now), update + i_logContent);
            logIni.SetValue(Const.fileSec, fsX_logCnt, logNumber);
        }

        public static void Error(string i_errContent)
        {
            INIFile logIni = new INIFile(iniPath);
            string errNumber = Convert.ToString(Convert.ToInt32(logIni.GetValue(Const.fileSec, fsX_errCnt)) + 1);
            logIni.SetValue(errSec, errNumber + "->" + Convert.ToString(DateTime.Now), error + i_errContent);
            logIni.SetValue(Const.fileSec, fsX_errCnt, errNumber);
        }

    }
}
