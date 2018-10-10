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

namespace PW
{
    /// <summary>
    /// Interaktionslogik für SignUpTeams.xaml
    /// </summary>
    public partial class SignUpTeams : UserControl
    {
        private MainWindow mnwd;
        SignedUpTeam newTeam;

        public SignUpTeams(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
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
            UserControl prepMenue = new PrepaireMenue(mnwd);
            mnwd.MainContent.Content = prepMenue;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                newTeam.suTeamPlayerFirstNames[0] = tbx_iSUTP1Firstname.Text;
                newTeam.suTeamPlayerLastNames[0] = tbx_iSUTP1Lastname.Text;
                newTeam.suTeamPlayerFirstNames[1] = tbx_iSUTP2Firstname.Text;
                newTeam.suTeamPlayerLastNames[1] = tbx_iSUTP2Lastname.Text;

                newTeam.Setter();
                ClearTbx();
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

    }
}
