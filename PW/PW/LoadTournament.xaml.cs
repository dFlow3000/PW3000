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

            System.Windows.Controls.UserControl main = new Main(mnwd);
            mnwd.MainContent.Content = main;
        }
    }
}
