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
using System.Windows.Forms;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für LoadTournament.xaml
    /// </summary>
    public partial class LoadTournament : System.Windows.Controls.UserControl
    {
        private MainWindow mainWindow;
        public LoadTournament(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void LoadTournament_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button[] iA_Btns =
            {
                btn_ExplorerSearch,
                btn_LoadTnmt
            };
            Settings.SwitchColorStyleDefaultButton(iA_Btns);
        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_ExplorerSearch_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if(browser.ShowDialog() == DialogResult.OK)
            {
                tbx_filePath.Text = browser.SelectedPath;
            }
        }

        private void btn_LoadTnmt_Click(object sender, RoutedEventArgs e)
        {
            bool loadFailed = false;
            try
            {
                foreach (var srcPath in Directory.GetFiles(tbx_filePath.Text))
                {
                    //Copy the file from sourcepath and place into mentioned target path, 
                    //Overwrite the file if same file is exist in target path
                    File.Copy(srcPath, srcPath.Replace(tbx_filePath.Text, Const.iniFolderPath), true);
                }
            }catch
            {
                loadFailed = true;
                Log.Error("Not able to load Files from " + tbx_filePath.Text);
                //System.Windows.MessageBox.Show("Es war nicht möglich diese Tunierdaten zu laden!" +
                //    "\nStellen Sie sicher, dass Sie aus einem \"Finished_Tournament\" - Ordner laden!",
                //    "Laden nicht möglich",
                //    MessageBoxButton.OK,
                //    MessageBoxImage.Error);
                mainWindow.MessageBar(MainWindow.ErrorMessage,
                                        "Laden nicht möglich",
                                        "Es war nicht möglich diese Tunierdaten zu laden!" +
                                        "\nStellen Sie sicher, dass Sie aus einem \"Finished_Tournament\" - Ordner laden!");
            }

            if (!loadFailed)
            {
                INIFile tnmtIni = new INIFile(Tournament.iniPath);
                try
                {
                    string specificTnmntPath = System.IO.Path.Combine(Const.CurDirPath, tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName));
                    Directory.CreateDirectory(specificTnmntPath);
                    tnmtIni.SetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath, specificTnmntPath);


                    System.Windows.Controls.UserControl main = new Main(mainWindow);
                    mainWindow.MainContent.Content = main;
                } catch
                {
                    loadFailed = true;
                    Log.Error("Not able to load Files from " + tbx_filePath.Text);
                    //System.Windows.MessageBox.Show("Es war nicht möglich diese Tunierdaten zu laden!" +
                    //    "\nStellen Sie sicher, dass Sie aus einem \"Finished_Tournament\" - Ordner laden!",
                    //    "Laden nicht möglich",
                    //    MessageBoxButton.OK,
                    //    MessageBoxImage.Error);
                    mainWindow.MessageBar(MainWindow.ErrorMessage,
                                            "Laden nicht möglich",
                                            "Es war nicht möglich diese Tunierdaten zu laden!" +
                                            "\nStellen Sie sicher, dass Sie aus einem \"Finished_Tournament\" - Ordner laden!");
                    loadFailed = true;
                }
            }

            if(loadFailed)
            {
                System.Windows.Controls.UserControl loadorcreate = new LoadOrCreateTournament(mainWindow);
                mainWindow.MainContent.Content = loadorcreate;
            }
        }
        #endregion
    }
}
