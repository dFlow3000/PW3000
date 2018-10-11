using System;
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
using System.Windows.Shapes;
using Nocksoft.IO.ConfigFiles;

namespace PW
{
    /// <summary>
    /// Interaktionslogik für PlayedGameInfo.xaml
    /// </summary>
    public partial class PlayedGameInfo : Window
    {
        public int pGWd_runId;
        public int pGWd_gameId;

        public PlayedGameInfo(int i_runId, int i_gameId)
        {
            InitializeComponent();
            pGWd_gameId = i_gameId;
            pGWd_runId = i_runId;
        }

        private void PlayedGameInfo_Loaded (object sender, RoutedEventArgs e)
        {
            INIFile teamIni = new INIFile(Team.iniPath);
            INIFile gameIni = new INIFile(Game.iniPath);
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            switch (tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode))
            {
                case Const.Red.colorRed:
                    this.Background = Settings.BackgroundSetUp(Const.Red.red1, Const.Red.red2, Const.Red.red3);
                    break;
                case Const.Blue.colorBlue:
                    this.Background = Settings.BackgroundSetUp(Const.Blue.blue1, Const.Blue.blue2, Const.Blue.blue3);
                    break;
                case Const.Green.colorGreen:
                    this.Background = Settings.BackgroundSetUp(Const.Green.green1, Const.Green.green2, Const.Green.green3);
                    break;
                default: break;
            }

            Game playedGame = new Game();
            playedGame.Getter(pGWd_gameId);

            Team team1 = new Team();
            team1.Getter(playedGame.gameTeams[0]);
            Team team2 = new Team();
            team2.Getter(playedGame.gameTeams[1]);

            lbl_oGameInfo.Content = pGWd_runId + ". Durchgang / " + playedGame.gameId + ". Spiel";

            lbl_oTeam1.Content = team1.teamName;
            lbl_oTeam2.Content = team2.teamName;

            lbl_oGamePointsTeam1.Content = Convert.ToString(playedGame.gamePoints[0]);
            lbl_oGamePointsTeam2.Content = Convert.ToString(playedGame.gamePoints[1]);
        }

        private void btn_ClosePlayedGameInfo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
