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

namespace PW
{
    /// <summary>
    /// Interaktionslogik für LoadTournament.xaml
    /// </summary>
    public partial class LoadTournament : System.Windows.Controls.UserControl
    {
        private MainWindow mnwd;
        public LoadTournament(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

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
            foreach (var srcPath in Directory.GetFiles(tbx_filePath.Text))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(tbx_filePath.Text, Const.iniFolderPath), true);
            }

            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            string specificTnmntPath = System.IO.Path.Combine(Const.CurDirPath, tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName));
            Directory.CreateDirectory(specificTnmntPath);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath, specificTnmntPath);


            System.Windows.Controls.UserControl main = new Main(mnwd);
            mnwd.MainContent.Content = main;
        }
    }
}
