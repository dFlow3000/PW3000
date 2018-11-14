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

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für TeamSwap.xaml
    /// </summary>
    public partial class TeamSwap : Window
    {
        #region Message-Bar
        #region Message Types
        public const int ErrorMessage = 1000;
        public const int InfoMessage = 2000;
        public const int WarnMessage = 3000;
        public const int AskMessage = 4000;
        #endregion
        public bool isClicked = false;
        public bool selection = false;
        #endregion
        private string team1Btn = "Team1Btn";
        private string team2Btn = "Team2Btn";
        private List<Button> otherTeam = new List<Button>();
        private List<Button> noTeam = new List<Button>();
        private int selectedOldTeam = 0;
        private int selectedNewTeam = 0;
        private Team i_team1 = new Team();
        private Team i_team2 = new Team();
        /// <summary>
        /// <para>
        /// Label teams[,] contains the Label from InsertRunData 
        /// </para>
        /// <para>
        ///     teams[0,0]
        ///     lbl_oTeam1Number
        ///</para>
        ///<para>
        ///     teams[0,1]
        ///     lbl_oTeam2Number
        ///</para>
        ///<para>
        ///     teams[1,0]
        ///     lbl_oTeam1Player1
        ///</para>
        ///<para>
        ///     teams[1,1]
        ///     lbl_oTeam1Player2
        ///</para>
        ///<para>
        ///     teams[2,0]
        ///     lbl_oTeam2Player1
        ///</para>
        ///<para>
        ///     teams[2,1]
        ///     lbl_oTeam2Player2
        ///</para>
        /// </summary>
        private Label[,] o_teamsLbl;
        /// <summary>
        /// <para>
        /// retValues[0] -> switched Team 1 or 2 : 1 | 2
        /// </para>
        /// <para>
        /// retValues[1] -> switched with used or unused Team : 3000 | 4000
        /// </para>
        /// </summary>
        private int[] o_retValues;
        public const int USED_TEAM = 3000;
        public const int UNUSED_TEAM = 4000;
        private bool usedTeamSelected = false;
        private int i_actRun;
        private int i_actTable;
        public TeamSwap(int t1, int t2, int run, int tableId, Label[,] i_teams, int[] i_retValues)
        {
            InitializeComponent();
            i_team1.Getter(t1);
            i_team2.Getter(t2);
            i_actRun = run;
            i_actTable = tableId;
            o_teamsLbl = i_teams;
            o_retValues = i_retValues;
        }

        private void TeamSwap_Loaded(object sender, RoutedEventArgs e)
        {
            Button[] iA_Buttons =
            {
                btn_Close
            };
            Settings.SwitchColorStyleDefaultButton(iA_Buttons);
            Const.SwitchColor(this);

            btn_Switch.IsEnabled = false;
            SwitchImg.Opacity = 0.5;

            FillTeamSwap();
        }

        private void FillTeamSwap()
        {
            INIFile tableIni = new INIFile(Table.iniPath);
            INIFile teamIni = new INIFile(Team.iniPath);
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            int teamCnt = Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt));
            //int actRun = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct));
            int tableSecCnt = Convert.ToInt32(tableIni.GetValue(Const.fileSec, Table.fsX_tableSecCnt));

            btn_actTeam1.Content = i_team1.teamName;
            btn_actTeam2.Content = i_team2.teamName;
            lbl_oActTable.Content = "Tisch " + Convert.ToString(i_actTable);
            
            List<Table> playedTable = Table.PlayedTableGetter(i_actRun);
            List<Team> teams = Team.GetTeamList();

            teams.RemoveAt(teams.FindIndex(x => x.teamId == i_team1.teamId));
            teams.RemoveAt(teams.FindIndex(x => x.teamId == i_team2.teamId));

            foreach (Table table in playedTable)
            {
                if (table.tableId != i_actTable)
                {
                    Button team1Btn = new Button();
                    team1Btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    Team team1 = new Team();
                    team1.Getter(table.teamsOnTable[0]);
                    team1Btn.Content = Convert.ToString(team1.teamId) + " - " + team1.teamName;
                    team1Btn.Uid = Convert.ToString(team1.teamId);
                    team1Btn.Click += new RoutedEventHandler(SelectTeamOther);
                    stp_TeamsOtherTable.Children.Add(team1Btn);
                    otherTeam.Add(team1Btn);
                    teams.RemoveAt(teams.FindIndex(x => x.teamId == team1.teamId));
                    Button team2Btn = new Button();
                    team2Btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    Team team2 = new Team();
                    team2.Getter(table.teamsOnTable[1]);
                    team2Btn.Content = Convert.ToString(team2.teamId) + " - " + team2.teamName;
                    team2Btn.Uid = Convert.ToString(team2.teamId);
                    team2Btn.Click += new RoutedEventHandler(SelectTeamOther);
                    stp_TeamsOtherTable.Children.Add(team2Btn);
                    otherTeam.Add(team2Btn);
                    teams.RemoveAt(teams.FindIndex(x => x.teamId == team2.teamId));
                }
            }

            foreach (Team team in teams)
            {
                Button teamBtn = new Button();
                teamBtn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                teamBtn.Content = team.teamName;
                teamBtn.Uid = Convert.ToString(team.teamId);
                teamBtn.Click += new RoutedEventHandler(SelectTeamNo);
                stp_TeamsNoTable.Children.Add(teamBtn);
                noTeam.Add(teamBtn);

            }

        }

        private void SelectTeamOther(object sender, RoutedEventArgs e)
        {
            btn_Switch.IsEnabled = true;
            SwitchImg.Opacity = 1;
            usedTeamSelected = true;
            ((Button)sender).Style = (Style)Application.Current.Resources["TeamSwapSelectedWhiteButton"];
            foreach(Button btn in otherTeam)
            {
                if (btn.Uid == "selectedOther")
                {
                    btn.Uid = Convert.ToString(selectedNewTeam);
                    btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    break;
                }
            }
            foreach (Button btn in noTeam)
            {
                if (btn.Uid == "selectedNo")
                {
                    btn.Uid = Convert.ToString(selectedNewTeam);
                    btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    break;
                }
            }
            selectedNewTeam = Convert.ToInt32(((Button)sender).Uid);
            ((Button)sender).Uid = "selectedOther";

        }


        private void SelectTeamNo(object sender, RoutedEventArgs e)
        {
            btn_Switch.IsEnabled = true;
            SwitchImg.Opacity = 1;
            usedTeamSelected = false;
            ((Button)sender).Style = (Style)Application.Current.Resources["TeamSwapSelectedWhiteButton"];
            foreach (Button btn in noTeam)
            {
                if (btn.Uid == "selectedNo")
                {
                    btn.Uid = Convert.ToString(selectedNewTeam);
                    btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    break;
                }
            }
            foreach (Button btn in otherTeam)
            {
                if (btn.Uid == "selectedOther")
                {
                    btn.Uid = Convert.ToString(selectedNewTeam);
                    btn.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                    break;
                }
            }
            selectedNewTeam = Convert.ToInt32(((Button)sender).Uid);
            ((Button)sender).Uid = "selectedNo";
        }

        private void selectTeamOnTable(Button i_btn)
        {
            if (i_btn.Uid == team1Btn)
            {
                btn_actTeam2.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                i_btn.Style = (Style)Application.Current.Resources["TeamSwapSelectedBlackButton"];
            } else
            {
                btn_actTeam1.Style = (Style)Application.Current.Resources["TeamSwapDefaultButton"];
                i_btn.Style = (Style)Application.Current.Resources["TeamSwapSelectedBlackButton"];
            }

            cnvs_BlockNoTableTeams.Visibility = Visibility.Hidden;
            cnvs_BlockOtherTableTeams.Visibility = Visibility.Hidden;
        }

        private void btn_actTeam1_Click(object sender, RoutedEventArgs e)
        {
            selectedOldTeam = 1;
            ((Button)sender).Uid = team1Btn;
            selectTeamOnTable((Button) sender);
        }

        private void btn_actTeam2_Click(object sender, RoutedEventArgs e)
        {
            selectedOldTeam = 2;
            ((Button)sender).Uid = team2Btn;
            selectTeamOnTable((Button)sender);
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }


        #region Titlebar - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void btn_MainWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_MainWindowClose_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragDropTitelBar(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        #region Message - Function +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public bool MessageBar(int i_MessageType, string i_MessageHeader, string i_MessageText)
        {
            bool retVal = true;
            cnvs_MessageBar.Visibility = Visibility.Visible;
            lbl_oMessageHeader.Content = i_MessageHeader;
            lbl_oMessageText.Content = i_MessageText;
            switch (i_MessageType)
            {

                case ErrorMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF4D4D"));
                    bar_ErroImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case InfoMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFAFE6FF"));
                    bar_InfoImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case WarnMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFAF"));
                    bar_WarnImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                default: break;
            }
            return retVal;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;
            cnvs_MessageBar.Visibility = Visibility.Hidden;
            btn_Ok.Visibility = Visibility.Hidden;
            if (thisButton.Uid == "InfoMessage")
            {
                Close();
            };
            thisButton.Uid = String.Empty;
        }
        #endregion

        private void btn_Switch_Click(object sender, RoutedEventArgs e)
        {
            //MessageBar(InfoMessage, "Funzt", "Tableteam: " + Convert.ToString(selectedOldTeam) +" | Selected Team: " + Convert.ToString(selectedNewTeam));

            Team newTeam = new Team();
            newTeam.Getter(selectedNewTeam);

            Player[] newPlayers = newTeam.GetterTeamPlayers();

            if (usedTeamSelected)
            {
                o_retValues[1] = USED_TEAM;
            } else
            {
                o_retValues[1] = UNUSED_TEAM;
            }

            if (selectedOldTeam == 1)
            {
                o_retValues[0] = 0;
                o_teamsLbl[0, 0].Content = Convert.ToString(newTeam.teamId);
                o_teamsLbl[1, 0].Content = newPlayers[0].playerFirstname + " " + newPlayers[0].playerLastname;
                o_teamsLbl[1, 1].Content = newPlayers[1].playerFirstname + " " + newPlayers[1].playerLastname;
            } else
            {
                o_retValues[0] = 1;
                o_teamsLbl[0, 1].Content = Convert.ToString(newTeam.teamId);
                o_teamsLbl[2, 0].Content = newPlayers[0].playerFirstname + " " + newPlayers[0].playerLastname;
                o_teamsLbl[2, 1].Content = newPlayers[1].playerFirstname + " " + newPlayers[1].playerLastname;
            }
            this.DialogResult = true;

        }
    }
}
