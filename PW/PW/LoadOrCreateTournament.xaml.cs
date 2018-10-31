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

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für LoadOrCreateTournament.xaml
    /// </summary>
    public partial class LoadOrCreateTournament : UserControl
    {
        private MainWindow mainWindow;
        public LoadOrCreateTournament(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;

            Const.SwitchColor(i_mainWindow);
            Const.SwitchColor(i_mainWindow);
        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_createTournament_Click(object sender, RoutedEventArgs e)
        {
            UserControl createTnmnt = new AddTournament(mainWindow);
            mainWindow.MainContent.Content = createTnmnt;
        }

        private void btn_loadTournament_Click(object sender, RoutedEventArgs e)
        {
            UserControl loadTnmnt = new LoadTournament(mainWindow);
            mainWindow.MainContent.Content = loadTnmnt;

        }
        #endregion
    }
}
