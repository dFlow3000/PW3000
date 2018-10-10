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

namespace PW
{
    /// <summary>
    /// Interaktionslogik für AddTeam.xaml
    /// </summary>
    public partial class AddTeam : UserControl
    {
        private MainWindow mnwd;
        private Team newTeam;
        private Player player1;
        private Player player2;
        private SignedUpTeam loadTeam;

        public AddTeam(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
            
        }

        private void AddTeam_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tIni = new INIFile(Team.iniPath);

            newTeam = new Team(true);

            gB_sTeamAdder.Header = "Team-Nummer " + Convert.ToString(newTeam.teamId);

            FillSignedUpTeams();
        }

        private void FillSignedUpTeams()
        {
            cmbx_signedUpTeams.Items.Clear();

            List<string> allsuTeams = new List<string>();
            INIFile sutIni = new INIFile(SignedUpTeam.iniPath);

            for(int i = 1; i <= Convert.ToInt32(sutIni.GetValue(Const.fileSec, SignedUpTeam.fsX_suTeamCnt)); i++)
            {
                allsuTeams.Add(sutIni.GetValue(SignedUpTeam.suTeamSec + Convert.ToString(i), SignedUpTeam.sutS_suTName));
            }

            foreach(string suTeamName in allsuTeams)
            {
                cmbx_signedUpTeams.Items.Add(suTeamName);
            }
        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mnwd);
            mnwd.MainContent.Content = main;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                if (!CheckTeamNameAllreadyExists())
                {
                    if (checkIfSignedUpTeam() == 1)
                    {
                        if (!mnwd.keepDeleting)
                        {
                            if (MessageBox.Show("Team aus Anmeldeliste wird übernommen!" +
                                                "\nSoll Team aus der Anmeldeliste entfernt werden?",
                                               "Angemeldetes Team aufnehmen und aus Anmeldeliste löschen",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                if (MessageBox.Show("Soll bei unverändert übernommenen Teams immer gelöscht werden?" +
                                                    "\nDie Auswahl wird bei einem Neustart des Programms zurückgesetzt!",
                                                     "Angemeldetes Team aufnehmen und aus Anmeldeliste löschen",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    mnwd.keepDeleting = true;
                                    loadTeam.deleteSignedUpTeam(loadTeam);
                                }
                                else
                                {
                                    mnwd.keepDeleting = false;
                                }
                            }
                        }
                        else
                        {
                            loadTeam.deleteSignedUpTeam(loadTeam);
                        }
                    }
                    else if (checkIfSignedUpTeam() == 0)
                    {
                        if (MessageBox.Show("Ausgewähltes Team aus Anmeldetliste unterschiedlich zu aktuellem Team" +
                                            "\nTeam aufnehmen und ausgewähltes Team aus Anmeldeliste löschen?",
                                              "Unterschied Anmeldeliste und aktuellem Team",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            loadTeam.deleteSignedUpTeam(loadTeam);
                        }

                    }
                    player1 = new Player(true);
                    player2 = new Player(true);

                    player1.playerFirstname = tbx_iTAP1Firstname.Text;
                    player1.playerLastname = tbx_iTAP1Lastname.Text;
                    player1.payedStartFee = Convert.ToBoolean(cbx_iTAP1Payed.IsChecked);

                    player2.playerFirstname = tbx_iTAP2Firstname.Text;
                    player2.playerLastname = tbx_iTAP2Lastname.Text;
                    player2.payedStartFee = Convert.ToBoolean(cbx_iTAP2Payed.IsChecked);

                    newTeam.teamName = tbx_oTeamName.Text;
                    newTeam.SaveTeam(player1, player2);
                    AddTeam_Loaded(sender, e);
                    ClearTbx();
                }
            } else
            {
                MessageBox.Show("Einige Informationen fehlen!\nBitte vervollständige die Eingabe!",
                                "Fehlende Informationen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        private int checkIfSignedUpTeam()
        {
            if (cmbx_signedUpTeams.SelectedIndex != -1)
            {
                if (loadTeam.suTeamPlayerFirstNames[0] == tbx_iTAP1Firstname.Text &&
                    loadTeam.suTeamPlayerLastNames[0]  == tbx_iTAP1Lastname.Text  &&
                    loadTeam.suTeamPlayerFirstNames[1] == tbx_iTAP2Firstname.Text && 
                    loadTeam.suTeamPlayerLastNames[1]  == tbx_iTAP2Lastname.Text     )
                {
                    return 1;
                } else
                {
                    return 0;
                }
            } else
            {
                return 2;
            }
        }


        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearTbx();
        }

        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if(tbx_iTAP1Lastname.Text != "" && tbx_iTAP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iTAP1Lastname.Text + "-" + tbx_iTAP2Lastname.Text;
                newTeam.teamName = tbx_oTeamName.Text;
            }
        }

        private bool CheckInput ()
        {
            if (tbx_iTAP1Firstname.Text != String.Empty &&
                tbx_iTAP1Lastname.Text  != String.Empty &&
                tbx_iTAP2Firstname.Text != String.Empty &&
                tbx_iTAP2Lastname.Text  != String.Empty &&
                tbx_oTeamName.Text      != String.Empty    )
            {
                return true;
            } else
            {
                return false;
            }

        }

        private bool CheckTeamNameAllreadyExists ()
        {
            INIFile tIni = new INIFile(Team.iniPath);

            for (int i = 1; i <= Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
            {
                if (tIni.GetValue(Team.teamSec + Convert.ToString(i), Team.tS_teamName) == tbx_oTeamName.Text)
                {
                    MessageBox.Show("Der Teamname \"" + tbx_oTeamName.Text + "\" ist bereits vergeben!",
                                    "Teamname bereits vergeben!",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return true;
                }
            }

            return false;
        }

        private void ClearTbx()
        {
            tbx_iTAP1Firstname.Text = String.Empty;
            tbx_iTAP1Lastname.Text = String.Empty;
            tbx_iTAP2Firstname.Text = String.Empty;
            tbx_iTAP2Lastname.Text = String.Empty;
            tbx_oTeamName.Text = String.Empty;
            cbx_iTAP1Payed.IsChecked = false;
            cbx_iTAP2Payed.IsChecked = false;
        }

        private void cmbx_signedUpTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadTeam = new SignedUpTeam();
            loadTeam.Getter(cmbx_signedUpTeams.SelectedIndex + 1);

            tbx_iTAP1Firstname.Text = loadTeam.suTeamPlayerFirstNames[0];
            tbx_iTAP1Lastname.Text = loadTeam.suTeamPlayerLastNames[0];
            tbx_iTAP2Firstname.Text = loadTeam.suTeamPlayerFirstNames[1];
            tbx_iTAP2Lastname.Text = loadTeam.suTeamPlayerLastNames[1];
        }
    }
}
