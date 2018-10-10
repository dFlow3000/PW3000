﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nocksoft.IO.ConfigFiles;

namespace PW
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private MainWindow mnwd;
        public Main(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

        private void Main_Loaded (object sender, RoutedEventArgs e)
        {
            mnwd.ActionMenue.Visibility = Visibility.Visible;
            mnwd.txbl_Logo.Visibility = Visibility.Hidden;

            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            INIFile gIni = new INIFile(Game.iniPath);
            INIFile tableIni = new INIFile(Table.iniPath);

            int teamCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt));
            int actRun = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct));
            int runCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt));
            int gamePerRunCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            int overAllGameCnt = teamCnt / 2 * gamePerRunCnt;
            int actGameCnt = 0;
            for(int i = 1; i <= Convert.ToInt32(gIni.GetValue(Const.fileSec, Game.fsX_gameCnt)); i++)
            {
                if (Convert.ToInt32(gIni.GetValue(Game.gameSec + Convert.ToString(i), Game.gS_dpndRun)) == actRun)
                {
                    actGameCnt++;
                }
            }
            if (actGameCnt == overAllGameCnt && actRun != 0)
            {
                tnmtIni.SetValue(Tournament.runSec + Convert.ToString(actRun), Tournament.rS_runComplete, Convert.ToString(true));
            }
            // Fill Main 
            // Header Label
            lbl_sMainHeaderTnmtName.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);
            // Actual Section
            if (actRun == 0)
            {
                lbl_oActRun.Content = "Tunier noch nicht gestartet!";
                lbl_ofinishedGamesAct.Visibility = Visibility.Hidden;
                lbl_sfinishedGamesOverall.Visibility = Visibility.Hidden;
                pgb_ActRun.Visibility = Visibility.Hidden;
                lbl_sPGB_ActRun.Visibility = Visibility.Hidden;
            }
            else
            {
                lbl_oActRun.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct);
                lbl_ofinishedGamesAct.Visibility = Visibility.Visible;
                lbl_sfinishedGamesOverall.Visibility = Visibility.Visible;
                pgb_ActRun.Visibility = Visibility.Visible;
                lbl_sPGB_ActRun.Visibility = Visibility.Visible;

            }
            lbl_ofinishedGamesAct.Content = Convert.ToString(actGameCnt);
            lbl_sfinishedGamesOverall.Content = Convert.ToString(overAllGameCnt);
            double value;
            if (actGameCnt > 0)
            {
                value = actGameCnt * 100 / overAllGameCnt;
            } else
            {
                value = 0; 
            }
            pgb_ActRun.Value = value;
            lbl_sPGB_ActRun.Content = actRun + ". Durchgang";
            if (actRun > 0)
            {
                value = actRun * 100 / runCnt;
            } else
            {
                value = 0;
            }
            pgb_Tournament.Value = value;
            lbl_sPGB_Tournament.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);
            // Overall Section
            lbl_oRunCnt.Content = Convert.ToString(runCnt);
            lbl_oGamePerRunCnt.Content = Convert.ToString(gamePerRunCnt);
            lbl_oTeamCnt.Content = Convert.ToString(teamCnt);
            lbl_oTableCnt.Content = tableIni.GetValue(Const.fileSec, Table.fsX_tableCnt);
    


        }
    }
}
