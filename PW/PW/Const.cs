using System;
using System.IO;
using Nocksoft.IO.ConfigFiles;

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
    }
}
