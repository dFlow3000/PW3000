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
    /// Interaktionslogik für Evaluation.xaml
    /// </summary>
    public partial class Evaluation : UserControl
    {
        private MainWindow mnwd;
        public Evaluation(MainWindow i_mnwd)
        {
            InitializeComponent();
            mnwd = i_mnwd;
        }

        public void Evaluation_Loaded (object sender, RoutedEventArgs e)
        {
            INIFile teamIni = new INIFile(Team.iniPath);
            if (Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) != 0)
            {
                Team[] allTeams = new Team[Convert.ToInt32(teamIni.GetValue(Const.fileSec, Team.fsX_teamCnt))];

                for (int i = 0; i < allTeams.Length; i++)
                {
                    Team addTeam = new Team();
                    addTeam.Getter(i + 1);
                    allTeams[i] = addTeam;
                }

                SortTeamsByWinAndGamePoints(allTeams);

                Label lbl_rowNumber = new Label();
                Label lbl_teamName = new Label();
                Label lbl_winPoints = new Label();
                Label lbl_gamePoints = new Label();

                int x = 0;
                do
                {
                    foreach (Team team in allTeams)
                    {
                        Separator sep = new Separator();
                        x++;
                        lbl_rowNumber = new Label();
                        lbl_teamName = new Label();
                        lbl_winPoints = new Label();
                        lbl_gamePoints = new Label();

                        lbl_rowNumber.Content = Convert.ToString(x);
                        lbl_teamName.Content = team.teamName;
                        lbl_winPoints.Content = Convert.ToString(team.winPoints);
                        lbl_gamePoints.Content = Convert.ToString(team.gamePointsTotal);

                        lbl_rowNumber.FontSize = 16;
                        lbl_teamName.FontSize = 16;
                        lbl_winPoints.FontSize = 16;
                        lbl_gamePoints.FontSize = 16;
                        //lbl_rowNumber.FontWeight = FontWeights.Bold;
                        //lbl_teamName.FontWeight = FontWeights.Bold;
                        //lbl_winPoints.FontWeight = FontWeights.Bold;
                        //lbl_gamePoints.FontWeight = FontWeights.Bold;

                        lbl_rowNumber.HorizontalContentAlignment = HorizontalAlignment.Right;
                        lbl_teamName.HorizontalContentAlignment = HorizontalAlignment.Center;
                        lbl_winPoints.HorizontalContentAlignment = HorizontalAlignment.Center;
                        lbl_gamePoints.HorizontalContentAlignment = HorizontalAlignment.Center;

                        switch (x)
                        {
                            case 1:
                                lbl_rowNumber.Background = Brushes.Gold;
                                lbl_teamName.Background = Brushes.Gold;
                                lbl_winPoints.Background = Brushes.Gold;
                                lbl_gamePoints.Background = Brushes.Gold;
                                break;
                            case 2:
                                lbl_rowNumber.Background = Brushes.Silver;
                                lbl_teamName.Background = Brushes.Silver;
                                lbl_winPoints.Background = Brushes.Silver;
                                lbl_gamePoints.Background = Brushes.Silver;
                                break;
                            case 3:
                                lbl_rowNumber.Background = Brushes.SandyBrown;
                                lbl_teamName.Background = Brushes.SandyBrown;
                                lbl_winPoints.Background = Brushes.SandyBrown;
                                lbl_gamePoints.Background = Brushes.SandyBrown;
                                break;
                        }


                        stp_posNumber.Children.Add(lbl_rowNumber);
                        sep = new Separator();
                        stp_posNumber.Children.Add(sep);
                        stp_TeamName.Children.Add(lbl_teamName);
                        sep = new Separator();
                        stp_TeamName.Children.Add(sep);
                        stp_WinPoints.Children.Add(lbl_winPoints);
                        sep = new Separator();
                        stp_WinPoints.Children.Add(sep);
                        stp_GamePoints.Children.Add(lbl_gamePoints);
                        sep = new Separator();
                        stp_GamePoints.Children.Add(sep);


                    }
                } while (x < 20);
            }
        }

        private static void SortTeamsByWinAndGamePoints(Team[] teams)
        {
            for (int a = 1; a < teams.Length; a++)
            {
                for (int b = 0; b < teams.Length - a; b++)
                {
                    if (teams[b].winPoints < teams[b+1].winPoints)
                    {
                        // swap movies[b] with movies[b+1]
                        Team temp = teams[b];
                        teams[b] = teams[b + 1];
                        teams[b + 1] = temp;
                    }
                }
            }

            for (int a = 1; a < teams.Length; a++)
            {
                for (int b = 0; b < teams.Length - a; b++)
                {
                    if (teams[b].winPoints == teams[b + 1].winPoints)
                    {
                        if (teams[b].gamePointsTotal < teams[b + 1].gamePointsTotal)
                        {
                            // swap movies[b] with movies[b+1]
                            Team temp = teams[b];
                            teams[b] = teams[b + 1];
                            teams[b + 1] = temp;
                        }
                    }
                }
            }

        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mnwd);
            mnwd.MainContent.Content = main;
        }
    }
}
