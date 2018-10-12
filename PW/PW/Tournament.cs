using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using System.Reflection;


namespace PW
{
    class Tournament
    {
        #region Tournament Const -----------------------------
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "TournamentData.ini");
        // .ini-File - Section - File Section Extension
        public const string fsX_prepMode = "Prepare-Mode";
        public const string fsX_prepMode_def = "1";
        public const string fsX_allKey = "@";
        public const string fsX_allKey_def = "§%,^^^\"#°~/%?$";
        public const string fsX_SpecTnmtPath = "Specific-Tournament-Path";
        public const string fsX_SpecTnmtPath_def = "C:/";
        public const string fsX_ColorMode = "Color-Mode";
        public const string fsX_ColorMode_def = Const.Red.colorRed;
        // .ini-File - Section - Tournament Section
        public const string tnmtSec = "tournament-section";
        public const string tnS_tnmtName = "Name";
        public const string tnS_tnmtTeamCnt = "Anzahl-Teams";
        public const string tnS_tnmtTeamCnt_def = "0";
        public const string tnS_tnmtRunCnt = "Anzahl-Durchgänge";
        public const string tnS_tnmtRunCnt_def = "0";
        public const string tnS_tnmtRunCntAct = "Anzahl-Durchgänge-Aktuell";
        public const string tnS_tnmtRunCntAct_def = "0";
        public const string tnS_tnmtGameProRunCnt = "Anzahl-Spiel-Pro-Durchgang";
        // .ini-File - Section - Run Section
        public const string runSec = "run";
        public const string rS_runId = "Id";
        public const string rS_tableCnt = "Anzahl-Tische";
        public const string rS_runComplete = "Durchgang-Abgeschlossen";
        public const string rS_runComplete_def = "false";


        #endregion
        public string tnmtName;
        public int tnmtTeamCnt;
        public int tnmtRunCnt;
        public int tnmtGameProRunCnt;
        public string tnmtSpecPath;
        public int tnmtActRun;

        public Tournament()
        {
        }

        public Tournament(string i_tnmtName, int i_tnmtRunCnt, int i_tnmtGameProRunCnt)
        {
            INIFile tnIni = new INIFile(iniPath);
            tnIni.SetValue(Const.fileSec, Const.fs_InitState, "0");
            tnIni.SetValue(tnmtSec, tnS_tnmtName, i_tnmtName);
            tnIni.SetValue(tnmtSec, tnS_tnmtTeamCnt, tnS_tnmtTeamCnt_def);
            tnIni.SetValue(tnmtSec, tnS_tnmtRunCnt, Convert.ToString(i_tnmtRunCnt));
            tnIni.SetValue(tnmtSec, tnS_tnmtGameProRunCnt, Convert.ToString(i_tnmtGameProRunCnt));
            tnIni.SetValue(tnmtSec, tnS_tnmtRunCntAct, tnS_tnmtRunCntAct_def);
            Log.Create("Tournament " + i_tnmtName + " RunCnt: " + Convert.ToString(i_tnmtRunCnt) + " GameCnt/Run: " + Convert.ToString(i_tnmtGameProRunCnt));
        }

        public static Run createRun(int i_id)
        {
            Run newRun = new Run(i_id, true);
            return newRun;

        }

        public void Setter()
        {
            INIFile tnIni = new INIFile(iniPath);
            tnIni.SetValue(tnmtSec, tnS_tnmtName, tnmtName);
            tnIni.SetValue(tnmtSec, tnS_tnmtRunCnt, Convert.ToString(tnmtRunCnt));
        }

        public void Getter()
        {
            INIFile tnIni = new INIFile(iniPath);
            tnmtName = tnIni.GetValue(tnmtSec, tnS_tnmtName);
            tnmtTeamCnt = Convert.ToInt32(tnIni.GetValue(tnmtSec, tnS_tnmtTeamCnt));
            tnmtRunCnt = Convert.ToInt32(tnIni.GetValue(tnmtSec, tnS_tnmtRunCnt));
            tnmtGameProRunCnt = Convert.ToInt32(tnIni.GetValue(tnmtSec, tnS_tnmtGameProRunCnt));
            tnmtSpecPath = tnIni.GetValue(Const.fileSec, fsX_SpecTnmtPath);
            tnmtActRun = Convert.ToInt32(tnIni.GetValue(tnmtSec, tnS_tnmtRunCntAct));
        }

    }
}
