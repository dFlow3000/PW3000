using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;


namespace PW
{
    class Team : Const
    {
        #region Team Const -----------------------------
        #region ini-File
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "TeamData.ini");
        // .ini-File - Section - Extras for File Section
        public const string fsX_teamCnt = "Team-Count";
        public const string fsX_teamCnt_def = "0";
        // .ini-File - Section - team Section
        public const string teamSec = "team";
        public const string tS_teamId = "Id";
        public const string tS_teamName = "Team-Name";
        public const string tS_player1Id = "Player1-Id";
        public const string tS_player2Id = "Player2-Id";
        public const string tS_winPoints = "Gewinn-Punkte";
        public const string tS_gamePointsTotal = "Spiel-Punkte-Gesamt";
        public const string tS_Points_def = "0";
        #endregion
        #endregion

        public int teamId;
        public int[] teamPlayer = new int[2];
        public string teamName;
        public int winPoints;
        public int gamePointsTotal;

        public Team(bool addNewOne = false)
        {
            if (addNewOne)
            {
                INIFile tIni = new INIFile(iniPath);
                teamId = Convert.ToInt32(tIni.GetValue(Const.fileSec, fsX_teamCnt)) + 1;
                //SetValue(Const.fileSec, fsX_teamCnt, Convert.ToString(teamId));
            }
        }
        #region Setter & Getter --------------------------------------------------------------------------------
        #region Setter --------------------------------------------------------------------------------
        public void Setter(Team i_team)
        {
            INIFile tIni = new INIFile(iniPath);
            string strId = Convert.ToString(i_team.teamId);
            tIni.SetValue(teamSec + strId, tS_teamId, strId);
            tIni.SetValue(teamSec + strId, tS_teamName, i_team.teamName);
            tIni.SetValue(teamSec + strId, tS_player1Id, Convert.ToString(i_team.teamPlayer[0]));
            tIni.SetValue(teamSec + strId, tS_player2Id, Convert.ToString(i_team.teamPlayer[1]));
            tIni.SetValue(teamSec + strId, tS_winPoints, Convert.ToString(i_team.winPoints));
            tIni.SetValue(teamSec + strId, tS_gamePointsTotal, Convert.ToString(i_team.gamePointsTotal));


        }

        public void Setter()
        {
            INIFile tIni = new INIFile(iniPath);
            string strId = Convert.ToString(teamId);
            tIni.SetValue(teamSec + strId, tS_teamId, strId);
            tIni.SetValue(teamSec + strId, tS_teamName, teamName);
            tIni.SetValue(teamSec + strId, tS_player1Id, Convert.ToString(teamPlayer[0]));
            tIni.SetValue(teamSec + strId, tS_player2Id, Convert.ToString(teamPlayer[1]));
            tIni.SetValue(teamSec + strId, tS_winPoints, Convert.ToString(winPoints));
            tIni.SetValue(teamSec + strId, tS_gamePointsTotal, Convert.ToString(gamePointsTotal));
            if (teamId > Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)))
            {
                tIni.SetValue(Const.fileSec, fsX_teamCnt, Convert.ToString(teamId));
            }
        }
        #endregion
        #region Getter --------------------------------------------------------------------------------
        public void Getter(int i_id)
        {
            INIFile tIni = new INIFile(iniPath);
            if (Const.CheckIdInRange(tIni, fileSec, fsX_teamCnt, i_id))
            {
                teamId = i_id;
                string strId = Convert.ToString(i_id);
                teamName = tIni.GetValue(teamSec + strId, tS_teamName);
                teamPlayer[0] = Convert.ToInt32(tIni.GetValue(teamSec + strId, tS_player1Id));
                teamPlayer[1] = Convert.ToInt32(tIni.GetValue(teamSec + strId, tS_player2Id));
                winPoints = Convert.ToInt32(tIni.GetValue(teamSec + strId, tS_winPoints));
                gamePointsTotal = Convert.ToInt32(tIni.GetValue(teamSec + strId, tS_gamePointsTotal));
            } else
            {
                Log.Error("Team-Getter input Id " + i_id + " out of Range!");
            }
        }

        #endregion
        #endregion
        public static void SetBackTeamCnt()
        {
            INIFile tIni = new INIFile(iniPath);
            int teamCnt = Convert.ToInt32(tIni.GetValue(fileSec, fsX_teamCnt));
            if (teamCnt > 0)
            {
                tIni.SetValue(fileSec, fsX_teamCnt, Convert.ToString(teamCnt - 1));
            }
            else
            {
                tIni.SetValue(fileSec, fsX_teamCnt, Convert.ToString(0));
            }
        }

        public void SaveTeam(Player p1, Player p2)
        {
            p1.Setter();
            p2.Setter();
            this.teamPlayer[0] = p1.playerId;
            this.teamPlayer[1] = p2.playerId;
            Setter();
            Log.InfoLog(" SAVED - " +"PLAYER " + p1.playerFirstname + " " + p1.playerLastname + " PLAYER " + p2.playerFirstname + " " + p2.playerLastname + " added to TEAM " + teamName);
            INIFile tnIni = new INIFile(Tournament.iniPath);
            INIFile teamIni = new INIFile(Team.iniPath);
            INIFile tableIni = new INIFile(Table.iniPath);
            tnIni.SetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt, Convert.ToString(Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt))));
            if (Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) % 2 == 0 && Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) != 0)
            {
                tableIni.SetValue(Const.fileSec, Table.fsX_tableCnt, Convert.ToString(Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) / 2));
            } else {
                tableIni.SetValue(Const.fileSec, Table.fsX_tableCnt, "/");
            }
        }

        public void DeleteTeam(Player p1, Player p2)
        {
            Player.SetBackPlayerCnt();
            Player.SetBackPlayerCnt();
            SetBackTeamCnt();
            Log.InfoLog(" NOT SAVED - " + "PLAYER " + p1.playerFirstname + " " + p1.playerLastname + " PLAYER " + p2.playerFirstname + " " + p2.playerLastname + " added to TEAM " + teamName + " BUT NOT SAVED");
        }
    }
}
