using System;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace PW
{
    class Const
    {
        // 1 - .ini-File Ordner
        public static string iniFileFolder = "iniFiles";
        public static string CurDirPath = Directory.GetCurrentDirectory();
        public static string iniFolderPath = Path.Combine(CurDirPath, iniFileFolder);

        // 2 - .ini-File - Section - standard file section
        public const string fileSec = "file-section";
        public const string fs_InitState = "Initialition-State";
        public const string fs_InitState_def = "1";
        public const string fs_TimeStamp = "Time-Stamp";

        // 3 - Const Functions
        //  -> 3.1 SetIniTimeStamp
        //     Get act Date and Time and set as value at given Section/Key
        public static void SetIniTimeStamp(INIFile i_ini)
        {
            i_ini.SetValue(fileSec, fs_TimeStamp, Convert.ToString(DateTime.Now));
        }

        //  -> 3.2 CheckIdInRange
        //     Check if input Id exists
        public static bool CheckIdInRange(INIFile i_ini, string i_section, string i_key, int i_id)
        {
            bool ret = false;
            string maxId = i_ini.GetValue(i_section, i_key);
            if (i_id > 0 && i_id <= Convert.ToInt32(maxId))
            {
                ret = true;
            }

            return ret;
        }

        // 4 - PDF Table for Evaluation Output
        public const string posHeader = "Platz ";
        public const int posHeaderLength = 6;
        public const string teamNumberHeader = "Team # ";
        public const int teamNumberHeaderLength = 6;
        public const string teamNameHeader = "            Teamname            ";
        public const int teamNameHeaderLength = 32;
        public const string winPointsHeader = "Gewinn ";
        public const int winPointsHeaderLength = 6;
        public const string gamePointsDiffHeader = "Differenz";
        public const int gamePointsDiffHeaderLength = 9;

        // 5 - Color for Backgroundchange

        public struct Red
        {
            public const string colorRed = "Red";
            public static Color red1 = Color.FromRgb(212, 44, 44);
            public static Color red2 = Color.FromRgb(195, 0, 0);
            public static Color red3 = Color.FromRgb(255, 97, 97);
            public static Color redHighlight = Color.FromRgb(255, 0, 0);
        }

        public struct Blue
        {
            public const string colorBlue = "Blue";
            public static Color blue1 = Color.FromRgb(159, 194, 255);
            public static Color blue2 = Color.FromRgb(66, 107, 178);
            public static Color blue3 = Color.FromRgb(38, 83, 137);
            public static Color blueHighlight = Color.FromRgb(0, 243, 255);

        }

        public struct Green
        {
            public const string colorGreen = "Green";
            public static Color green1 = Color.FromRgb(82, 249, 120);
            public static Color green2 = Color.FromRgb(32, 180, 79);
            public static Color green3 = Color.FromRgb(0, 135, 24);
            public static Color greenHighlight = Color.FromRgb(0, 255, 0);

        }
    }
}
