using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using System.Reflection;

namespace PW
{
    class IniFileDataTemplate
    {
        public static INIFile CreateIniFile(string i_iniPath) 
        {
            INIFile newIniFile = new INIFile(i_iniPath, true);
            newIniFile.SetValue(Const.fileSec, Const.fs_InitState, Const.fs_InitState_def);
            Const.SetIniTimeStamp(newIniFile);


            // Special Keys for specific ini-Files
            if (i_iniPath == Log.iniPath)
            {
                // For log ini-File
                newIniFile.SetValue(Const.fileSec, Log.fsX_logCnt, Log.fsX_logCnt_def);
            } else if (i_iniPath == Team.iniPath)
            {
                // For team ini-File
                newIniFile.SetValue(Const.fileSec, Team.fsX_teamCnt, Team.fsX_teamCnt_def);
            } else if (i_iniPath == Player.iniPath)
            {
                // For Player ini-File
                newIniFile.SetValue(Const.fileSec, Player.fsX_playerCnt, Player.fsX_playerCnt_def);
            } else if (i_iniPath == Game.iniPath)
            {
                // For Game ini-File
                newIniFile.SetValue(Const.fileSec, Game.fsX_gameCnt, Game.fsX_gameCnt_def);
            } else if (i_iniPath == Table.iniPath)
            {
                // For Table ini-File
                newIniFile.SetValue(Const.fileSec, Table.fsX_tableCnt, Table.fsX_tableCnt_def);
                newIniFile.SetValue(Const.fileSec, Table.fsX_tableSecCnt, Table.fsX_tableSecCnt_def);
            } else if (i_iniPath == Tournament.iniPath)
            {
                // For Tournament ini-File
                newIniFile.SetValue(Const.fileSec, Tournament.fsX_prepMode, Tournament.fsX_prepMode_def);
                newIniFile.SetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath, Tournament.fsX_SpecTnmtPath_def);
                newIniFile.SetValue(Const.fileSec, Tournament.fsX_allKey, Tournament.fsX_allKey_def);
                newIniFile.SetValue(Const.fileSec, Tournament.fsX_ColorMode, Tournament.fsX_ColorMode_def);
            } else if (i_iniPath == SignedUpTeam.iniPath)
            {
                // For SignedUpTeam ini-File
                newIniFile.SetValue(Const.fileSec, SignedUpTeam.fsX_suTeamCnt, SignedUpTeam.fsX_suTeamCnt_def);
            }


            Log.Create(i_iniPath);
            return newIniFile;
        }

        public static INIFile CheckIfIniExists(string i_iniPath)
        {
            INIFile i_File; 
            if (!File.Exists(i_iniPath))
            {
                i_File = IniFileDataTemplate.CreateIniFile(i_iniPath);
            }
            else
            {
                i_File = new INIFile(i_iniPath);
                Log.Info(i_iniPath + "already exists!");
            }

            return i_File;
        }
    }
}
