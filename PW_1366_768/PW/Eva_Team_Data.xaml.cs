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
using System.Windows.Shapes;
using Nocksoft.IO.ConfigFiles;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für Eva_Team_Data.xaml
    /// </summary>
    public partial class Eva_Team_Data : Window
    {
        private int teamId;
        private int winCnt = 0;
        public Eva_Team_Data(int i_teamId)
        {
            InitializeComponent();
            teamId = i_teamId;
        }

        private void Eva_Team_Data_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            Button[] iA_Buttons =
            {
                btn_CloseTeamGameInfo,
                btn_WindowInfo
            };
            Settings.SwitchColorStyleDefaultButton(iA_Buttons);
            Const.SwitchColor(this);
            SolidColorBrush brush;
            brush = new SolidColorBrush(Const.SwitchFontColor());

            lbl_sGameNr.Foreground = brush;
            lbl_sTeamPoints.Foreground = brush;
            lbl_swinPoints.Foreground = brush;
            lbl_sDiffPoints.Foreground = brush;
            lbl_oSumDiffPoints.Foreground = brush;
            lbl_oSumTeamPoints.Foreground = brush;
            lbl_sAponPoints.Foreground = brush;
            lbl_sGamePointsTotal.Foreground = brush;
            lbl_sTotal.Foreground = brush;


            INIFile gameIni = new INIFile(Game.iniPath);
            Tournament tnmt = new Tournament();
            tnmt.Getter();

            Data.MinHeight = tnmt.tnmtGameProRunCnt * tnmt.tnmtRunCnt * 28 + 100;

            int gameCnt = Convert.ToInt32(gameIni.GetValue(Const.fileSec, Game.fsX_gameCnt));
            int playedGameCnt = tnmt.tnmtGameProRunCnt * tnmt.tnmtRunCnt;

            for(int i = 1; i <= gameCnt; i++)
            {
                Game gameOfIni = new Game();
                gameOfIni.Getter(i);

                if (gameOfIni.gameTeams[0] == teamId)
                {
                    FillLable(0, 1, gameOfIni);
                } else if (gameOfIni.gameTeams[1] == teamId)
                {
                    FillLable(1, 0, gameOfIni);
                }
            }

            Team team = new Team();
            team.Getter(teamId);

            lbl_oTeamName.Content = team.teamName;
            lbl_oTeamNr.Content = Convert.ToString(team.teamId);

            if (winCnt == team.winPoints)
            {
                lbl_swinPoints.Content = Convert.ToString(team.winPoints);
            }
            else
            {
                lbl_swinPoints.Content = Convert.ToString(winCnt) + "(+" + Convert.ToString(team.winPoints - winCnt) + ")";
            }

        }

        #region Fill - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void FillLable(int i_teamPos, int i_aponPos, Game i_game)
        {
            Label lbl_GameId = new Label();
            Label lbl_TeamPoints = new Label();
            Label lbl_AponPoints = new Label();
            Label lbl_DiffPoints = new Label();

            lbl_GameId.HorizontalContentAlignment = HorizontalAlignment.Center;
            lbl_TeamPoints.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbl_AponPoints.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbl_DiffPoints.HorizontalContentAlignment = HorizontalAlignment.Right;

            lbl_GameId.Content = Convert.ToString(i_game.gameId);
            lbl_TeamPoints.Content = Convert.ToString(i_game.gamePoints[i_teamPos]);
            lbl_oSumTeamPoints.Content = Convert.ToString(Convert.ToInt32(lbl_oSumTeamPoints.Content) + Convert.ToInt32(lbl_TeamPoints.Content));
            lbl_AponPoints.Content = Convert.ToString(i_game.gamePoints[i_aponPos]);
            if (i_game.gamePoints[i_teamPos] > i_game.gamePoints[i_aponPos])
            {
                lbl_DiffPoints.Content = Convert.ToString(i_game.gamePoints[i_teamPos] - i_game.gamePoints[i_aponPos]);
                lbl_oSumDiffPoints.Content = Convert.ToString(Convert.ToInt32(lbl_oSumDiffPoints.Content) + Convert.ToInt32(lbl_DiffPoints.Content));
                winCnt++;
            } else
            {
                //lbl_DiffPoints.Content = "0";
                lbl_DiffPoints.Content = Convert.ToString(i_game.gamePoints[i_teamPos] - i_game.gamePoints[i_aponPos]);
                lbl_oSumDiffPoints.Content = Convert.ToString(Convert.ToInt32(lbl_oSumDiffPoints.Content) + Convert.ToInt32(lbl_DiffPoints.Content));
                lbl_GameId.Background = Brushes.IndianRed;
                lbl_GameId.Foreground = Brushes.Black;
                lbl_DiffPoints.Background = Brushes.IndianRed;
                lbl_DiffPoints.Foreground = Brushes.Black;
                lbl_TeamPoints.Background = Brushes.IndianRed;
                lbl_TeamPoints.Foreground = Brushes.Black;
                lbl_AponPoints.Background = Brushes.IndianRed;
                lbl_AponPoints.Foreground = Brushes.Black;
            }


            Separator sep = new Separator();
            stp_GameId.Children.Add(lbl_GameId);
            stp_GameId.Children.Add(sep);
            sep = new Separator();
            stp_TeamPoints.Children.Add(lbl_TeamPoints);
            stp_TeamPoints.Children.Add(sep);
            sep = new Separator();
            stp_AponPoints.Children.Add(lbl_AponPoints);
            stp_AponPoints.Children.Add(sep);
            sep = new Separator();
            stp_DiffPoints.Children.Add(lbl_DiffPoints);
            stp_DiffPoints.Children.Add(sep);
            
        }
        #endregion

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_CloseTeamGameInfo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new Eva_Team_DataInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }
        #endregion

        #region Titlebar - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void btn_MainWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_MainWindowClose_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragDropTitelBar(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        #endregion
    }

    public class Eva_Team_DataInfo
    {
        public const string Header = "Info zu gewonnen Spielen | Spiel-Punkte-Differenz";
        public const string Para1_Header = "Tabelle:";
        public const string Para1_Content1Key = "Spiel-Nr:";
        public const string Para1_Content1Value = "Zeigt die ID eines Spiels.";
        public const string Para1_Content2Key = "Team-Punkte:";
        public const string Para1_Content2Value = "Zeigt die vom aktuell ausgewählten Team erzielte Punktezahl im jeweiligen Spiel.";
        public const string Para1_Content3Key = "Gegener-Punkte:";
        public const string Para1_Content3Value = "Zeigt die vom Gegener erzielte Punktezahl im jeweiligen Spiel.";
        public const string Para1_Content4Key = "Differenz:";
        public const string Para1_Content4Value = "Zeigt die in einem gewonnenen Spiel erzielte Differenz.";
        public const string Para1_Content5Key = "Graue Einträge";
        public const string Para1_Content5Value = "Grau hinterlegte Einträge zeigen die Daten der verlorenen Spiele.";
        public const string Para2_Header = "Gesamt:";
        public const string Para2_Content1Value = "Gesamt-Team-Punkte und Gesamt-Differenz-Punkte";
        public const string Para3_Header = "Gewinn-Punkte:";
        public const string Para3_Content1Value = "Gesamt-Gewinn-Punkte die im Turnier erzielt wurden. Die Klammern enthalten die Menge an zusätzlichen Gewinnpunkten, " +
            "erzielt durch \"Nackerte\" oder \"Zruckgschleiderte\"";

        public Eva_Team_DataInfo(Dictionary<string, string> i_InfoWindowText)
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
            i_InfoWindowText.Add(Para3_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para3_Content1Value, InfoStyles.ParaContentValue);
        }

    }

}
