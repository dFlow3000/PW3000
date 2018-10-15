using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.Reflection;

namespace PW
{
    /// <summary>
    /// Interaktionslogik für ShowTeam.xaml
    /// </summary>
    public partial class ShowTeam : UserControl
    {
        private MainWindow mainWindow;
        private Team selectedTeam;
        private Player player1;
        private Player player2;
        private bool playedGameSelectionChanged = false;
        public ShowTeam(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void ShowTeam_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbxTeamSelect();
            selectedTeam = new Team();
            player1 = new Player();
            player2 = new Player();
            lbl_sNotPayed.Visibility = Visibility.Hidden;
            cnvs_NotPayed1.Visibility = Visibility.Hidden;
            cnvs_NotPayed2.Visibility = Visibility.Hidden;
            CheckBackGroundForNotPayed();

        }

        private void btn_EditTeam_Click(object sender, RoutedEventArgs e)
        {
            if (cmbx_oiTeamShowerSelectTeam.SelectedIndex != -1)
            {
                Tournament tnmt = new Tournament();
                tnmt.Getter();

                FillShowTeam(selectedTeam, player1, player2);
                btn_EditTeam.Visibility = Visibility.Hidden;
                btn_EditTeam_Save.Visibility = Visibility.Visible;
                btn_EditTeam_Clear.Visibility = Visibility.Visible;

                // Show Textbox and Hide Label
                tbx_oTeamName.Visibility = Visibility.Visible;
                tbx_iTSP1Firstname.Visibility = Visibility.Visible;
                tbx_iTSP1Lastname.Visibility = Visibility.Visible;
                tbx_iTSP2Firstname.Visibility = Visibility.Visible;
                tbx_iTSP2Lastname.Visibility = Visibility.Visible;
                if (tnmt.tnmtActRun == 0)
                {
                    cbx_iTAP1Payed.IsEnabled = true;
                    cbx_iTAP2Payed.IsEnabled = true;
                } else
                {
                    cbx_iTAP1Payed.IsEnabled = false;
                    cbx_iTAP2Payed.IsEnabled = false;
                }
                lbl_oTeamName.Visibility = Visibility.Hidden;
                lbl_iTSP1Firstname.Visibility = Visibility.Hidden;
                lbl_iTSP1Lastname.Visibility = Visibility.Hidden;
                lbl_iTSP2Firstname.Visibility = Visibility.Hidden;
                lbl_iTSP2Lastname.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Wählen Sie zuerst ein Team aus um es zu bearbeiten!",
                                "Bearbeiten nicht möglich",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

        }

        private void btn_EditTeam_Save_Click(object sender, RoutedEventArgs e)
        {
            btn_EditTeam_Save.Visibility = Visibility.Hidden;
            btn_EditTeam.Visibility = Visibility.Visible;
            btn_EditTeam_Clear.Visibility = Visibility.Hidden;

            //Hide Textbox and show Label
            tbx_oTeamName.Visibility = Visibility.Hidden;
            tbx_iTSP1Firstname.Visibility = Visibility.Hidden;
            tbx_iTSP1Lastname.Visibility = Visibility.Hidden;
            tbx_iTSP2Firstname.Visibility = Visibility.Hidden;
            tbx_iTSP2Lastname.Visibility = Visibility.Hidden;
            cbx_iTAP1Payed.IsEnabled = false;
            cbx_iTAP2Payed.IsEnabled = false;
            lbl_oTeamName.Visibility = Visibility.Visible;
            lbl_iTSP1Firstname.Visibility = Visibility.Visible;
            lbl_iTSP1Lastname.Visibility = Visibility.Visible;
            lbl_iTSP2Firstname.Visibility = Visibility.Visible;
            lbl_iTSP2Lastname.Visibility = Visibility.Visible;

            bool noSameNames = true;

            if (Convert.ToString(lbl_oTeamName.Content) != tbx_oTeamName.Text)
            {
                INIFile tIni = new INIFile(Team.iniPath);
                for(int i = 1; i <= Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
                {
                    if (tIni.GetValue(Team.teamSec + Convert.ToString(i), Team.tS_teamName) == tbx_oTeamName.Text)
                    {
                        MessageBox.Show("Der Teamname \"" + tbx_oTeamName.Text + "\" ist bereits vergeben!", 
                                        "Teamname bereits vergeben!", 
                                        MessageBoxButton.OK, 
                                        MessageBoxImage.Error);
                        noSameNames = false;
                        break;
                    }
                }
            }

            if(noSameNames)
            {
                player1.playerFirstname = tbx_iTSP1Firstname.Text;
                player1.playerLastname = tbx_iTSP1Lastname.Text;
                player1.payedStartFee = Convert.ToBoolean(cbx_iTAP1Payed.IsChecked);

                player2.playerFirstname = tbx_iTSP2Firstname.Text;
                player2.playerLastname = tbx_iTSP2Lastname.Text;
                player2.payedStartFee = Convert.ToBoolean(cbx_iTAP2Payed.IsChecked);

                player1.Setter();
                player2.Setter();

                selectedTeam.teamName = tbx_oTeamName.Text;

                selectedTeam.Setter();
                ShowTeam_Loaded(sender, e);
                FillShowTeam(selectedTeam, player1, player2);
            } else
            {
                FillShowTeam(selectedTeam, player1, player2);
            }
        }

        private void btn_EditTeam_Clear_Click(object sender, RoutedEventArgs e)
        {

            btn_EditTeam_Clear.Visibility = Visibility.Hidden;
            btn_EditTeam.Visibility = Visibility.Visible;
            btn_EditTeam_Save.Visibility = Visibility.Hidden;

            //Hide Textbox and show Label
            tbx_oTeamName.Visibility = Visibility.Hidden;
            tbx_iTSP1Firstname.Visibility = Visibility.Hidden;
            tbx_iTSP1Lastname.Visibility = Visibility.Hidden;
            tbx_iTSP2Firstname.Visibility = Visibility.Hidden;
            tbx_iTSP2Lastname.Visibility = Visibility.Hidden;
            cbx_iTAP1Payed.IsEnabled = false;
            cbx_iTAP2Payed.IsEnabled = false;
            lbl_oTeamName.Visibility = Visibility.Visible;
            lbl_iTSP1Firstname.Visibility = Visibility.Visible;
            lbl_iTSP1Lastname.Visibility = Visibility.Visible;
            lbl_iTSP2Firstname.Visibility = Visibility.Visible;
            lbl_iTSP2Lastname.Visibility = Visibility.Visible;

            FillShowTeam(selectedTeam, player1, player2);

        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
        }

        private void FillCmbxTeamSelect()
        {
            cmbx_oiTeamShowerSelectTeam.Items.Clear();

            List<string> allTeams = new List<string>();
            INIFile tIni = new INIFile(Team.iniPath);

            for(int i = 1; i <= Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
            {
                allTeams.Add(tIni.GetValue(Team.teamSec + Convert.ToString(i), Team.tS_teamName));
            }

            int x = 0;
            foreach(string teamName in allTeams)
            {
                x++;
                cmbx_oiTeamShowerSelectTeam.Items.Add(x + " - " + teamName);
            }
        }

        private void FillCmbxPlayedGames()
        {
            cmbx_oiTeamPlayedGames.Items.Clear();

            List<string> allPlayedGames = new List<string>();
            INIFile gIni = new INIFile(Game.iniPath);
            INIFile teamIni = new INIFile(Team.iniPath);

            for(int i = 1; i <= Convert.ToInt32(gIni.GetValue(Const.fileSec, Game.fsX_gameCnt)); i++)
            {
                Game playedGame = new Game();
                playedGame.Getter(i);

                if(playedGame.gameTeams[0] == selectedTeam.teamId)
                {
                    allPlayedGames.Add("D:"+ playedGame.dpndRun + "| S:" + playedGame.gameId + " gegen " + teamIni.GetValue(Team.teamSec + Convert.ToString(playedGame.gameTeams[1]), Team.tS_teamName));
                } else if (playedGame.gameTeams[1] == selectedTeam.teamId)
                {
                    allPlayedGames.Add("D:"+playedGame.dpndRun + "| S:" + playedGame.gameId + " gegen " + teamIni.GetValue(Team.teamSec + Convert.ToString(playedGame.gameTeams[0]), Team.tS_teamName));
                }
            }

            foreach(string game in allPlayedGames)
            {
                cmbx_oiTeamPlayedGames.Items.Add(game);
            }
        }

        private void cmbx_oiTeamShowerSelectTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_oiTeamShowerSelectTeam.SelectedIndex != -1)
            {
                selectedTeam.Getter(cmbx_oiTeamShowerSelectTeam.SelectedIndex + 1);
                Log.Info("Team Getter Id " + Convert.ToString(cmbx_oiTeamShowerSelectTeam.SelectedIndex + 1));
                player1.Getter(selectedTeam.teamPlayer[0]);
                player2.Getter(selectedTeam.teamPlayer[1]);
                FillShowTeam(selectedTeam, player1, player2);
                FillCmbxPlayedGames();

                if (!player1.payedStartFee || !player2.payedStartFee)
                {
                    cnvs_NotPayed1.Visibility = Visibility.Visible;
                    cnvs_NotPayed2.Visibility = Visibility.Visible;
                    lbl_sNotPayed.Visibility = Visibility.Visible;
                }
                else
                {
                    cnvs_NotPayed1.Visibility = Visibility.Hidden;
                    cnvs_NotPayed2.Visibility = Visibility.Hidden;
                    lbl_sNotPayed.Visibility = Visibility.Hidden;
                }
            }
        }

        private void FillShowTeam (Team i_team, Player i_player1, Player i_player2)
        {
            gB_sTeamShower.Header = "Team-Nummer " + Convert.ToString(i_team.teamId);
            // Teamname
            lbl_oTeamName.Content = i_team.teamName;
            tbx_oTeamName.Text = i_team.teamName;
            // Player 1
            lbl_iTSP1Firstname.Content = i_player1.playerFirstname;
            tbx_iTSP1Firstname.Text = i_player1.playerFirstname;
            lbl_iTSP1Lastname.Content = i_player1.playerLastname;
            tbx_iTSP1Lastname.Text = i_player1.playerLastname;

            if (i_player1.payedStartFee)
            {
                cbx_iTAP1Payed.IsChecked = true;
            } else
            {
                cbx_iTAP1Payed.IsChecked = false;
            }
            // Player 2
            lbl_iTSP2Firstname.Content = i_player2.playerFirstname;
            tbx_iTSP2Firstname.Text = i_player2.playerFirstname;
            lbl_iTSP2Lastname.Content = i_player2.playerLastname;
            tbx_iTSP2Lastname.Text = i_player2.playerLastname;
            if (i_player2.payedStartFee)
            {
                cbx_iTAP2Payed.IsChecked = true;
            } else
            {
                cbx_iTAP2Payed.IsChecked = false;
            }

            //Team-Info
            lbl_oTeamGamePoints.Content = Convert.ToString(i_team.gamePointsTotal);
            lbl_oTeamWinPoints.Content = Convert.ToString(i_team.winPoints);
            lbl_oTeamGamePointsDiff.Content = Convert.ToString(i_team.gamePointsTotalDiff);

        }

        private void CheckBackGroundForNotPayed()
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            SolidColorBrush brush;
            switch (tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode))
            {
                case Const.Red.colorRed:
                    brush = new SolidColorBrush(Const.Red.redHighlight);
                    break;
                case Const.Blue.colorBlue:
                    brush = new SolidColorBrush(Const.Blue.blueHighlight);
                    break;
                case Const.Green.colorGreen:
                    brush = new SolidColorBrush(Const.Green.greenHighlight);
                    break;
                default:
                    brush = new SolidColorBrush(Const.Red.redHighlight);
                    break;
            }

