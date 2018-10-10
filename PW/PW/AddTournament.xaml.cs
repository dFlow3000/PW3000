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
    /// Interaktionslogik für AddTournament.xaml
    /// </summary>
    public partial class AddTournament : UserControl
    {
        private MainWindow mnwd;
        public AddTournament(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

        private void btn_StartTnmt_Click(object sender, RoutedEventArgs e)
        {
            if (checkTextBoxNotEmpty())
            {
                INIFile tnmtIni = new INIFile(Tournament.iniPath);
                Tournament newTnmt = new Tournament(tbx_TnmtName.Text, Convert.ToInt32(tbx_TnmtRunCnt.Text), Convert.ToInt32(tbx_TnmtGameCnt.Text));

                UserControl prepMenue = new PrepaireMenue(mnwd);
                mnwd.MainContent.Content = prepMenue;
            } else
            {
                MessageBox.Show("Bitte erfassen Sie alle geforderten Daten!",
                                "Nicht alle relevanten Felder wurden befüllt!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private bool checkTextBoxNotEmpty()
        {
            if (tbx_TnmtName.Text != String.Empty &&
                tbx_TnmtRunCnt.Text != String.Empty &&
                tbx_TnmtGameCnt.Text != String.Empty   )
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
