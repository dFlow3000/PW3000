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
using System.IO;
using Nocksoft.IO.ConfigFiles;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für PrepaireMenue.xaml
    /// </summary>
    public partial class PrepaireMenue : UserControl
    {
        private MainWindow mainWindow;
        public PrepaireMenue(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void PrepaireMenue_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            Button[] iA_Btn =
            {
                btn_SignUpTeams
            };
            Settings.SwitchColorStyleDefaultButton(iA_Btn);

            lbl_oPrepMenueHeader.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);
        }

        private void btn_SignUpTeams_Click(object sender, RoutedEventArgs e)
        {
            // Open sign up User Control
            UserControl signUpTeam = new SignUpTeams(mainWindow);
            mainWindow.MainContent.Content = signUpTeam;
        }

        private void btn_StartTournament_Click(object sender, RoutedEventArgs e)
        {

            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_prepMode, Convert.ToString(0));

            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
        }

        private void btn_endTournament_Click(object sender, RoutedEventArgs e)
        {
            INIFile suIni = new INIFile(SignedUpTeam.iniPath);
            Tournament tnmt = new Tournament();
            tnmt.Getter();

            bool saveSignedUpTeams = false;
            if (Convert.ToInt32(suIni.GetValue(Const.fileSec, SignedUpTeam.fsX_suTeamCnt)) > 0)
            {
                if (MessageBox.Show("Es wurden bereits Teams angemeldet.\nWollen Sie diese Teams speichern um Sie später laden zu können?",
                                "Angemeldete Teams speichern",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    saveSignedUpTeams = true;
                }
            }

            if (saveSignedUpTeams)
            {
                switchToFinishFolder(tnmt);
            }

            Directory.Delete(Const.iniFolderPath);
            mainWindow.Close();
        }

        private void switchToFinishFolder(Tournament tnmt)
        {
            string finishedFolderPath = "";
            try
            {
                finishedFolderPath = System.IO.Path.Combine(tnmt.tnmtSpecPath, "Finished_Tournament_Data");
                Directory.CreateDirectory(finishedFolderPath);
                Log.Create("Directory: " + finishedFolderPath);
            }
            catch
            {
                Log.Error("creating Directory: " + finishedFolderPath);
            }

            string newLogPath = System.IO.Path.Combine(finishedFolderPath, Log.iniFileName);
            bool logSwitched = false;

            foreach (var srcPath in Directory.GetFiles(Const.iniFolderPath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                if (srcPath == Log.iniPath)
                {
                    logSwitched = true;
                }
                File.Copy(srcPath, srcPath.Replace(Const.iniFolderPath, finishedFolderPath), true);
                Log.Info("SWITCH " + srcPath + " TO " + finishedFolderPath, logSwitched, newLogPath);
                File.Delete(srcPath);
                Log.Delete(srcPath + " at Tournament Quit while Switch", logSwitched, newLogPath);
            }

            Directory.Delete(Const.iniFolderPath);
            Log.Delete(Const.iniFileFolder + "at Tournament Quit after Switch", logSwitched, newLogPath);
            mainWindow.Close();

        }
    }
}
