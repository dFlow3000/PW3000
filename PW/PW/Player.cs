using System;
using Nocksoft.IO.ConfigFiles;
using System.IO;
using System.Reflection;

namespace PW
{
    class Player : Const
    {
        #region Player Const -----------------------------
        #region ini-File
        // .ini-File - Config
        public static string iniPath = Path.Combine(Const.iniFolderPath, "PlayerData.ini");
        // .ini-File - Section - Extras for File Section
        public const string fsX_playerCnt = "Player-Count";
        public const string fsX_playerCnt_def = "0";
        // .ini-File - Section - player Section
        public const string playerSec = "player";
        public const string pS_playerId = "Id";
        public const string pS_firstname = "Firstname";
        public const string pS_lastname = "Lastname";
        public const string pS_payed = "PayedStartFee";
        public const string pS_payed_def = "false";
        #endregion
        #endregion


        public int playerId;
        public string playerFirstname;
        public string playerLastname;
        public bool payedStartFee;

        public Player(bool addNewOne = false)
        {
            if (addNewOne)
            {
                INIFile pIni = new INIFile(iniPath);
                playerId = Convert.ToInt32(pIni.GetValue(fileSec, fsX_playerCnt)) + 1;
                pIni.SetValue(Const.fileSec, fsX_playerCnt, Convert.ToString(playerId));
            }
        }


        public void Setter(Player i_player)
        {
            INIFile pIni = new INIFile(iniPath);
            Const.SetIniTimeStamp(pIni);
            string strId = Convert.ToString(i_player.playerId);
            pIni.SetValue(playerSec + strId, pS_playerId, strId);
            pIni.SetValue(playerSec + strId, pS_firstname, i_player.playerFirstname);
            pIni.SetValue(playerSec + strId, pS_lastname, i_player.playerLastname);
            pIni.SetValue(playerSec + strId, pS_payed, pS_payed_def);
        }

        public void Setter()
        {
            INIFile pIni = new INIFile(iniPath);
            Const.SetIniTimeStamp(pIni);
            string strId = Convert.ToString(this.playerId);
            pIni.SetValue(playerSec + strId, pS_playerId, strId);
            pIni.SetValue(playerSec + strId, pS_firstname, this.playerFirstname);
            pIni.SetValue(playerSec + strId, pS_lastname, this.playerLastname);
            pIni.SetValue(playerSec + strId, pS_payed, Convert.ToString(this.payedStartFee));
        }

        public void Getter(int i_id)
        {
            INIFile pIni = new INIFile(iniPath);
            if (CheckIdInRange(pIni, fileSec, fsX_playerCnt, i_id))
            {
                playerId = i_id;
                playerFirstname = pIni.GetValue(playerSec + Convert.ToString(i_id), pS_firstname);
                playerLastname = pIni.GetValue(playerSec + Convert.ToString(i_id), pS_lastname);
                payedStartFee = Convert.ToBoolean(pIni.GetValue(playerSec + Convert.ToString(i_id), pS_payed));
            } else
            {
                Log.Error("Player-Getter input Id " + i_id + " out of Range!");
            }
        }

        public void Getter()
        {
            INIFile pIni = new INIFile(iniPath);
            this.playerFirstname = pIni.GetValue(playerSec + Convert.ToString(this.playerId), pS_firstname);
            this.playerLastname = pIni.GetValue(playerSec + Convert.ToString(this.playerId), pS_lastname);
            this.payedStartFee = Convert.ToBoolean(pIni.GetValue(playerSec + Convert.ToString(this.playerId), pS_payed));
        }

        public static void SetBackPlayerCnt()
        {
            INIFile pIni = new INIFile(iniPath);
            int playerCnt = Convert.ToInt32(pIni.GetValue(fileSec, fsX_playerCnt));
            if (playerCnt > 0 )
            {
                pIni.SetValue(fileSec, fsX_playerCnt, Convert.ToString(playerCnt - 1));
            } else
            {
                pIni.SetValue(fileSec, fsX_playerCnt, Convert.ToString(0));
            }
        }
    }
}
