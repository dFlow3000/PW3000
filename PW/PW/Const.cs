using System;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Preiswattera_3000
{
    class Const
    {
        #region 1 - .ini-File - Folder +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Name of the folder which containts all .ini-Files
        /// </summary>
        public static string iniFileFolder = "iniFiles";
        /// <summary>
        /// Holds current directory 
        /// </summary>
        public static string CurDirPath = Directory.GetCurrentDirectory();
        /// <summary>
        /// Holds path to the actual .ini-File-Folder
        /// </summary>
        public static string iniFolderPath = Path.Combine(CurDirPath, iniFileFolder);
        #endregion

        #region 2 - .ini-File - Section - standard file section ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// main section in every .ini-File
        /// <para>holds all counter and global states</para>
        /// </summary>
        public const string fileSec = "file-section";
        /// <summary>
        /// initialization State
        /// <para>0 -> no virgin anymore</para>
        /// </summary>
        public const string fs_InitState = "Initialition-State";
        public const string fs_InitState_def = "1";
        public const string fs_TimeStamp = "Time-Stamp";
        #endregion

        #region 3 - Const Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //   -> 3.1 SetIniTimeStamp
        /// <summary>
        /// Sets actual date and time in .ini-File
        /// </summary>
        /// <param name="i_ini">.ini-File Path which has been changed</param>
        public static void SetIniTimeStamp(INIFile i_ini)
        {
            i_ini.SetValue(fileSec, fs_TimeStamp, Convert.ToString(DateTime.Now));
        }

        //   -> 3.2 CheckIdInRange
        /// <summary>
        /// Checks if requested Id is in the range of .ini-File
        /// </summary>
        /// <param name="i_ini">.ini-File which contains requested data</param>
        /// <param name="i_section">name of the section which contains the entry-count-key</param>
        /// <param name="i_key">name of the key which containts the entry-count-value</param>
        /// <param name="i_id">Id which should be checked</param>
        /// <returns>true if Id is in Range</returns>
        public static bool CheckIdInRange(INIFile i_ini, string i_section, string i_key, int i_id)
        {
            bool ret = false;
            string maxId = i_ini.GetValue(i_section, i_key);
            if (i_id > 0 && i_id <= Convert.ToInt32(maxId))
            {
                ret = true;
            } else
            {
                Log.Error("Id:" + i_id + " out of Range! Request Para:" + i_ini + "|" + i_section + "|" + i_key);
            }

            return ret;
        }

        //   -> 3.3 SwitchColor
        /// <summary>
        /// Switchs the backgroundcolor of the actual window depending on the selected color-setting
        /// <para>
        /// Special function: Sets Buttonbackground depending on the selected color-setting if i_ChangeBtnBackground true
        /// </para>
        /// </summary>
        /// <param name="i_window">actual window</param>
        /// <param name="i_btn">Button which should be changed</param>
        /// <param name="i_ChangeBtnBackground">if true, just changing Buttonbackground</param>
        public static void SwitchColor(Window i_window, Button i_btn = null, bool i_ChangeBtnBackground = false)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            switch (tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode))
            {
                case Const.Red.colorRed:
                    if (!i_ChangeBtnBackground)
                    {
                        i_window.Background = Settings.BackgroundSetUp(Const.Red.red1, Const.Red.red2, Const.Red.red3);
                    } else
                    {
                        i_btn.Background = Settings.BackgroundSetUp(Const.Red.red1, Const.Red.red2, Const.Red.red3);
                    }
                    break;
                case Const.Blue.colorBlue:
                    if (!i_ChangeBtnBackground)
                    {
                        i_window.Background = Settings.BackgroundSetUp(Const.Blue.blue1, Const.Blue.blue2, Const.Blue.blue3);
                    }
                    else
                    {
                        i_btn.Background = Settings.BackgroundSetUp(Const.Blue.blue1, Const.Blue.blue2, Const.Blue.blue3);
                    }
                    break;
                case Const.Green.colorGreen:
                    if (!i_ChangeBtnBackground)
                    {
                        i_window.Background = Settings.BackgroundSetUp(Const.Green.green1, Const.Green.green2, Const.Green.green3);
                    }
                    else
                    {
                        i_btn.Background = Settings.BackgroundSetUp(Const.Green.green1, Const.Green.green2, Const.Green.green3);
                    }
                    break;
                case Const.Gray.colorGray:
                    if (!i_ChangeBtnBackground)
                    {
                        i_window.Background = Settings.BackgroundSetUp(Const.Gray.gray1, Const.Gray.gray2, Const.Gray.gray3);
                    }
                    else
                    {
                        i_btn.Background = Settings.BackgroundSetUp(Const.Gray.gray1, Const.Gray.gray2, Const.Gray.gray3);
                    }
                    break;
                default: break;
            }
        }

        //   -> 3.4 SwitchFontColor
        /// <summary>
        /// Retruns color for switching font-color depending on actual color-setting 
        /// </summary>
        /// <returns>color fitting to used backgroundcolor</returns>
        public static Color SwitchFontColor()
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            Color retColor;
            switch (tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode))
            {
                case Const.Red.colorRed:
                    retColor = Const.Red.redHighlight;
                    break;
                case Const.Blue.colorBlue:
                    retColor = Const.Blue.blueHighlight;
                    break;
                case Const.Green.colorGreen:
                    retColor = Const.Green.green2;
                    break;
                case Const.Gray.colorGray:
                    retColor = Const.Gray.grayHighlight;
                    break;
                default:
                    retColor = Const.Red.redHighlight;
                    break;
            }

            return retColor;
        }
        #endregion

        #region 4 - PDF Table for Evaluation Output ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
        #endregion

        #region 5 - Color for Backgroundchange +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Color struct for Backgroundcolor-Switch
        /// <para>Red
        /// <para>Holds the three Colorvalues for LinearGradientBrush</para></para>
        /// </summary>
        public struct Red
        {
            public const string colorRed = "Red";
            public static Color red1 = Color.FromRgb(212, 44, 44);
            public static Color red2 = Color.FromRgb(195, 0, 0);
            public static Color red3 = Color.FromRgb(255, 97, 97);
            public static Color redHighlight = Color.FromRgb(255, 0, 0);
        }

        /// <summary>
        /// Color struct for Backgroundcolor-Switch
        /// <para>Blue
        /// <para>Holds the three Colorvalues for LinearGradientBrush</para></para>
        /// </summary>
        public struct Blue
        {
            public const string colorBlue = "Blue";
            public static Color blue1 = Color.FromRgb(159, 194, 255);
            public static Color blue2 = Color.FromRgb(66, 107, 178);
            public static Color blue3 = Color.FromRgb(38, 83, 137);
            public static Color blueHighlight = Color.FromRgb(0, 243, 255);

        }

        /// <summary>
        /// Color struct for Backgroundcolor-Switch
        /// <para>Green
        /// <para>Holds the three Colorvalues for LinearGradientBrush</para></para>
        /// </summary>
        public struct Green
        {
            public const string colorGreen = "Green";
            public static Color green1 = Color.FromRgb(82, 249, 120);
            public static Color green2 = Color.FromRgb(32, 180, 79);
            public static Color green3 = Color.FromRgb(0, 135, 24);
            public static Color greenHighlight = Color.FromRgb(0, 255, 0);

        }

        /// <summary>
        /// Color struct for Backgroundcolor-Switch
        /// <para>Gray
        /// <para>Holds the three Colorvalues for LinearGradientBrush</para></para>
        /// </summary>
        public struct Gray
        {
            public const string colorGray = "Gray";
            public static Color gray1 = Color.FromRgb(226, 226, 226);
            public static Color gray2 = Color.FromRgb(168, 168, 168);
            public static Color gray3 = Color.FromRgb(83, 83, 83);
            public static Color grayHighlight = Color.FromRgb(240, 240, 240);

        }
        #endregion




    }
}
