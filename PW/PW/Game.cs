using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using System.Reflection;


namespace Preiswattera_3000
{
    class Game : Const
    {
        #region Game Const -----------------------------
        #region ini File
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "GameData.ini");
        // .ini-File - Section - Extras for File Section
        public const string fsX_gameCnt = "Game-Count";
        public const string fsX_gameCnt_def = "0";
        // .ini-File - Section - game Section
        public const string gameSec = "game";
        public const string gS_gameId = "Id";
        public const string gS_gameTeam1 = "Team-Id-1";
        public const string gS_gameTeam2 = "Team-Id-2";
        public const string gS_gamePointsTeam1 = "Team-1-Punkte";
        public const string gS_gamePointsTeam2 = "Team-2-Punkte";
        public const string gS_dpndRun = "Durchlauf-Id";
        #endregion
        #endregion


        public int gameId;
        public int dpndRun;
        public int[] gameTeams = new int[2];
        public int[] gamePoints = new int[2];

        public Game(bool addNewOne = false)
        {
            if (addNewOne)
            {
                INIFile gIni = new INIFile(iniPath);
                gameId = Convert.ToInt32(gIni.GetValue(Const.fileSec, fsX_gameCnt)) + 1;
                gIni.SetValue(fileSec, fsX_gameCnt, Convert.ToString(gameId));
                Log.Create("Game " + Convert.ToString(gameId));
            }
        }

        #region Setter & Getter -------------------------------------------------------------------------------
        #region Setter -------------------------------------------------------------------------------
        public void Setter ()
        {
            INIFile gIni = new INIFile(iniPath);
            SetIniTimeStamp(gIni);
            string strId = Convert.ToString(gameId);
            gIni.SetValue(gameSec + strId, gS_gameId, Convert.ToString(gameId));
            gIni.SetValue(gameSec + strId, gS_gameTeam1, Convert.ToString(gameTeams[0]));
            gIni.SetValue(gameSec + strId, gS_gameTeam2, Convert.ToString(gameTeams[1]));
            gIni.SetValue(gameSec + strId, gS_gamePointsTeam1, Convert.ToString(gamePoints[0]));
            gIni.SetValue(gameSec + strId, gS_gamePointsTeam2, Convert.ToString(gamePoints[1]));
            gIni.SetValue(gameSec + strId, gS_dpndRun, Convert.ToString(dpndRun));
            if (Convert.ToInt32(gIni.GetValue(fileSec, fsX_gameCnt)) < gameId)
            {
                gIni.SetValue(fileSec, fsX_gameCnt, Convert.ToString(Convert.ToInt32(gIni.GetValue(fileSec, fsX_gameCnt)) + 1));
            }
        }
        #endregion
        #region Getter -------------------------------------------------------------------------------
        public void Getter(int i_id)
        {
            INIFile gIni = new INIFile(iniPath);
            if (Const.CheckIdInRange(gIni, Const.fileSec, fsX_gameCnt, i_id))
            {
                string strId = Convert.ToString(i_id);
                gameId = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_gameId));
                gameTeams[0] = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_gameTeam1));
                gameTeams[1] = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_gameTeam2));
                gamePoints[0] = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_gamePointsTeam1));
                gamePoints[1] = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_gamePointsTeam2));
                dpndRun = Convert.ToInt32(gIni.GetValue(gameSec + strId, gS_dpndRun));
            } else
            {
                Log.Error("Game-Getter input Id " + i_id + " out of Range!");
            }
        }
        #endregion
        #endregion

        public static Game[] FillTableWithGames (int i_team1Id, int i_team2Id)
        {
            INIFile tnIni = new INIFile(Tournament.iniPath);
            int maxGamesPerRun = Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            Game[] gamesInRun = new Game[maxGamesPerRun];
            for (int i = 0; i < maxGamesPerRun; i++)
            {
                gamesInRun[i] = new Game();
                gamesInRun[i].gameId = GetActGameCnt() + i + 1;
                gamesInRun[i].gameTeams[0] = i_team1Id;
                gamesInRun[i].gameTeams[1] = i_team2Id;
            }

            return gamesInRun;
        }

        private static int GetActGameCnt ()
        {
            INIFile gIni = new INIFile(iniPath);
            return Convert.ToInt32(gIni.GetValue(fileSec, fsX_gameCnt));
        }
    }

}
