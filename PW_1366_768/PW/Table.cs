using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nocksoft.IO.ConfigFiles;

namespace Preiswattera_3000
{
    class Table : Const
    {
        #region Table Const -----------------------------
        #region ini File
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "TableData.ini");
        // .ini-File - Section - Extras for File Section
        public const string fsX_tableCnt = "Anzahl-Tische";
        public const string fsX_tableCnt_def = "0";
        public const string fsX_tableSecCnt = "Anzahl-Tisch-Sektionen";
        public const string fsX_tableSecCnt_def = "0";
        // .ini-File - Section - table Section
        public const string tableSec = "table";
        public const string taS_id = "Id";
        public const string taS_runId = "Durchgang-Nr";
        public const string taS_gameId = "Spiel-";
        public const string taS_gameId_def = "0";
        public const string taS_team1OnTable = "Team1";
        public const string taS_team2OnTable = "Team2";
        public const string taS_team1WinPoints = "Team1-erzielte-GP";
        public const string taS_team2WinPoints = "Team2-erzielte-GP";
        public const string tas_teamsOntable_def = "0";
        public const string tas_winPoints_def = "0";
        #endregion
        #endregion

        public int tableId;
        public int runId;
        public int[] tableGameIds;
        public int[] teamsOnTable = new int[2];
        public int[] winPointsAtGame = new int[2];

        public Table()
        {
            INIFile tnIni = new INIFile(Tournament.iniPath);
            int maxGamePerRound = Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            tableGameIds = new int[maxGamePerRound];
        }

        public Table(int i_id = 0, int i_runId = 0)
        {

            INIFile tnIni = new INIFile(Tournament.iniPath);
            int maxGamePerRound = Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            if (TableExists(i_id, i_runId) == 0)
            {
                INIFile tbIni = new INIFile(iniPath);
                string tbSecCnt = Convert.ToString(Convert.ToInt32(tbIni.GetValue(fileSec, fsX_tableSecCnt)) + 1);
                tbIni.SetValue(fileSec, fsX_tableSecCnt, tbSecCnt);
                tbIni.SetValue(tableSec + tbSecCnt, taS_id, Convert.ToString(i_id));
                tbIni.SetValue(tableSec + tbSecCnt, taS_runId, Convert.ToString(i_runId));
                for (int i = 1; i <= maxGamePerRound; i++)
                {
                    tbIni.SetValue(tableSec + tbSecCnt, taS_gameId + Convert.ToString(i), taS_gameId_def);
                }
                tableGameIds = new int[maxGamePerRound];
                tbIni.SetValue(tableSec + tbSecCnt, taS_team1OnTable, tas_teamsOntable_def);
                tbIni.SetValue(tableSec + tbSecCnt, taS_team2OnTable, tas_teamsOntable_def);
                tbIni.SetValue(tableSec + tbSecCnt, taS_team1WinPoints, tas_winPoints_def);
                tbIni.SetValue(tableSec + tbSecCnt, taS_team2WinPoints, tas_winPoints_def);
                Getter(i_id, i_runId);
            } else if (i_id != 0 && i_runId != 0)
            {
                Getter(i_id, i_runId);
            }
        }

