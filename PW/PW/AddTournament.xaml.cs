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
    /// Interaktionslogik für AddTournament.xaml
    /// </summary>
    public partial class AddTournament : UserControl
    {
        private MainWindow mainWindow;
        public AddTournament(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        #region Button - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Sets up Tournament through user inputs -> actual tournamtent-state prepairMode 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartTnmt_Click(object sender, RoutedEventArgs e)
        {
            if (checkTextBoxNotEmpty())
            {
                INIFile tnmtIni = new INIFile(Tournament.iniPath);
                Tournament newTnmt = new Tournament(tbx_TnmtName.Text, Convert.ToInt32(tbx_TnmtRunCnt.Text), Convert.ToInt32(tbx_TnmtGameCnt.Text));

                if (tbx_TnmtName.Text != null)
                {
                    string specificTnmntPath = System.IO.Path.Combine(Const.CurDirPath, tbx_TnmtName.Text);
                    Directory.CreateDirectory(specificTnmntPath);
                    tnmtIni.SetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath, specificTnmntPath);
                }

                UserControl prepMenue = new PrepaireMenue(mainWindow);
                mainWindow.MainContent.Content = prepMenue;
            } else
            {
                MessageBox.Show("Bitte erfassen Sie alle geforderten Daten!",
                                "Nicht alle relevanten Felder wurden befüllt!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #region Check - Functions +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Checks if input-fields are not empty
        /// </summary>
        /// <returns>true if all inputs are made</returns>
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
##en
    }
}
