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

namespace PW
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush lgbrush_temp = new LinearGradientBrush();
        public bool keepDeleting = false;
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

            if (!checkIfTournamentIsRuning(tournamentData))
            {
                // No running Tournament! Start New One and get needed Parameters
                UserControl addTnmt = new AddTournament(this);
                MainContent.Content = addTnmt;

                ActionMenue.Visibility = Visibility.Hidden;
                txbl_Logo.Visibility = Visibility.Visible;

            } else if (checkIfTournamentIsInPrepMode(tournamentData))
            {
                UserControl prepMenue = new PrepaireMenue(this);
                MainContent.Content = prepMenue;
                ActionMenue.Visibility = Visibility.Hidden;
                txbl_Logo.Visibility = Visibility.Visible;

            } else
            {
                UserControl main = new Main(this);
                MainContent.Content = main;

                btn_GoToAddTeam.IsEnabled = true;
                btn_GoToShowTeam.IsEnabled = true;
                btn_GoToTnmtData.IsEnabled = true;
                btn_GoToEvaluation.IsEnabled = true;

                ActionMenue.Visibility = Visibility.Visible;
                txbl_Logo.Visibility = Visibility.Hidden;

                lgbrush_temp = btn_GoToTnmtData.Background;

                if (Convert.ToInt32(tournamentData.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) == 0) {
                    btn_GoToTnmtData.Content = "Tunier Starten!";
                    // Farbverlauf
                    LinearGradientBrush lgbrush = new LinearGradientBrush();
                    lgbrush.StartPoint = new Point(0.5, 0);
                    lgbrush.EndPoint = new Point(0.5, 1);
                    lgbrush.GradientStops.Add(new GradientStop(Colors.White, 1.0));
                    lgbrush.GradientStops.Add(new GradientStop(Colors.LimeGreen, 0.0));

                    btn_GoToTnmtData.Background = lgbrush;
                } else
                {
                    btn_GoToAddTeam.IsEnabled = false;
                }
            }
        }

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
                            btn_GoToTnmtData.Background = lgbrush_temp;
                            btn_GoToTnmtData.Content = "Tunier Daten erfassen";
                        }
                    } else
                    {
                        UserControl runMenue = new RunMenue(this);
                        MainContent.Content = runMenue;
                        btn_GoToTnmtData.Background = lgbrush_temp;
                        btn_GoToTnmtData.Content = "Tunier Daten erfassen";
                    }
                }
            } else
            {
                MessageBox.Show("Derzeit sind nur " + teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt) + " Teams erfasst!" +
                                "\n Ein Tunier ist nur mit einer geraden Anzahl Teams möglich!" +
                                "\n Erfassen Sie noch mindestens ein Team!",
                                "Ungerade Anzahl Teams erfasst",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void btn_GoToShowTeam_Click(object sender, RoutedEventArgs e)
        {
            UserControl showTeam = new ShowTeam(this);
            MainContent.Content = showTeam;
        }

        private void btn_GoToAddTeam_Click(object sender, RoutedEventArgs e)
        {
            UserControl addTeam = new AddTeam(this);
            MainContent.Content = addTeam;
        }

        private bool checkAmountOfTeamsEven ()
        {
            INIFile teamIni = new INIFile(Team.iniPath);

            if(Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) % 2 == 0)
            {
                return true;
            } else
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

            for(int i = 1; i <= playerCnt; i++)
            {
                Player newPlayer = new Player();
                newPlayer.Getter(i);

                if(!newPlayer.payedStartFee)
                {
                    playerNotPayed[i - 1] = newPlayer.playerId + " | " + newPlayer.playerFirstname + " | " + newPlayer.playerLastname;
                    OnenotPayed = true;
                }
            }

            if(OnenotPayed)
            {
                string errorMessage = "";
                for(int i = 0; i < playerNotPayed.Length; i++)
                {
                    if (playerNotPayed[i] != null)
                    {
                        errorMessage += playerNotPayed[i] + "\n";
                    }
                }

                MessageBox.Show("Offene Startgebühr:\n" + errorMessage, "Ausstehnde Zahlung", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else
            {
                return true;
            }


        }
    }
}
