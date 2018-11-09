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

            Button[] iA_Btns =
            {
                btn_MainMenue,
                btn_PrintEvaluation
            };

            Settings.SwitchColorStyleDefaultButton(iA_Btns);

            if (CheckTnmtNotFinishState(tnmtIni))
            {
                lbl_oTnmtFinishStatement.Visibility = Visibility.Visible;
            } else
            {
                lbl_oTnmtFinishStatement.Visibility = Visibility.Hidden;
            }
            
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
                int last = allTeams.Length;
                //do
                //{
                foreach (Team team in allTeams)
                {
                    #region create Button n Lable for Table
                    Separator sep = new Separator();
                    Button btn_teamGameInfo = new Button();
                    x++;
                    lbl_rowNumber = new Label();
                    lbl_teamName = new Label();
                    lbl_winPoints = new Label();
                    lbl_gamePointsDiff = new Label();
                    
                    lbl_rowNumber.Background = Brushes.Transparent;
                    lbl_teamName.Background = Brushes.Transparent;
                    lbl_winPoints.Background = Brushes.Transparent;
                    lbl_gamePointsDiff.Background = Brushes.Transparent;
                    
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
                    btn_teamGameInfo.Style = (Style)Application.Current.Resources["InfoButton"];
                    btn_teamGameInfo.Click += new RoutedEventHandler(OpenTeamGameInfo);

                    lbl_rowNumber.FontSize = 16;
                    lbl_teamName.FontSize = 16;
                    lbl_winPoints.FontSize = 16;
                    lbl_gamePointsDiff.FontSize = 16;
                    
                    lbl_rowNumber.HorizontalContentAlignment = HorizontalAlignment.Right;
                    lbl_teamName.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lbl_winPoints.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lbl_gamePointsDiff.HorizontalContentAlignment = HorizontalAlignment.Center;
                    #endregion
                    //switch (x)
                    //{
                    //    case 1:
                    //        lbl_rowNumber.Background = Brushes.DarkGoldenrod;
                    //        lbl_teamName.Background = Brushes.DarkGoldenrod;
                    //        lbl_winPoints.Background = Brushes.DarkGoldenrod;
                    //        lbl_gamePointsDiff.Background = Brushes.DarkGoldenrod;
                    //        lbl_gamePointsTotal.Background = Brushes.DarkGoldenrod;
                    //        break;
                    //    case 2:
                    //        lbl_rowNumber.Background = Brushes.Silver;
                    //        lbl_teamName.Background = Brushes.Silver;
                    //        lbl_winPoints.Background = Brushes.Silver;
                    //        lbl_gamePointsDiff.Background = Brushes.Silver;
                    //        lbl_gamePointsTotal.Background = Brushes.Silver;
                    //        break;
                    //    case 3:
                    //        lbl_rowNumber.Background = Brushes.SandyBrown;
                    //        lbl_teamName.Background = Brushes.SandyBrown;
                    //        lbl_winPoints.Background = Brushes.SandyBrown;
                    //        lbl_gamePointsDiff.Background = Brushes.SandyBrown;
                    //        lbl_gamePointsTotal.Background = Brushes.SandyBrown;
                    //        break;
                    //}

                    if (x == last)
                    {
                        lbl_rowNumber.Background = Brushes.Black;
                        lbl_rowNumber.Foreground = Brushes.White;
                        lbl_teamName.Background = Brushes.Black;
                        lbl_teamName.Foreground = Brushes.White;
                        lbl_winPoints.Background = Brushes.Black;
                        lbl_winPoints.Foreground = Brushes.White;
                        lbl_gamePointsDiff.Background = Brushes.Black;
                        lbl_gamePointsDiff.Foreground = Brushes.White;
                    }
                    
                    if(x >= 1 && x <= 3)
                    {
                        lbl_rowNumber.FontWeight = FontWeights.Bold;
                        lbl_teamName.FontWeight = FontWeights.Bold;
                        lbl_winPoints.FontWeight = FontWeights.Bold;
                        lbl_gamePointsDiff.FontWeight = FontWeights.Bold;
                    }

                    #region add Button n Lable to Table
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
                    #endregion
                }
                //} while (x < 20);
            }
        }

        private static bool CheckTnmtNotFinishState(INIFile i_tnmtIni)
        {
            if (Convert.ToInt32(i_tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) > 0 &&
                Convert.ToBoolean(i_tnmtIni.GetValue(Tournament.runSec + i_tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt), Tournament.rS_runComplete)))
            {
                return false;
            } else
            {
                return true;
            }
        }

        #region Sort - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
        #endregion

        #region PDF - Functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private static void SetHeader(Tournament i_tnmt, XFont i_font, PdfPage i_page, XGraphics i_graph)
        {
            i_graph.DrawString("Rangliste " + i_tnmt.tnmtName, i_font, XBrushes.Black, new XRect(10, 10, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopCenter);
        }

        private static void SetTnmtNotFinishState(INIFile i_tnmtIni, string i_line, XFont i_font, PdfPage i_page, XGraphics i_graph,  ref int i_yPoint)
        {
            if (CheckTnmtNotFinishState(i_tnmtIni))
            {
                i_line = "ZUM ZEITPUNKT DER ERSTELLUNG DIESER DATEI WAR DAS TURNIER NOCH NICHT BEENDET!";
                i_graph.DrawString(i_line, i_font, XBrushes.Red, new XRect(10, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
                i_yPoint += 20;
            }
        }

        private static void SetTimeStamp(ref string i_line, XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            i_line = "Erstellt: " + DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToShortTimeString();
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
        }

        private static void SetDefaultData(Tournament i_tnmt, ref string i_line, XFont i_fontHeader, XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            SetTimeStamp(ref i_line, i_fontHeader, i_page, i_graph, ref i_yPoint);
            i_line = "Allgemine Daten:";
            i_graph.DrawString(i_line, i_fontHeader, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "    Anzahl Durchgänge: " + Convert.ToString(i_tnmt.tnmtRunCnt);
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "    Anzahl Spiel/Druchgang: " + Convert.ToString(i_tnmt.tnmtGameProRunCnt);
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "    Anzahl Teams: " + Convert.ToString(i_tnmt.tnmtTeamCnt);
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 50;
        }

        private static void SetPageNumber(ref string i_line, int pageNumber, XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            i_line = "Seite: " + Convert.ToString(pageNumber);
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "";
        }

        private static void SetTableHeader(ref string i_line, XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            i_line = Const.posHeader + "|" + Const.teamNumberHeader + "|" + Const.teamNameHeader + "|" + Const.winPointsHeader + "|" + Const.gamePointsDiffHeader;
            i_graph.DrawString(i_line, i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "";
            SetTableRowGrid(i_font, i_page, i_graph, ref i_yPoint);
        }

        private static void SetTableRowGrid(XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            i_graph.DrawString(SetRow(), i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
        }

        private static void SetTableRowData(ref string i_line, Team i_team, int i_TeamNumber, XFont i_fontHeader, XFont i_font, PdfPage i_page, XGraphics i_graph, ref int i_yPoint)
        {
            i_graph.DrawString(FillPdfTable(i_team, Convert.ToString(i_TeamNumber)), i_font, XBrushes.Black, new XRect(40, i_yPoint, i_page.Width.Point, i_page.Height.Point), XStringFormats.TopLeft);
            i_yPoint += 20;
            i_line = "";
            SetTableRowGrid(i_fontHeader, i_page, i_graph, ref i_yPoint);
        }

        public static void SaveEva()
        {
            Tournament tnmt = new Tournament();
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
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
            XFont fontNotFinishedState = new XFont("Courier New", 12, XFontStyle.Bold);

            //graph.DrawString("Ranglist " + tnmt.tnmtName, fontHeader, XBrushes.Black, new XRect(10, 10, page.Width.Point, page.Height.Point), XStringFormats.TopCenter);
            SetHeader(tnmt, fontHeader, page, graph);

            string line = "";
            int yPoint = 40;

            //if (CheckTnmtNotFinishState(tnmtIni))
            //{
            //    line = "ZUM ZEITPUNKT DER ERSTELLUNG DIESER DATEI WAR DAS TURNIER NOCH NICHT BEENDET!";
            //    graph.DrawString(line, notFinishedState, XBrushes.Black, new XRect(10, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //    yPoint += 20;
            //}

            SetTnmtNotFinishState(tnmtIni, line, fontNotFinishedState, page, graph, ref yPoint);

            //line = "Allgemine Daten:";
            //graph.DrawString(line, fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 20;
            //line = "    Anzahl Durchgänge: " + Convert.ToString(tnmt.tnmtRunCnt);
            //graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 20;
            //line = "    Anzahl Spiel/Druchgang: " + Convert.ToString(tnmt.tnmtGameProRunCnt);
            //graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 20;
            //line = "    Anzahl Teams: " + Convert.ToString(tnmt.tnmtTeamCnt);
            //graph.DrawString(line, fontLine, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 50;

            SetDefaultData(tnmt, ref line, fontLineInfo, fontLine, page, graph, ref yPoint);

            int x = 0;
            line = "";

            //line = Const.posHeader + "|" + Const.teamNumberHeader + "|" + Const.teamNameHeader + "|" + Const.winPointsHeader + "|" + Const.gamePointsDiffHeader;
            //graph.DrawString(line, fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 20;
            //line = "";

            int pageNumber = 1;
            SetPageNumber(ref line, pageNumber, fontLineInfo, page, graph, ref yPoint);

            SetTableHeader(ref line, fontLineInfo, page, graph, ref yPoint);

            //graph.DrawString(SetRow(), fontLineInfo, XBrushes.Black, new XRect(40, yPoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //yPoint += 20;

            

            int rowCnt = 0;
            int rowCntperPage = 13;
            foreach (Team team in evaTeams)
            {
                if (rowCnt < rowCntperPage)
                {
                    x++;
                    SetTableRowData(ref line, team, x, fontLineInfo, fontLine, page, graph, ref yPoint);
                    rowCnt++;
                } else
                {
                    page = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(page);
                    rowCnt = 0;
                    yPoint = 40;
                    pageNumber += 1;
                    SetTimeStamp(ref line, fontLineInfo, page, graph, ref yPoint);
                    SetPageNumber(ref line, pageNumber, fontLineInfo, page, graph, ref yPoint);
                    SetTableHeader(ref line, fontLineInfo, page, graph, ref yPoint);
                    x++;
                    SetTableRowData(ref line, team, x, fontLineInfo, fontLine, page, graph, ref yPoint);
                }
               
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
        #endregion

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void OpenTeamGameInfo(object sender, EventArgs e)
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
            //MessageBox.Show("Rangliste wurde gespeichert!" +
            //                "Speicherort: " + tnmt.tnmtSpecPath,
            //                "Speichern erfolgreich",
            //                MessageBoxButton.OK,
            //                MessageBoxImage.Information);
            mainWindow.MessageBar(MainWindow.InfoMessage,
                                    "Speichern erfolgreich",
                                    "Rangliste wurde gespeichert!" +
                                    "\nSpeicherort: " + tnmt.tnmtSpecPath);
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new EvaluationInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }
        #endregion
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


