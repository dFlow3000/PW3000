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
    /// Interaktionslogik für PrepaireMenue.xaml
    /// </summary>
    public partial class PrepaireMenue : UserControl
    {
        private MainWindow mnwd;
        public PrepaireMenue(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

        private void PrepaireMenue_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            lbl_oPrepMenueHeader.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);
        }

        private void btn_SignUpTeams_Click(object sender, RoutedEventArgs e)
        {
            // Open sign up User Control
            UserControl signUpTeam = new SignUpTeams(mnwd);
            mnwd.MainContent.Content = signUpTeam;
        }

        private void btn_StartTournament_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mnwd);
            mnwd.MainContent.Content = main;

            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_prepMode, Convert.ToString(0));

        }

        private void btn_endTournament_Click(object sender, RoutedEventArgs e)
        {
            // switch Folder
        }
    }
}
