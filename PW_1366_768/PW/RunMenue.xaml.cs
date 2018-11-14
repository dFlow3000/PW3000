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
    /// Interaktionslogik für RunMenue.xaml
    /// </summary>
    public partial class RunMenue : UserControl
    {
        private MainWindow mainWindow;
        public RunMenue(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        public void RunMenue_Loaded (object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            int runCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt));
            int actRun = Convert.ToInt32(tnmtIni.GetValue(Tournament.runSec, Tournament.tnS_tnmtRunCntAct));
            for (int i = 1; i <= runCnt; i++)
            {
                Button btn_Run_Menue = new Button();
                btn_Run_Menue.Uid = Convert.ToString(i);
                btn_Run_Menue.Content = i + ". Durchgang";
                btn_Run_Menue.Height = 75;
                btn_Run_Menue.FontWeight = FontWeights.Bold;
                btn_Run_Menue.FontSize = 24;
                if(i != 1)
                {
                    Run prevRun = new Run();
                    prevRun.Getter(i - 1);
                    if(!prevRun.completeState)
                    {
                        btn_Run_Menue.IsEnabled = false;
                    }
                }
                
                string colorMode = tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode);
                btn_Run_Menue.Style = (Style)Application.Current.Resources["DefaultIAButton_" + colorMode];
                btn_Run_Menue.Click += new RoutedEventHandler(openRun);
                stp_RunMenueBtn.Children.Add(btn_Run_Menue);
            }
        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void openRun(object sender, EventArgs e)
        {
            Button btn_clicked = new Button();
            btn_clicked = (Button)sender;

            int runId = Convert.ToInt32(btn_clicked.Uid);

            UserControl inserRunData = new InsertRunData(mainWindow, runId);
            mainWindow.MainContent.Content = inserRunData;
        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
        }
        #endregion
    }
}
