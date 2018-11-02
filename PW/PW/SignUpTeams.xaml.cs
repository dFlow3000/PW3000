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

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für SignUpTeams.xaml
    /// </summary>
    public partial class SignUpTeams : UserControl
    {
        private MainWindow mainWindow;
        SignedUpTeam newTeam;
        int multiCreate = 0;

        public SignUpTeams(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void SignUpTeams_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tIni = new INIFile(SignedUpTeam.iniPath);

            newTeam = new SignedUpTeam(true);

            gB_sSignUpTeam.Header = "Team-Nummer " + Convert.ToString(newTeam.suTeamId);
        }

        #region Fill - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Create Teamname out of Players Lastname
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if (tbx_iSUTP1Lastname.Text != "" && tbx_iSUTP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iSUTP1Lastname.Text + "-" + tbx_iSUTP2Lastname.Text;
                newTeam.suTeamName = tbx_oTeamName.Text;
            }
        }
        #endregion

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl prepMenue = new PrepaireMenue(mainWindow);
            mainWindow.MainContent.Content = prepMenue;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (multiCreate == 0)
            {
                SaveSignedUpTeam(sender, e);
            }
            else
            {
                SaveSignedUpTeam(sender, e, multiCreate);
            }
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            tbx_iSUTP1Firstname.Text = String.Empty;
            tbx_iSUTP1Lastname.Text = String.Empty;
            tbx_iSUTP2Firstname.Text = String.Empty;
            tbx_iSUTP2Lastname.Text = String.Empty;
        }
        
        private void btn_MultiCreate_Click(object sender, RoutedEventArgs e)
        {
            lbl_sMultiCreate.Visibility = Visibility.Visible;
            tbx_iMultiCreate.Visibility = Visibility.Visible;
            btn_MultiCreate.Visibility = Visibility.Hidden;
            btn_ClsMultiCreate.Visibility = Visibility.Visible;
        }

        private void btn_ClsMultiCreate_Click(object sender, RoutedEventArgs e)
        {
            multiCreate = 0;
            tbx_iMultiCreate.Visibility = Visibility.Hidden;
            lbl_sMultiCreate.Visibility = Visibility.Hidden;
            btn_MultiCreate.Visibility = Visibility.Visible;
            btn_ClsMultiCreate.Visibility = Visibility.Hidden;            
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new SignedUpTeamsInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }

        #endregion

        #region Utility - Functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Fill Textboxes with String.Empty
        /// </summary>
        private void ClearTbx()
        {
            tbx_iSUTP1Firstname.Text = String.Empty;
            tbx_iSUTP1Lastname.Text = String.Empty;
            tbx_iSUTP2Firstname.Text = String.Empty;
            tbx_iSUTP2Lastname.Text = String.Empty;
            tbx_oTeamName.Text = String.Empty;
        }

        /// <summary>
        /// Saves entered Signed Up Team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSignedUpTeam(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                newTeam.suTeamPlayerFirstNames[0] = tbx_iSUTP1Firstname.Text;
                newTeam.suTeamPlayerLastNames[0] = tbx_iSUTP1Lastname.Text;
                newTeam.suTeamPlayerFirstNames[1] = tbx_iSUTP2Firstname.Text;
                newTeam.suTeamPlayerLastNames[1] = tbx_iSUTP2Lastname.Text;
                newTeam.Setter();
                ClearTbx();
                btn_MultiCreate.Visibility = Visibility.Visible;
                tbx_iMultiCreate.Visibility = Visibility.Hidden;
                lbl_sMultiCreate.Visibility = Visibility.Hidden;
                btn_ClsMultiCreate.Visibility = Visibility.Hidden;
                SignUpTeams_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Einige Informationen fehlen!\nBitte vervollständige die Eingabe!",
                                "Fehlende Informationen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                multiCreate = 0;
            }
        }

        /// <summary>
        /// Saves Multiple Teams from entered Data with identifining postfix added to the teamname
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="i_multiCreate"></param>
        private void SaveSignedUpTeam(object sender, RoutedEventArgs e, int i_multiCreate)
        {
            string teamName = "";
            if (CheckInput())
            {

                btn_MultiCreate.Visibility = Visibility.Visible;
                tbx_iMultiCreate.Visibility = Visibility.Hidden;
                lbl_sMultiCreate.Visibility = Visibility.Hidden;
                btn_ClsMultiCreate.Visibility = Visibility.Hidden;
                for (int i = 1; i <= multiCreate; i++)
                {
                    if (i == 1)
                    {
                        newTeam.suTeamPlayerFirstNames[0] = tbx_iSUTP1Firstname.Text;
                        newTeam.suTeamPlayerLastNames[0] = tbx_iSUTP1Lastname.Text;
                        newTeam.suTeamPlayerFirstNames[1] = tbx_iSUTP2Firstname.Text;
                        newTeam.suTeamPlayerLastNames[1] = tbx_iSUTP2Lastname.Text;
                        teamName = newTeam.suTeamName;
                        newTeam.suTeamName = newTeam.suTeamName + Convert.ToString(i);
                        newTeam.Setter();
                    }
                    else
                    {
                        SignedUpTeam multiTeam = new SignedUpTeam(true);
                        for (int member = 0; member < 2; member++)
                        {
                            multiTeam.suTeamPlayerFirstNames[member] = newTeam.suTeamPlayerFirstNames[member];
                            multiTeam.suTeamPlayerLastNames[member] = newTeam.suTeamPlayerLastNames[member];
                        }
                        multiTeam.suTeamName = teamName + Convert.ToString(i);
                        multiTeam.Setter();
                    }
                }
                ClearTbx();
                btn_MultiCreate.Visibility = Visibility.Visible;
                tbx_iMultiCreate.Visibility = Visibility.Hidden;
                lbl_sMultiCreate.Visibility = Visibility.Hidden;
                SignUpTeams_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Einige Informationen fehlen!\nBitte vervollständige die Eingabe!",
                                "Fehlende Informationen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region Check - Functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private bool CheckInput()
        {
            if (tbx_iSUTP1Firstname.Text != String.Empty &&
                tbx_iSUTP1Lastname.Text != String.Empty &&
                tbx_iSUTP2Firstname.Text != String.Empty &&
                tbx_iSUTP2Lastname.Text != String.Empty &&
                tbx_oTeamName.Text != String.Empty)
            {
                if (tbx_iMultiCreate.Visibility == Visibility.Visible)
                {
                    if (tbx_iMultiCreate.Text != String.Empty)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                } else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Textbox - TextChanged +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void tbx_iMultiCreate_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputMultiCreate = 0;
            if (int.TryParse(tbx_iMultiCreate.Text.Trim(), out inputMultiCreate) && inputMultiCreate > 0)
            {
                multiCreate = inputMultiCreate;
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine Zahl größer 0 ein!",
                                "Mehrfach anlegen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                tbx_iMultiCreate.Text = String.Empty;
            }
        }
        #endregion

    }

    public class SignedUpTeamsInfo
    {
        public const string Header = "Team anmelden";
        public const string Para1_Header = "Team-Nummer:";
        public const string Para1_Content1Value = "Zeigt die Nummer des Teams, dass als nächstes angemeldet wird.";
        public const string Para2_Header = "Spieler 1 & Spieler 2:";
        public const string Para2_Content1Key = "Vor- & Nachname";
        public const string Para2_Content1Value = "Erfassen Sie Vor- & Nachnamen der Spieler 1 und 2.";
        public const string Para3_Header = "Mehrfach anlegen";
        public const string Para3_Content1Value = "\"Mehrfach anlegen\" bietet Ihnen die Möglichkeit mehrere \"Platzhalter\"-Teams mit gleichen Spielerinformationen anzulegen." +
                                                    "\nWählen Sie eine Anzahl und Speichern Sie. Es werden entsprechend der Anzahl Teams angemeldet die durch eine fortlaufende Nummer im Teamname zu unterscheiden sind.";

        public SignedUpTeamsInfo(Dictionary<string, string> i_InfoWindowText)
        {
            i_InfoWindowText.Add(Header, InfoStyles.HeaderStyle);
            i_InfoWindowText.Add(Para1_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para1_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para2_Content1Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para3_Content1Value, InfoStyles.ParaContentValue);
        }

    }
}
