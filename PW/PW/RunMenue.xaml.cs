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
                btn_Run_Menue.Foreground = Brushes.WhiteSmoke;
                if(i != 1)
                {
                    Run prevRun = new Run();
                    prevRun.Getter(i - 1);
                    if(!prevRun.completeState)
                    {
                        btn_Run_Menue.IsEnabled = false;
                    }
                }

                // Farbverlauf
                LinearGradientBrush lgbrushRed = new LinearGradientBrush();
                lgbrushRed.StartPoint = new Point(0.5, 0);
                lgbrushRed.EndPoint = new Point(0.5, 1);
                lgbrushRed.GradientStops.Add(new GradientStop(Colors.Black, 1.0));
                lgbrushRed.GradientStops.Add(new GradientStop(Colors.Gray, 0.0));

                btn_Run_Menue.Background = lgbrushRed;
                btn_Run_Menue.Click += new RoutedEventHandler(openRun);
                stp_RunMenueBtn.Children.Add(btn_Run_Menue);
            }
        }

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
    }
}