            cnvs_NotPayed1.Background = brush;
            cnvs_NotPayed2.Background = brush;
        }

        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if (tbx_iTSP1Lastname.Text != "" && tbx_iTSP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iTSP1Lastname.Text + "-" + tbx_iTSP2Lastname.Text;
                selectedTeam.teamName = tbx_oTeamName.Text;
            }
        }

        private void cmbx_oiTeamPlayedGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playedGameSelectionChanged = true;
        }

        private void btn_ShowGameInfo_Click(object sender, RoutedEventArgs e)
        {
            if (playedGameSelectionChanged)
            {
                int runId = 0;
                int gameId = 0;

                string x = cmbx_oiTeamPlayedGames.SelectedValue.ToString().Substring(cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf(':') + 1,
                                                                                                  (cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf('|') - cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf(':') - 1));
                string y = cmbx_oiTeamPlayedGames.SelectedValue.ToString().Substring(cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf('|') + 4,
                                                                                                   cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf('g') - cmbx_oiTeamPlayedGames.SelectedValue.ToString().IndexOf('|') - 5);
                runId = Convert.ToInt32(x);
                gameId = Convert.ToInt32(y);

                Window playedGameInfo = new PlayedGameInfo(runId, gameId);
                playedGameInfo.Show();
            } else
            {
                MessageBox.Show("Wählen Sie zuerst ein Spiel!", "Kein Spiel ausgewählt", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
