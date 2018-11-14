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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nocksoft.IO.ConfigFiles;
using System.IO;
using System.Threading;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        public string NO_TEAM_ADDING = "NoAdding";
        public string NO_RETURN = "NoReturn";
        public bool keepDeleting = false;
        public Button[] actionMenueButton = new Button[5];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Directory.CreateDirectory(Const.iniFolderPath);

            INIFile logFile = IniFileDataTemplate.CheckIfIniExists(Log.iniPath);
            INIFile playerData = IniFileDataTemplate.CheckIfIniExists(Player.iniPath);
            INIFile teamData = IniFileDataTemplate.CheckIfIniExists(Team.iniPath);
            INIFile tournamentData = IniFileDataTemplate.CheckIfIniExists(Tournament.iniPath);
            INIFile gameData = IniFileDataTemplate.CheckIfIniExists(Game.iniPath);
            INIFile tableData = IniFileDataTemplate.CheckIfIniExists(Table.iniPath);
            INIFile signedUpTeamsData = IniFileDataTemplate.CheckIfIniExists(SignedUpTeam.iniPath);

            actionMenueButton[0] = btn_GoToAddTeam;
            actionMenueButton[1] = btn_GoToEvaluation;
            actionMenueButton[2] = btn_GoToShowTeam;
            actionMenueButton[3] = btn_GoToTnmtData;
            actionMenueButton[4] = btn_GoTournamentMenue;

            Const.SwitchColor(this, actionMenueButton);

            UserControl creOLoaTnmt = new LoadOrCreateTournament(this);
            MainContent.Content = creOLoaTnmt;

            if (!checkIfTournamentIsRuning(tournamentData))
            {
                // No running Tournament! Start New One and get needed Parameters
                //UserControl addTnmt = new AddTournament(this);
                //MainContent.Content = addTnmt;

                Tournament tnmt = new Tournament();
                tnmt.Getter();

                // Set for first run after creating new Tournament
                btn_GoToTnmtData.Style = (Style)Application.Current.Resources["StartTnmtButton"];

                ActionMenue.Visibility = Visibility.Hidden;
                cnvs_PWHeader.Visibility = Visibility.Visible;

            } else if (checkIfTournamentIsInPrepMode(tournamentData))
            {
                UserControl prepMenue = new PrepaireMenue(this);
                MainContent.Content = prepMenue;
                ActionMenue.Visibility = Visibility.Hidden;
                cnvs_PWHeader.Visibility = Visibility.Visible;

            } else
            {
                UserControl main = new Main(this);
                MainContent.Content = main;

                btn_GoToAddTeam.IsEnabled = true;
                btn_GoToShowTeam.IsEnabled = true;
                btn_GoToTnmtData.IsEnabled = true;
                btn_GoToEvaluation.IsEnabled = true;

                ActionMenue.Visibility = Visibility.Visible;
                cnvs_PWHeader.Visibility = Visibility.Hidden;

                if (Convert.ToInt32(tournamentData.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) != 0)
                {
                    btn_GoToAddTeam.Style = (Style)Application.Current.Resources["DisabledButton"];
                    addTeamImg.Opacity = 0.5;
                    btn_GoToAddTeam.Uid = NO_TEAM_ADDING;
                }

                if (Convert.ToInt32(tournamentData.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) == 0)
                {
                    //btn_GoToTnmtData.Content = "";
                    btn_GoToTnmtData.Style = (Style)Application.Current.Resources["StartTnmtButton"];
                } else
                {
                    //btn_GoToTnmtData.Content = "";
                    btn_GoToTnmtData.Style = (Style)Application.Current.Resources["ActionMenueButton_" + tournamentData.GetValue(Const.fileSec, Tournament.fsX_ColorMode)];
                }

            }
        }

        #region Check - Function +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private bool checkIfTournamentIsRuning (INIFile i_ini)
        {
            if (Const.fs_InitState_def  != i_ini.GetValue(Const.fileSec, Const.fs_InitState))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private bool checkIfTournamentIsInPrepMode (INIFile i_ini)
        {
            if (Tournament.fsX_prepMode_def != i_ini.GetValue(Const.fileSec, Tournament.fsX_prepMode))
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool checkAmountOfTeamsEven()
        {
            INIFile teamIni = new INIFile(Team.iniPath);

            if (Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) > 0 && Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkAllPlayersPayed()
        {
            bool OnenotPayed = false;
            INIFile playerIni = new INIFile(Player.iniPath);
            INIFile teamIni = new INIFile(Team.iniPath);

            int playerCnt = Convert.ToInt32(playerIni.GetValue(Const.fileSec, Player.fsX_playerCnt));
            string[] playerNotPayed = new string[playerCnt];

            for (int i = 1; i <= playerCnt; i++)
            {
                Player newPlayer = new Player();
                newPlayer.Getter(i);

                if (!newPlayer.payedStartFee)
                {
                    playerNotPayed[i - 1] = newPlayer.playerId + " " + newPlayer.playerFirstname + " " + newPlayer.playerLastname;
                    OnenotPayed = true;
                }
            }

            if (OnenotPayed)
            {
                string errorMessage = "";
                for (int i = 0; i < playerNotPayed.Length; i++)
                {
                    if (playerNotPayed[i] != null)
                    {
                        errorMessage += playerNotPayed[i] + " || ";
                    }
                }

                //MessageBox.Show("Offene Startgebühr:\n" + errorMessage, "Ausstehnde Zahlung", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBar(ErrorMessage,
                            "Ausstehnde Startgebühren",
                            "Das Turnier kann erst gestartet werden, wenn alle Startgebühren gezahlt wurden!"+
                            "\n" + errorMessage);
                return false;
            }
            else
            {
                return true;
            }


        }

        #endregion

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_GoToEvaluation_Click(object sender, RoutedEventArgs e)
        {
            UserControl evaluation = new Evaluation(this);
            MainContent.Content = evaluation;
        }

        private void btn_GoToTnmtData_Click(object sender, RoutedEventArgs e)
        {
            INIFile teamIni = new INIFile(Team.iniPath);

            if (checkAmountOfTeamsEven())
            {
                if (checkAllPlayersPayed())
                {
                    INIFile tnmtIni = new INIFile(Tournament.iniPath);
                    if (Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) == 0)
                    {
                        MessageBoxResult ret =
                        MessageBox.Show("Sind Sie Sicher, dass Sie alle Teams erfasst haben?" +
                                    "\nNachdem das Tunier gestartet wurde können keine Teams mehr erfasst werden!",
                                    "Tunier wirklich starten",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question
                                    );
                        if (ret == MessageBoxResult.Yes)
                        {
                            UserControl runMenue = new RunMenue(this);
                            MainContent.Content = runMenue;
                            btn_GoToTnmtData.Style = (Style)Application.Current.Resources["ActionMenueButton_" + tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode)];
                            btn_GoToAddTeam.Style = (Style)Application.Current.Resources["DisabledButton"];
                            btn_GoToAddTeam.Uid = NO_TEAM_ADDING;
                        }
                    } else
                    {
                        UserControl runMenue = new RunMenue(this);
                        MainContent.Content = runMenue;
                    }
                }
            } else
            {
                //MessageBox.Show("Derzeit sind nur " + teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt) + " Teams erfasst!" +
                //                "\nEin Tunier ist nur mit einer geraden Anzahl Teams möglich!",
                //                "Ungerade Anzahl Teams erfasst",
                //                MessageBoxButton.OK,
                //                MessageBoxImage.Error);
                MessageBar(ErrorMessage,
                            "Ungerade Anzahl Teams erfasst",
                            "Derzeit sind nur " + teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt) + " Teams erfasst!" +
                            "\nEin Tunier ist nur mit einer geraden Anzahl Teams möglich!");
            }
        }

        private void btn_GoToShowTeam_Click(object sender, RoutedEventArgs e)
        {
            UserControl showTeam = new ShowTeam(this);
            MainContent.Content = showTeam;
        }

        private void btn_GoToAddTeam_Click(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;
            if (thisButton.Uid != NO_TEAM_ADDING)
            {
                UserControl addTeam = new AddTeam(this);
                MainContent.Content = addTeam;
            }
        }

        private void btn_GoTournamentMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(this);
            MainContent.Content = main;
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
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBB3737"));
                    bar_ErroImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case InfoMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF39AEDA"));
                    bar_InfoImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case WarnMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF3F322"));
                    bar_WarnImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                default:break;
            }
            return retVal;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            cnvs_MessageBar.Visibility = Visibility.Hidden;
            bar_AskImg.Visibility = Visibility.Hidden;
            bar_ErroImg.Visibility = Visibility.Hidden;
            bar_InfoImg.Visibility = Visibility.Hidden;
            bar_WarnImg.Visibility = Visibility.Hidden;
            btn_Ok.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Saver / Waiter - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        public void ShowSaver()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
            {
                timer1.Stop();
                Saver.Visibility = Visibility.Hidden;
            }

            Saver.Visibility = Visibility.Visible;
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(TimerEventProcessor);
            timer1.Start();
        }

        public void ShowWaiterTimer()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
            {
                timer1.Stop();
                Waiter.Visibility = Visibility.Hidden;
            }

            Waiter.Visibility = Visibility.Visible;
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(TimerEventProcessor);
            timer1.Start();
        }

        public void ShowWaiter()
        {
            if (Waiter.Visibility == Visibility.Hidden)
            {
                Waiter.Visibility = Visibility.Visible;
            } else
            {
                Waiter.Visibility = Visibility.Hidden;
            }
        }

        #endregion


        #region Titlebar - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void btn_MainWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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

        private void btn_PWHeader_Click(object sender, RoutedEventArgs e)
        {
            Button thisBtn = (Button)sender;
            if (thisBtn.Uid != NO_RETURN)
            {
                UserControl creOLoaTnmt = new LoadOrCreateTournament(this);
                MainContent.Content = creOLoaTnmt;
            }
        }
    }
}