        #region Setter & Getter ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region Setter +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void Setter()
        {
            INIFile tbIni = new INIFile(iniPath);
            INIFile tnIni = new INIFile(Tournament.iniPath);
            int tableSecId = TableExists(tableId, runId);
            string strId = Convert.ToString(tableSecId);
            tbIni.SetValue(tableSec + strId, taS_id, Convert.ToString(tableId));
            tbIni.SetValue(tableSec + strId, taS_runId, Convert.ToString(runId));
            tbIni.SetValue(tableSec + strId, taS_team1OnTable, Convert.ToString(teamsOnTable[0]));
            tbIni.SetValue(tableSec + strId, taS_team2OnTable, Convert.ToString(teamsOnTable[1]));
            tbIni.SetValue(tableSec + strId, taS_team1WinPoints, Convert.ToString(winPointsAtGame[0]));
            tbIni.SetValue(tableSec + strId, taS_team2WinPoints, Convert.ToString(winPointsAtGame[1]));
            int maxGamePerRound = Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            for (int i = 0; i < maxGamePerRound; i++)
            {

                tbIni.SetValue(tableSec + strId, taS_gameId + Convert.ToString(i + 1), Convert.ToString(tableGameIds[i]));
            }
        }
        #endregion
        #region Getter +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void Getter(int i_id, int i_runId)
        {
            INIFile tbIni = new INIFile(iniPath);
            INIFile tnIni = new INIFile(Tournament.iniPath);
            int tableSecId = TableExists(i_id, i_runId);
            if (tableSecId != 0)
            {
                string strId = Convert.ToString(tableSecId);
                tableId = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_id));
                runId = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_runId));
                int maxGamePerRound = Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
                tableGameIds = new int[maxGamePerRound];
                for (int i = 0; i < maxGamePerRound; i++)
                {
                    tableGameIds[i] = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_gameId + Convert.ToString(i + 1)));
                }
                teamsOnTable[0] = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_team1OnTable));
                teamsOnTable[1] = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_team2OnTable));
                winPointsAtGame[0] = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_team1WinPoints));
                winPointsAtGame[1] = Convert.ToInt32(tbIni.GetValue(tableSec + strId, taS_team2WinPoints));

            } else
            {
                Log.Error("Table - Getter input Table-Id " + i_id + " / Run-Id " + i_runId + " out of Range!");
            }
        }

        public void GetTableByTeam(int i_teamId, int i_runId)
        {
            INIFile tbIni = new INIFile(iniPath);

            for(int i = 1; i <= Convert.ToInt32(tbIni.GetValue(Const.fileSec, Table.fsX_tableCnt)); i++)
            {
                Table t = new Table();
                t.Getter(i, i_runId);
                if(t.teamsOnTable[0] == i_teamId || t.teamsOnTable[1] == i_teamId)
                {
                    this.tableId = t.tableId;
                    this.runId = t.runId;
                    this.tableGameIds[0] = t.tableGameIds[0];
                    this.tableGameIds[1] = t.tableGameIds[1];
                    this.tableGameIds[2] = t.tableGameIds[2];
                    this.teamsOnTable[0] = t.teamsOnTable[0];
                    this.teamsOnTable[1] = t.teamsOnTable[1];
                    this.winPointsAtGame[0] = t.winPointsAtGame[0];
                    this.winPointsAtGame[1] = t.winPointsAtGame[1];
                    break;
                }
            }


        }

        public static List<Table> PlayedTableGetter(int i_runId)
        {
            INIFile tbIni = new INIFile(iniPath);
            List<Table> retTable = new List<Table>();

            for(int i = 1; i <= Convert.ToInt32(tbIni.GetValue(Const.fileSec, Table.fsX_tableCnt)); i++)
            {
                Table table = new Table();
                table.Getter(i, i_runId);
                if (table.teamsOnTable[0] != 0)
                {
                    retTable.Add(table);
                }
            }

            return retTable;

        }
        #endregion
        #endregion

        #region Utility - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Check if Table already exists in actual Run
        /// </summary>
        /// <param name="i_tableId"></param>
        /// <param name="i_runId"></param>
        /// <returns>0 if not exists | id if exists</returns>
        public static int TableExists(int i_tableId, int i_runId)
        {
            int ret = 0;
            INIFile tbIni = new INIFile(iniPath);
            int maxTable = Convert.ToInt32(tbIni.GetValue(fileSec, fsX_tableSecCnt));

            for(int i = 1; i <= maxTable; i++)
            {
                if (Convert.ToInt32(tbIni.GetValue(tableSec + Convert.ToString(i), taS_id)) == i_tableId &&
                    Convert.ToInt32(tbIni.GetValue(tableSec + Convert.ToString(i), taS_runId)) == i_runId)
                {
                    ret = i;
                }
            }
            return ret;

        }

        /// <summary>
        /// Check if Team is already playing on another table in this run 
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="i_runId"></param>
        /// <returns></returns>
        public static bool TeamTableCheck(int teamId, int i_runId)
        {
            bool ret = true;
            INIFile tbIni = new INIFile(iniPath);
            for(int i = 1; i <= Convert.ToInt32(tbIni.GetValue(fileSec, fsX_tableSecCnt)); i++)
            {
                int tableRunId = Convert.ToInt32(tbIni.GetValue(tableSec + Convert.ToString(i), taS_runId));
                int tableTeam1 = Convert.ToInt32(tbIni.GetValue(tableSec + Convert.ToString(i), taS_team1OnTable));
                int tableTeam2 = Convert.ToInt32(tbIni.GetValue(tableSec + Convert.ToString(i), taS_team2OnTable));
                if (tableRunId == i_runId)
                {
                    if (teamId == tableTeam1 || teamId == tableTeam2)
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion

    }
}
