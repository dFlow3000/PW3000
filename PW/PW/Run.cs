using System;
using System.Collections.Generic;
using System.Text;
using Nocksoft.IO.ConfigFiles;
using System.Reflection;

namespace PW
{
    class Run
    {
        public int runId;
        public int tableCnt;
        public bool completeState;
        public Run(int i_id = 0, bool addNewOne = false)
        {
            if (addNewOne)
            {
                INIFile tnIni = new INIFile(Tournament.iniPath);
                INIFile tableIni = new INIFile(Table.iniPath);
                string strId = Convert.ToString(i_id);
                tnIni.SetValue(Tournament.runSec + strId, Tournament.rS_runId, strId);
                tnIni.SetValue(Tournament.runSec + strId, Tournament.rS_tableCnt, Convert.ToString(Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt)) / 2));
                tableIni.SetValue(Const.fileSec, Table.fsX_tableCnt, Convert.ToString(Convert.ToInt32(tnIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt)) / 2));
                tnIni.SetValue(Tournament.runSec + strId, Tournament.rS_runComplete, Tournament.rS_runComplete_def);
                tnIni.SetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct, strId);
                Getter(Convert.ToInt32(strId));
                Log.Create("Run " + Convert.ToString(runId) + " with " + Convert.ToString(tableCnt) + " table's");
            }
        }

        #region Setter & Getter -------------------------------------------------------------------------------
        #region Setter -------------------------------------------------------------------------------
        public void SetCompleteState(int i_runId, bool i_state)
        {
            INIFile tnIni = new INIFile(Tournament.iniPath);
            Const.SetIniTimeStamp(tnIni);
            if (Const.CheckIdInRange(tnIni, Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt, i_runId))
            {
                string strId = Convert.ToString(i_runId);
                tnIni.SetValue(Tournament.runSec + strId, Tournament.rS_runComplete, Convert.ToString(i_state));
                Log.Info("Run " + Convert.ToString(i_runId) + " completed");
            } else
            {
                Log.Error("Run-SetCompleteState input Id " + i_runId + " out of Range!");
            }
        }
        #endregion
        #region Getter -------------------------------------------------------------------------------
        public void Getter (int i_id)
        {
            INIFile tnIni = new INIFile(Tournament.iniPath);
            if (i_id != 0)
            {
                if (Const.CheckIdInRange(tnIni, Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt, i_id))
                {
                    runId = Convert.ToInt32(tnIni.GetValue(Tournament.runSec + i_id, Tournament.rS_runId));
                    tableCnt = Convert.ToInt32(tnIni.GetValue(Tournament.runSec + runId, Tournament.rS_tableCnt));
                    completeState = Convert.ToBoolean(tnIni.GetValue(Tournament.runSec + runId, Tournament.rS_runComplete));
                }
                else
                {
                    Log.Error("Run-Getter input Id " + i_id + " out of Range!");
                }
            } else
            {
                completeState = true;
            }
        }
        #endregion
        #endregion

    }
}
