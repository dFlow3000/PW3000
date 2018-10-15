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

namespace PW
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
            if (winCnt == team.winPoints)
            {
                lbl_swinPoints.Content = Convert.ToString(team.winPoints);
            }
            else
            {
                lbl_swinPoints.Content = Convert.ToString(winCnt) + "(+" + Convert.ToString(team.winPoints - winCnt) + ")";
            }

        }

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
                lbl_DiffPoints.Content = "0";
                lbl_DiffPoints.Background = Brushes.LightGray;
                lbl_DiffPoints.Foreground = Brushes.SlateGray;
                lbl_TeamPoints.Background = Brushes.LightGray;
                lbl_TeamPoints.Foreground = Brushes.SlateGray;
                lbl_AponPoints.Background = Brushes.LightGray;
                lbl_AponPoints.Foreground = Brushes.SlateGray;
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

        private void btn_CloseTeamGameInfo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
