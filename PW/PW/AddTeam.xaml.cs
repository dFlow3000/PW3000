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

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für AddTeam.xaml
    /// 
    /// Add Teams to Tournament by requesting First- and Lastname of two Player
    /// 
    /// </summary>
    public partial class AddTeam : UserControl
    {
        private MainWindow mainWindow;
        private Team newTeam;
        private Player player1;
        private Player player2;
        private SignedUpTeam preSignedUpTeam;

        public AddTeam(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }
        
        private void AddTeam_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tIni = new INIFile(Team.iniPath);
            Button[] iA_Buttons =
            {
                btn_Clear,
                btn_MainMenue,
                btn_Save
            };
            Settings.SwitchColorStyleDefaultButton(iA_Buttons);

            newTeam = new Team(true);

            gB_sTeamAdder.Header = "Team-Nummer " + Convert.ToString(newTeam.teamId);

            FillSignedUpTeams();
        }

        #region Fill - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Loads previous signed up teams from .ini-File
        /// and fills cmbx_signedUpTeams
        /// </summary>
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

        /// <summary>
        /// Fills AddTeam-Usercontrol with data from selected previous signed up team 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbx_signedUpTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            preSignedUpTeam = new SignedUpTeam();
            preSignedUpTeam.Getter(cmbx_signedUpTeams.SelectedIndex + 1);

            tbx_iTAP1Firstname.Text = preSignedUpTeam.suTeamPlayerFirstNames[0];
            tbx_iTAP1Lastname.Text = preSignedUpTeam.suTeamPlayerLastNames[0];
            tbx_iTAP2Firstname.Text = preSignedUpTeam.suTeamPlayerFirstNames[1];
            tbx_iTAP2Lastname.Text = preSignedUpTeam.suTeamPlayerLastNames[1];
        }

        /// <summary>
        /// Creates team-name from team-players lastnames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if (tbx_iTAP1Lastname.Text != "" && tbx_iTAP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iTAP1Lastname.Text + "-" + tbx_iTAP2Lastname.Text;
                newTeam.teamName = tbx_oTeamName.Text;
            }
        }
        #endregion

        #region Button - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Switch back to Tournament-Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
        }

        /// <summary>
        /// Saves added team after checking if all inputs are correct
        /// and clears all fields
        /// 
        /// Checks:
        /// -> no empty inputs
        /// -> check if team name already exists
        /// -> check if previous signed up teams should be deleted after they were added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                if (!CheckTeamNameAllreadyExists())
                {
                    if (checkIfSignedUpTeam() == 1)
                    {
                        if (!mainWindow.keepDeleting)
                        {
                            if (MessageBox.Show("Team aus Anmeldeliste wird übernommen!" +
                                                                             "\nSoll Team aus der Anmeldeliste entfernt werden?",
                                                                             "Angemeldetes Team aufnehmen und aus Anmeldeliste löschen",
                                                                             MessageBoxButton.YesNo,
                                                                             MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                Log.Delete("Signed Up Team after adding it: " + preSignedUpTeam.suTeamName);
                                if (MessageBox.Show("Soll bei unverändert übernommenen Teams immer gelöscht werden?" +
                                                    "\nDie Auswahl wird bei einem Neustart des Programms zurückgesetzt!",
                                                     "Angemeldetes Team aufnehmen und aus Anmeldeliste löschen",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    mainWindow.keepDeleting = true;
                                    preSignedUpTeam.deleteSignedUpTeam(preSignedUpTeam);
                                    Log.Delete("Signed Up Team after adding it: " + preSignedUpTeam.suTeamName);
                                    Log.Update("Keep deleting signed up team after adding: true");
                                }
                                else
                                {
                                    mainWindow.keepDeleting = false;
                                }
                            }
                        }
                        else
                        {
                            preSignedUpTeam.deleteSignedUpTeam(preSignedUpTeam);
                            Log.Delete("Signed Up Team after adding it: " + preSignedUpTeam.suTeamName);
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
                            preSignedUpTeam.deleteSignedUpTeam(preSignedUpTeam);
                            Log.Delete("Signed Up Team after adding it: " + preSignedUpTeam.suTeamName);
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
                //MessageBox.Show("Einige Informationen fehlen!\nBitte vervollständige die Eingabe!",
                //                "Fehlende Informationen",
                //                MessageBoxButton.OK,
                //                MessageBoxImage.Exclamation);
                mainWindow.MessageBar(MainWindow.WarnMessage,
                                        "Fehlende Informationen",
                                        "Einige Informationen die zum Anlegen eines Teams benötigt werden fehlen!" +
                                        "\nBitte verfolständigen Sie diese!");
            }
        }
        
        /// <summary>
        /// Clear all inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearTbx();
        }

        /// <summary>
        /// Starts InfoWindow and loades depending Content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new AddTeamInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }

        #endregion

        #region Check - Functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Checks if added team is one of the previous signed up teams
        /// and should this be the case if there has something changed
        /// | return 1 if nothing has changed / return 0 if something has changed
        /// </summary>
        /// <returns></returns>
        private int checkIfSignedUpTeam()
        {
            if (cmbx_signedUpTeams.SelectedIndex != -1)
            {
                if (preSignedUpTeam.suTeamPlayerFirstNames[0] == tbx_iTAP1Firstname.Text &&
                    preSignedUpTeam.suTeamPlayerLastNames[0] == tbx_iTAP1Lastname.Text &&
                    preSignedUpTeam.suTeamPlayerFirstNames[1] == tbx_iTAP2Firstname.Text &&
                    preSignedUpTeam.suTeamPlayerLastNames[1] == tbx_iTAP2Lastname.Text)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// Checks if all input-fields are filled
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if team name is already used by another team
        /// </summary>
        /// <returns></returns>
        private bool CheckTeamNameAllreadyExists ()
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
                                            "Der Teamname \"" + tbx_oTeamName.Text + "\" wurde bereits vergeben!"+
                                            "\nWählen Sie bitte einen anderen Teamnamen!"+
                                            "\nVorschläge: " + tbx_oTeamName.Text + "1, " + tbx_oTeamName.Text + "...");
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Additional Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Sets all inputs-fields back to empty string
        /// </summary>
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

        #endregion

    }

    public class AddTeamInfo
    {
        public const string Header = "Team erfassen";
        public const string Para1_Header = "Team-Nummer:";
        public const string Para1_Content1Value = "Zeigt die Nummer des Teams, dass als nächstes erfasst wird.";
        public const string Para2_Header = "Angemeldete Spiele:";
        public const string Para2_Content1Value = "Wählen Sie Teams aus die Sie während der Vorbereitungsphase erfasst haben, um sie hinzuzufügen.";
        public const string Para3_Header = "Spieler 1 & Spieler 2:";
        public const string Para3_Content1Key = "Vor- & Nachname";
        public const string Para3_Content1Value = "Erfassen Sie Vor- & Nachnamen der Spieler 1 und 2.";
        public const string Para3_Content2Key = "Startgebühr:";
        public const string Para3_Content2Value = "Erfassen Sie die Zahlung der Startgebühr pro Teammitglied. Sie können die Zahlungsbestätigung auch nach der Teamanmeldung erfassen.";
        public const string Para4_Header = "Zusatzinformationen:";
        public const string Para4_Content1Key = "Teamname:";
        public const string Para4_Content1Value = "Der Teamname wird automatisch erstellt (Nachname Spieler 1 - Nachname Spieler 2). Er kann beliebig verändert werden, muss jedoch eindeutig sein.";
        
        public AddTeamInfo(Dictionary<string, string> i_InfoWindowText)
        {
            i_InfoWindowText.Add(Header, InfoStyles.HeaderStyle);
            i_InfoWindowText.Add(Para1_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para1_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para2_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para3_Content1Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Content2Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para3_Content2Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para4_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para4_Content1Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para4_Content1Value, InfoStyles.ParaContentValue);
        }

    }
}
