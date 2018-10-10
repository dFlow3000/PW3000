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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.IO;

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

        private void btn_PrintEvaluation_Click(object sender, RoutedEventArgs e)
        {
            Tournament tnmt = new Tournament();
            tnmt.Getter();
            SaveEva();
            MessageBox.Show("Rangliste wurde gespeichert!" +
                            "Speicherort: " + tnmt.tnmtSpecPath,
                            "Speichern erfolgreich",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        public static void SaveEva()
        {
            Tournament tnmt = new Tournament();
            tnmt.Getter();
            Team[] evaTeams = new Team[tnmt.tnmtTeamCnt];
            for (int i = 0; i < evaTeams.Length; i++)
            {
                Team addTeam = new Team();
                addTeam.Getter(i + 1);
                evaTeams[i] = addTeam;
            }

            SortTeamsByWinAndGamePoints(evaTeams);

            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(page);

            XFont fontHeader = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont fontLine = new XFont("Courier New", 12, XFontStyle.Regular);
            XFont fontLineInfo = new XFont("Courier New", 12, XFontStyle.Regular);

            graph.DrawString("Ranglist " + tnmt.tnmtName, fontHeader, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point), XStringFormats.TopCenter);

            string line = "";
            int yPoint = 40;

            line = "Allgemine Daten:";
            graph.DrawString(line, fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 20;
            line = "    Anzahl Durchgänge: " + Convert.ToString(tnmt.tnmtRunCnt);
            graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 20;
            line = "    Anzahl Spiel/Druchgang: " + Convert.ToString(tnmt.tnmtGameProRunCnt);
            graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 20;
            line = "    Anzahl Teams: " + Convert.ToString(tnmt.tnmtTeamCnt);
            graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 50;
            int x = 0;
            line = "";
            foreach (Team team in evaTeams)
            {
                x++;
                line += Convert.ToString(x) + "." + " | ";
                line += team.teamName + " | #";
                line += Convert.ToString(team.teamId) + " | ";
                line += Convert.ToString(team.winPoints) + " | ";
                line += Convert.ToString(team.gamePointsTotal);
                graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                yPoint += 20;
                line = "";
            }


            string pdfFilename = System.IO.Path.Combine(tnmt.tnmtSpecPath, "Rangliste_" + tnmt.tnmtName + "_" + DateTime.Now.ToShortDateString() + ".pdf");
            pdf.Save(pdfFilename);
        }
    }
}
