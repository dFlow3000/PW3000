using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Nocksoft.IO.ConfigFiles;

namespace Preiswattera_3000
{
    class SignedUpTeam
    {
        #region Tournament Const -----------------------------
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "SignUpTeamsData.ini");
        // .ini-File - Section - File Section Extension
        public const string fsX_suTeamCnt = "Anzahl-angemeldete-Teams";
        public const string fsX_suTeamCnt_def = "0";
        // .ini-File - Section - Signed up Team Section
        public const string suTeamSec = "team";
        public const string sutS_suTId = "Id";
        public const string sutS_suTName = "Name";
        public const string sutS_suTPlayer1Firstname = "Spieler1-Vorname";
        public const string sutS_suTPlayer1Lastname = "Spieler1-Nachname";
        public const string sutS_suTPlayer2Firstname = "Spieler2-Vorname";
        public const string sutS_suTPlayer2Lastname = "Spieler2-Nachname";
        // .ini-File - Section - Length of Section!
        public const int suTeamSec_Length = 7;
        #endregion

        public int suTeamId;
        public string suTeamName;
        public string[] suTeamPlayerFirstNames = new string[2];
        public string[] suTeamPlayerLastNames = new string[2];


        public SignedUpTeam(bool addNewOne = false)
        {
            INIFile sutIni = new INIFile(iniPath);
            suTeamId = Convert.ToInt32(sutIni.GetValue(Const.fileSec, fsX_suTeamCnt)) + 1;
        }

        
        #region Setter & Getter ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region Setter +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void Setter(SignedUpTeam i_suTeam)
        {
            INIFile sutIni = new INIFile(iniPath);
            string strId = Convert.ToString(i_suTeam.suTeamId);
            sutIni.SetValue(suTeamSec + strId, sutS_suTId, strId);
            sutIni.SetValue(suTeamSec + strId, sutS_suTName, i_suTeam.suTeamName);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer1Firstname, i_suTeam.suTeamPlayerFirstNames[0]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer1Lastname, i_suTeam.suTeamPlayerLastNames[0]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer2Firstname, i_suTeam.suTeamPlayerFirstNames[1]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer2Lastname, i_suTeam.suTeamPlayerLastNames[1]);
        }

        public void Setter()
        {
            INIFile sutIni = new INIFile(iniPath);
            string strId = Convert.ToString(suTeamId);
            sutIni.SetValue(suTeamSec + strId, sutS_suTId, strId);
            sutIni.SetValue(suTeamSec + strId, sutS_suTName, suTeamName);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer1Firstname, suTeamPlayerFirstNames[0]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer1Lastname, suTeamPlayerLastNames[0]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer2Firstname, suTeamPlayerFirstNames[1]);
            sutIni.SetValue(suTeamSec + strId, sutS_suTPlayer2Lastname, suTeamPlayerLastNames[1]);
            if (suTeamId > Convert.ToInt32(sutIni.GetValue(Const.fileSec, fsX_suTeamCnt)))
            {
                sutIni.SetValue(Const.fileSec, fsX_suTeamCnt, Convert.ToString(suTeamId));
            }
        }
        #endregion
        #region Getter +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void Getter(int i_id)
        {
            INIFile sutIni = new INIFile(iniPath);
            if (Const.CheckIdInRange(sutIni, Const.fileSec, fsX_suTeamCnt, i_id))
            {
                suTeamId = i_id;
                string strId = Convert.ToString(i_id);
                suTeamName = sutIni.GetValue(suTeamSec + strId, sutS_suTName);
                suTeamPlayerFirstNames[0] = sutIni.GetValue(suTeamSec + strId, sutS_suTPlayer1Firstname);
                suTeamPlayerFirstNames[1] = sutIni.GetValue(suTeamSec + strId, sutS_suTPlayer2Firstname);
                suTeamPlayerLastNames[0] = sutIni.GetValue(suTeamSec + strId, sutS_suTPlayer1Lastname);
                suTeamPlayerLastNames[1] = sutIni.GetValue(suTeamSec + strId, sutS_suTPlayer2Lastname);

            }
            else
            {
                Log.Error("Team-Getter input Id " + i_id + " out of Range!");
            }
        }

        #endregion
        #endregion

        #region Delete - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Delete Team-Section from .ini-File
        /// </summary>
        /// <param name="i_deleteTeam"></param>
        public void deleteSignedUpTeam(SignedUpTeam i_deleteTeam)
        {
            INIFile sutIni = new INIFile(iniPath);
            int suTeamCnt = Convert.ToInt32(sutIni.GetValue(Const.fileSec, SignedUpTeam.fsX_suTeamCnt));

            if (suTeamCnt == i_deleteTeam.suTeamId)
            {
                //Last Team in ini-File! No switching needed
                string o_deleteString = "[" + SignedUpTeam.suTeamSec + i_deleteTeam.suTeamId + "]";
                sutIni.DeleteFromIni(iniPath, o_deleteString, suTeamSec_Length);
            } else
            {
                SignedUpTeam switch_Team = new SignedUpTeam();

                for (int i = i_deleteTeam.suTeamId; i < suTeamCnt; i++)
                {
                    switch_Team.Getter(i + 1);
                    switch_Team.suTeamId = i;
                    switch_Team.Setter();
                }


                string o_deletestring = "[" + SignedUpTeam.suTeamSec + Convert.ToInt32(suTeamCnt) + "]";
                sutIni.DeleteFromIni(iniPath, o_deletestring, suTeamSec_Length);
            }

            sutIni.SetValue(Const.fileSec, fsX_suTeamCnt, Convert.ToString(suTeamCnt - 1));
        }
        #endregion
    }
}
