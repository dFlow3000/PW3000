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
using System.Reflection;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für Evaluation.xaml
    /// </summary>
    public partial class Evaluation : UserControl
    {
        private MainWindow mainWindow;
        public Evaluation(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        public void Evaluation_Loaded (object sender, RoutedEventArgs e)
        {
            INIFile teamIni = new INIFile(Team.iniPath);
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            
            SolidColorBrush brush;
            brush = new SolidColorBrush(Const.SwitchFontColor());
            lbl_sPosHeader.Foreground = brush;
            lbl_sTeamHeader.Foreground = brush; 
            lbl_sWinPointsHeader.Foreground = brush; 
            lbl_sGamePointsHeader.Foreground = brush;
            lbl_sInfoHeader.Foreground = brush;


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
                Label lbl_gamePointsDiff = new Label();

                int x = 0;
                //do
                //{
                foreach (Team team in allTeams)
                {
                    Separator sep = new Separator();
                    Button btn_teamGameInfo = new Button();
                    x++;
                    lbl_rowNumber = new Label();
                    lbl_teamName = new Label();
                    lbl_winPoints = new Label();
                    lbl_gamePointsDiff = new Label();

                    lbl_rowNumber.Content = Convert.ToString(x);
                    lbl_teamName.Content = team.teamName;
                    lbl_winPoints.Content = Convert.ToString(team.winPoints);
                    lbl_gamePointsDiff.Content = Convert.ToString(team.gamePointsTotalDiff);

                    btn_teamGameInfo.Uid = Convert.ToString(team.teamId);
                    btn_teamGameInfo.Content = "i";
                    btn_teamGameInfo.Height = 31.28;
                    btn_teamGameInfo.FontWeight = FontWeights.Bold;
                    btn_teamGameInfo.FontFamily = new FontFamily("Courier New");
                    btn_teamGameInfo.FontSize = 16;
                    btn_teamGameInfo.Foreground = Brushes.WhiteSmoke;
                    LinearGradientBrush lgbrushRed = new LinearGradientBrush();
                    lgbrushRed.StartPoint = new Point(0.5, 0);
                    lgbrushRed.EndPoint = new Point(0.5, 1);
                    lgbrushRed.GradientStops.Add(new GradientStop(Colors.DeepSkyBlue, 1.0));
                    lgbrushRed.GradientStops.Add(new GradientStop(Colors.SkyBlue, 0.0));

                    btn_teamGameInfo.Background = lgbrushRed;
                    btn_teamGameInfo.Click += new RoutedEventHandler(OpenTeamGameInfo);

                    lbl_rowNumber.FontSize = 16;
                    lbl_teamName.FontSize = 16;
                    lbl_winPoints.FontSize = 16;
                    lbl_gamePointsDiff.FontSize = 16;
                    //lbl_rowNumber.FontWeight = FontWeights.Bold;
                    //lbl_teamName.FontWeight = FontWeights.Bold;
                    //lbl_winPoints.FontWeight = FontWeights.Bold;
                    //lbl_gamePoints.FontWeight = FontWeights.Bold;

                    lbl_rowNumber.HorizontalContentAlignment = HorizontalAlignment.Right;
                    lbl_teamName.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lbl_winPoints.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lbl_gamePointsDiff.HorizontalContentAlignment = HorizontalAlignment.Center;

                    switch (x)
                    {
                        case 1:
                            lbl_rowNumber.Background = Brushes.Gold;
                            lbl_teamName.Background = Brushes.Gold;
                            lbl_winPoints.Background = Brushes.Gold;
                            lbl_gamePointsDiff.Background = Brushes.Gold;
                            break;
                        case 2:
                            lbl_rowNumber.Background = Brushes.Silver;
                            lbl_teamName.Background = Brushes.Silver;
                            lbl_winPoints.Background = Brushes.Silver;
                            lbl_gamePointsDiff.Background = Brushes.Silver;
                            break;
                        case 3:
                            lbl_rowNumber.Background = Brushes.SandyBrown;
                            lbl_teamName.Background = Brushes.SandyBrown;
                            lbl_winPoints.Background = Brushes.SandyBrown;
                            lbl_gamePointsDiff.Background = Brushes.SandyBrown;
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
                    stp_GamePointsDiff.Children.Add(lbl_gamePointsDiff);
                    sep = new Separator();
                    stp_GamePointsDiff.Children.Add(sep);
                    stp_TeamGameInfo.Children.Add(btn_teamGameInfo);
                    sep = new Separator();
                    stp_TeamGameInfo.Children.Add(sep);

                }
                //} while (x < 20);
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
                        if (teams[b].gamePointsTotalDiff < teams[b + 1].gamePointsTotalDiff)
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

        private void OpenTeamGameInfo (object sender, EventArgs e)
        {
            Button btn_activ = (Button)sender;
            Window teamGameInfo = new Eva_Team_Data(Convert.ToInt32(btn_activ.Uid));
            teamGameInfo.Show();
        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
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
            XFont fontLineInfo = new XFont("Courier New", 12, XFontStyle.Bold);

            graph.DrawString("Ranglist " + tnmt.tnmtName, fontHeader, XBrushes.Black, new XRect(10, 0, page.Width.Point, page.Height.Point), XStringFormats.TopCenter);

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

            line = Const.posHeader + "|" + Const.teamNumberHeader + "|" + Const.teamNameHeader + "|" + Const.winPointsHeader + "|" + Const.gamePointsDiffHeader;
            graph.DrawString(line, fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 20;
            line = "";

            graph.DrawString(SetRow(), fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            yPoint += 20;

            foreach (Team team in evaTeams)
            {
                x++;
                graph.DrawString(FillPdfTable(team, Convert.ToString(x)), fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                yPoint += 20;
                line = "";
                graph.DrawString(SetRow(), fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                yPoint += 20;
            }

            if (!Directory.Exists(tnmt.tnmtSpecPath))
            {
                Directory.CreateDirectory(tnmt.tnmtSpecPath);
                Log.Create("Directory: " + tnmt.tnmtSpecPath);
            }

            string pdfFileName = "";
            string pdfFilePath = "";
            try
            {
                pdfFileName = "Rangliste_" + tnmt.tnmtName + "_" + DateTime.Now.ToShortDateString() + ".pdf";
                pdfFilePath = System.IO.Path.Combine(tnmt.tnmtSpecPath, pdfFileName);
                pdf.Save(pdfFilePath);
                Log.Create("Evaluation-PDF: " + pdfFileName + " | saved at: " + pdfFilePath);
            }
            catch
            {
                Log.Error("creating Evaluation-PDF: " + pdfFileName + " tried to save at: " + pdfFilePath);
            }
            
        }

        private static string SetRow()
        {
            string retVal = "";
            foreach(char c in Const.posHeader)
            {
                retVal += "-";
            }
            retVal += "+";

            foreach (char c in Const.teamNumberHeader)
            {
                retVal += "-";
            }
            retVal += "+";

            foreach (char c in Const.teamNameHeader)
            {
                retVal += "-";
            }
            retVal += "+";

            foreach (char c in Const.winPointsHeader)
            {
                retVal += "-";
            }
            retVal += "+";
            foreach (char c in Const.gamePointsDiffHeader)
            {
                retVal += "-";
            }

            return retVal;
        }

        private static string FillPdfTable(Team i_team, string i_pos)
        {
            string retVal = "";
            for(int i = 1; i < Const.posHeaderLength - i_pos.Length; i++)
            {
                retVal += " ";
            }

            retVal += i_pos + " |";

            for(int i = 1; i < Const.teamNumberHeaderLength - Convert.ToString(i_team.teamId).Length; i++)
            {
                retVal += " ";
            }

            retVal += i_team.teamId;
            retVal += "  |";

            for(int i = 1; i < Const.teamNameHeaderLength - i_team.teamName.Length; i++)
            {
                retVal += " ";
            }

            retVal += i_team.teamName;
            retVal += " |";

            for(int i = 1; i < Const.winPointsHeaderLength - Convert.ToString(i_team.winPoints).Length; i++)
            {
                retVal += " ";
            }

            retVal += Convert.ToString(i_team.winPoints) + "  |";

            for (int i = 1; i < Const.gamePointsDiffHeaderLength - Convert.ToString(i_team.gamePointsTotalDiff).Length; i++)
            {
                retVal += " ";
            }

            retVal += Convert.ToString(i_team.gamePointsTotalDiff);


            return retVal;
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new EvaluationInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }
    }

    public class EvaluationInfo
    {
        public const string Header = "Rangliste";
        public const string Para1_Header = "Tabelle:";
        public const string Para1_Content1Key = "Platzierung:";
        public const string Para1_Content1Value = "Zeigt die Platzierung.";
        public const string Para1_Content2Key = "Team:";
        public const string Para1_Content2Value = "Zeigt die Teamnamen.";
        public const string Para1_Content3Key = "Gewinn-Punkte:";
        public const string Para1_Content3Value = "Zeigt die erzielten Gewinn-Punkte.";
        public const string Para1_Content4Key = "Diff. Spiel-Punkte:";
        public const string Para1_Content4Value = "Zeigt die in den gewonnenen Spielen erzielte Differenz.";
        public const string Para1_Content5Key = "Info";
        public const string Para1_Content5Value = "Öffnet Info zu gewonnen Spielen | Spiel-Punkte-Differenz.";
        public const string Para2_Header = "PDF erstellen:";
        public const string Para2_Content1Value = "Erstellt ein PDF-Datei mit der aktuellen Rangliste.";


        public EvaluationInfo(Dictionary<string, string> i_InfoWindowText)
        {
            i_InfoWindowText.Add(Header, InfoStyles.HeaderStyle);
            i_InfoWindowText.Add(Para1_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para1_Content1Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para1_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para1_Content2Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para1_Content2Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para1_Content3Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para1_Content3Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para1_Content4Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para1_Content4Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para1_Content5Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para1_Content5Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para2_Content1Value, InfoStyles.ParaContentValue);
        }

    }
}


