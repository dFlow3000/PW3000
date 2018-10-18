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


        private void CreateTeamName(object sender, RoutedEventArgs e)
        {
            if (tbx_iSUTP1Lastname.Text != "" && tbx_iSUTP2Lastname.Text != "")
            {
                tbx_oTeamName.Text = tbx_iSUTP1Lastname.Text + "-" + tbx_iSUTP2Lastname.Text;
                newTeam.suTeamName = tbx_oTeamName.Text;
            }
        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl prepMenue = new PrepaireMenue(mainWindow);
            mainWindow.MainContent.Content = prepMenue;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (multiCreate > 0)
            {
                for (int i = 1; i <= multiCreate; i++)
                {
                    SaveSignedUpTeam(sender, e, i);
                }
            } else
            {
                SaveSignedUpTeam(sender, e, multiCreate);
            }

            multiCreate = 0;
        }

        private  void ClearTbx()
        {
            tbx_iSUTP1Firstname.Text = String.Empty;
            tbx_iSUTP1Lastname.Text = String.Empty;
            tbx_iSUTP2Firstname.Text = String.Empty;
            tbx_iSUTP2Lastname.Text = String.Empty;
            tbx_oTeamName.Text = String.Empty;
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            tbx_iSUTP1Firstname.Text = String.Empty;
            tbx_iSUTP1Lastname.Text = String.Empty;
            tbx_iSUTP2Firstname.Text = String.Empty;
            tbx_iSUTP2Lastname.Text = String.Empty;
        }

        private bool CheckInput()
        {
            if (tbx_iSUTP1Firstname.Text != String.Empty &&
                tbx_iSUTP1Lastname.Text != String.Empty &&
                tbx_iSUTP2Firstname.Text != String.Empty &&
                tbx_iSUTP2Lastname.Text != String.Empty &&
                tbx_oTeamName.Text != String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveSignedUpTeam(object sender, RoutedEventArgs e, int i_multiCreate = 0)
        {
            if (CheckInput())
            {
                newTeam.suTeamPlayerFirstNames[0] = tbx_iSUTP1Firstname.Text;
                newTeam.suTeamPlayerLastNames[0] = tbx_iSUTP1Lastname.Text;
                newTeam.suTeamPlayerFirstNames[1] = tbx_iSUTP2Firstname.Text;
                newTeam.suTeamPlayerLastNames[1] = tbx_iSUTP2Lastname.Text;
                if (i_multiCreate > 0)
                {
                    newTeam.suTeamName += Convert.ToString(i_multiCreate);
                    newTeam.Setter();
                    if (i_multiCreate == multiCreate)
                    {
                        ClearTbx();
                        btn_MultiCreate.Visibility = Visibility.Visible;
                        tbx_iMultiCreate.Visibility = Visibility.Hidden;
                        lbl_sMultiCreate.Visibility = Visibility.Hidden;
                        SignUpTeams_Loaded(sender, e);
                    }
                }
                else
                {
                    newTeam.Setter();
                    ClearTbx();
                    btn_MultiCreate.Visibility = Visibility.Visible;
                    tbx_iMultiCreate.Visibility = Visibility.Hidden;
                    lbl_sMultiCreate.Visibility = Visibility.Hidden;
                    SignUpTeams_Loaded(sender, e);
                }
   
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

        private void btn_MultiCreate_Click(object sender, RoutedEventArgs e)
        {
            lbl_sMultiCreate.Visibility = Visibility.Visible;
            tbx_iMultiCreate.Visibility = Visibility.Visible;
            btn_MultiCreate.Visibility = Visibility.Hidden;
        }

        private void tbx_iMultiCreate_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputMultiCreate = 0;
            if (int.TryParse(tbx_iMultiCreate.Text.Trim(), out inputMultiCreate))
            {
                multiCreate = inputMultiCreate;
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine Zahl ein!",
                                "Mehrfach anlegen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
