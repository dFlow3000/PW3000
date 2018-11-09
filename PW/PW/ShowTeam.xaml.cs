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

namespace Preiswattera_3000
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

        #region Fill - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void FillCmbxTeamSelect()
        {
            cmbx_oiTeamShowerSelectTeam.Items.Clear();

            List<string> allTeams = new List<string>();
            INIFile tIni = new INIFile(Team.iniPath);

            for (int i = 1; i <= Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
            {
                allTeams.Add(tIni.GetValue(Team.teamSec + Convert.ToString(i), Team.tS_teamName));
            }

            int x = 0;
            foreach (string teamName in allTeams)
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
            int gameCnt = 0;

            cmbx_oiTeamPlayedGames.IsEnabled = false;
            btn_ShowGameInfo.IsEnabled = false;

            for (int i = 1; i <= Convert.ToInt32(gIni.GetValue(Const.fileSec, Game.fsX_gameCnt)); i++)
            {
                Game playedGame = new Game();
                playedGame.Getter(i);

                if (playedGame.gameTeams[0] == selectedTeam.teamId)
                {
                    gameCnt++;
                    allPlayedGames.Add("D:" + playedGame.dpndRun + "| S:" + playedGame.gameId + " gegen " + teamIni.GetValue(Team.teamSec + Convert.ToString(playedGame.gameTeams[1]), Team.tS_teamName));
                }
                else if (playedGame.gameTeams[1] == selectedTeam.teamId)
                {
                    gameCnt++;
                    allPlayedGames.Add("D:" + playedGame.dpndRun + "| S:" + playedGame.gameId + " gegen " + teamIni.GetValue(Team.teamSec + Convert.ToString(playedGame.gameTeams[0]), Team.tS_teamName));
                }
            }

            foreach (string game in allPlayedGames)
            {
                cmbx_oiTeamPlayedGames.Items.Add(game);
            }

            if (gameCnt > 0)
            {
                cmbx_oiTeamPlayedGames.IsEnabled = true;
                btn_ShowGameInfo.IsEnabled = true;
            }
        }

        private void FillShowTeam(Team i_team, Player i_player1, Player i_player2)
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
            }
            else
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
            }
            else
            {
                cbx_iTAP2Payed.IsChecked = false;
            }

            //Team-Info
            lbl_oTeamGamePoints.Content = Convert.ToString(i_team.gamePointsTotal);
            lbl_oTeamWinPoints.Content = Convert.ToString(i_team.winPoints);
            lbl_oTeamGamePointsDiff.Content = Convert.ToString(i_team.gamePointsTotalDiff);

        }
        #endregion

        #region Button - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_EditTeam_Click(object sender, RoutedEventArgs e)
        {
            if (cmbx_oiTeamShowerSelectTeam.SelectedIndex != -1)
            {
                tbx_iTSP1Firstname.Focus();

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
                //MessageBox.Show("Wählen Sie zuerst ein Team aus um es zu bearbeiten!",
                //                "Bearbeiten nicht möglich",
                //                MessageBoxButton.OK,
                //                MessageBoxImage.Error);
                mainWindow.MessageBar(MainWindow.ErrorMessage,
                                        "Bearbeitung nicht möglich",
                                        "Wählen Sie zuerst ein Team aus um es zu bearbeiten!");
            }

        }

        private void btn_EditTeam_Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckEmptyString())
            {
                if (CheckTeamNameLength())
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
                        for (int i = 1; i <= Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
                        {
                            if (tIni.GetValue(Team.teamSec + Convert.ToString(i), Team.tS_teamName) == tbx_oTeamName.Text)
                            {
                                //MessageBox.Show("Der Teamname \"" + tbx_oTeamName.Text + "\" ist bereits vergeben!",
                                //                "Teamname bereits vergeben!",
                                //                MessageBoxButton.OK,
                                //                MessageBoxImage.Error);
                                mainWindow.MessageBar(MainWindow.WarnMessage,
                                                        "Teamname bereits vergeben!",
                                                        "Der Teamname \"" + tbx_oTeamName.Text + "\" wurde bereits vergeben!" +
                                                        "\nWählen Sie bitte einen anderen Teamnamen!" +
                                                        "\nVorschläge: " + tbx_oTeamName.Text + "1, " + tbx_oTeamName.Text + "...");
                                noSameNames = false;
                                break;
                            }
                        }
                    }

                    if (noSameNames)
                    {
                        int log_cnt = 1;
                        Log.Update("EDIT TEAM Log#1 - TeamNr:" + selectedTeam.teamId + "|TeamName:" + selectedTeam.teamName);
                        if (TeamUpdateCheck("Player1-Firstname", player1.playerFirstname, tbx_iTSP1Firstname.Text, ++log_cnt))
                        {
                            player1.playerFirstname = tbx_iTSP1Firstname.Text;
                        }
                        if (TeamUpdateCheck("Player1-Lastname", player1.playerLastname, tbx_iTSP1Lastname.Text, ++log_cnt))
                        {
                            player1.playerLastname = tbx_iTSP1Lastname.Text;
                        }
                        if (TeamUpdateCheck("Player1-StartFee", Convert.ToString(player1.payedStartFee), Convert.ToString(cbx_iTAP1Payed.IsChecked), ++log_cnt))
                        {
                            player1.payedStartFee = Convert.ToBoolean(cbx_iTAP1Payed.IsChecked);
                        }

                        if (TeamUpdateCheck("Player2-Firstname", player2.playerFirstname, tbx_iTSP2Firstname.Text, ++log_cnt))
                        {
                            player2.playerFirstname = tbx_iTSP2Firstname.Text;
                        }
                        if (TeamUpdateCheck("Player2-Lastname", player1.playerLastname, tbx_iTSP2Lastname.Text, ++log_cnt))
                        {
                            player2.playerLastname = tbx_iTSP2Lastname.Text;
                        }
                        if (TeamUpdateCheck("Player2-StartFee", Convert.ToString(player2.payedStartFee), Convert.ToString(cbx_iTAP2Payed.IsChecked), ++log_cnt))
                        {
                            player2.payedStartFee = Convert.ToBoolean(cbx_iTAP2Payed.IsChecked);
                        }
                        player1.Setter();
                        player2.Setter();

                        if (TeamUpdateCheck("TeamName", selectedTeam.teamName, tbx_oTeamName.Text, ++log_cnt))
                        {
                            selectedTeam.teamName = tbx_oTeamName.Text;
                        }
                        selectedTeam.Setter();
                        mainWindow.ShowSaver();
                        ShowTeam_Loaded(sender, e);
                        FillShowTeam(selectedTeam, player1, player2);
                    }
                    else
                    {
                        FillShowTeam(selectedTeam, player1, player2);
                    }
                }
            } else
            {
                mainWindow.MessageBar(MainWindow.WarnMessage,
                                        "Fehlende Informationen",
                                        "Einige Informationen die nach der Bearbeitung eines Teams benötigt werden fehlen!" +
                                        "\nBitte verfolständigen Sie diese!");
            }
        }

        private bool TeamUpdateCheck(string desc, string oldValue, string newValue, int log_Cnt)
        {
            if (oldValue != newValue)
            {
                Log.Update("EDIT TEAM Log#" + Convert.ToString(log_Cnt) + " - " + desc + ":" + oldValue + " -> " + newValue);
                return true;
            } else
            {
                return false;
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

                Window playedGameInfo = new PlayedGameInfo(runId, gameId, cmbx_oiTeamShowerSelectTeam.SelectedIndex+1);
                playedGameInfo.Show();
            }
            else
            {
                //MessageBox.Show("Wählen Sie zuerst ein Spiel!", "Kein Spiel ausgewählt", MessageBoxButton.OK, MessageBoxImage.Warning);
                mainWindow.MessageBar(MainWindow.WarnMessage,
                                        "Kein Spiel ausgewählt",
                                        "Wählen Sie zuerst ein Spiel aus!");
            }
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new ShowTeamInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }
        #endregion

        #region Combobox - SelectionChanged +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void cmbx_oiTeamShowerSelectTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_oiTeamShowerSelectTeam.SelectedIndex != -1)
            {
                btn_EditTeam.IsEnabled = true;
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

        private void cmbx_oiTeamPlayedGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playedGameSelectionChanged = true;
        }

        #endregion

        #region Check - Functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void CheckBackGroundForNotPayed()
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            SolidColorBrush brush;
            switch (tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode))
            {
                case Const.Red.color:
                    brush = new SolidColorBrush(Const.Red.redHighlight);
                    break;
                case Const.Blue.color:
                    brush = new SolidColorBrush(Const.Blue.blueHighlight);
                    break;
                case Const.Green.color:
                    brush = new SolidColorBrush(Const.Green.greenHighlight);
                    break;
                default:
                    brush = new SolidColorBrush(Const.Red.redHighlight);
                    break;
            }

            cnvs_NotPayed1.Background = brush;
            cnvs_NotPayed2.Background = brush;
        }


        private bool CheckTeamNameLength()
        {
            if (tbx_oTeamName.Text.Length > 32)
            {
                mainWindow.MessageBar(MainWindow.ErrorMessage,
                                        "Teamname zu lang",
                                        "Der angegebene Teamname ist zu lang! Kürzen Sie ihn auf max. 30 Zeichen!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckEmptyString()
        {
            if (tbx_iTSP1Firstname.Text != String.Empty &&
                tbx_iTSP1Lastname.Text != String.Empty  &&
                tbx_iTSP2Firstname.Text != String.Empty &&
                tbx_iTSP2Lastname.Text != String.Empty  &&
                tbx_oTeamName.Text != String.Empty          )
            {
                return true;
            } else
            {
                return false;
            }
        }
        #endregion

        #region Utitlity - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if (tbx_iTSP1Lastname.Text != "" && tbx_iTSP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iTSP1Lastname.Text + "-" + tbx_iTSP2Lastname.Text;
                selectedTeam.teamName = tbx_oTeamName.Text;
            }
        }
        #endregion
        
    }

    public class ShowTeamInfo
    {
        public const string Header = "Team anzeigen";
        public const string Para1_Header = "Team auswählen:";
        public const string Para1_Content1Value = "Wählen Sie die das Team, dass Sie anzeigen wollen";
        public const string Para2_Header = "Team-Daten:";
        public const string Para2_Content1Header = "Team-Nummer:";
        public const string Para2_Content1Value = "Zeigt die Team-Nummer des aktuell ausgewählten Teams";
        public const string Para2_Content2Header = "Spieler 1 & Spieler 2:";
        public const string Para2_Content2Value = "Zeigt die Vor- & Nachnamen, sowie die Zahlungstatus der Teammitglieder.";
        public const string Para2_Content3Header = "Bearbeiten:";
        public const string Para2_Content3Value = "Ermöglicht die Bearbeitung der Team-Daten.";
        public const string Para3_Header = "Team-Informationen:";
        public const string Para3_Content1Header = "Gewinn-Punkte:";
        public const string Para3_Content1Value = "Zeigt die Anzahl der erzielten Gewinn-Punkte.";
        public const string Para3_Content2Header = "Spiel-Punkte:";
        public const string Para3_Content2Value = "Zeigt die Anzahl der erzielten Spiel-Punkte.";
        public const string Para3_Content3Header = "Spiel-Punkte-Diff:";
        public const string Para3_Content3Value = "Zeigt die Anzahl der erzielten Spiel-Punkte-Differenz.";
        public const string Para3_Content4Header = "gespielte Spiele:";
        public const string Para3_Content4Value = "Wählen Sie ein Spiel aus der Liste aus und lassen Sie sich durch \"Spiel-Informationen anzeigen\" die zugehörigen Daten anzeigen.";



        public ShowTeamInfo(Dictionary<string, string> i_InfoWindowText)
        {
            i_InfoWindowText.Add(Header, InfoStyles.HeaderStyle);
            i_InfoWindowText.Add(Para1_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para1_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para2_Content1Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Content2Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content2Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Content3Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content3Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para3_Content1Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Content2Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content2Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Content3Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content3Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Content4Header, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content4Value, InfoStyles.ParaContentValue);
        }

    }
}
