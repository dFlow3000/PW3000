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

namespace PW
{
    /// <summary>
    /// Interaktionslogik für LoadOrCreateTournament.xaml
    /// </summary>
    public partial class LoadOrCreateTournament : UserControl
    {
        private MainWindow mnwd;
        public LoadOrCreateTournament(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

        private void btn_createTournament_Click(object sender, RoutedEventArgs e)
        {
            UserControl createTnmnt = new AddTournament(mnwd);
            mnwd.MainContent.Content = createTnmnt;
        }

        private void btn_loadTournament_Click(object sender, RoutedEventArgs e)
        {
            UserControl loadTnmnt = new LoadTournament(mnwd);
            mnwd.MainContent.Content = loadTnmnt;

        }
    }
}
