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
    /// Interaktionslogik für PlayedGameHistory.xaml
    /// </summary>
    public partial class PlayedGameHistory : Window
    {
        int calledTeamId = 0;
        const int NOT_PLAYED = 4000;
        const int gamePoints_calledTeam = 0;
        const int gamePoints_oppondTeam = 1;
        const int gameId = 2;
        const int tableId = 3;
        const int opponedTeamId = 4;

        private Label[,] tableLable;

        struct gameData
        {
            public int runId;
            public int tableId;
            public int winPoints;
            public int gameId;
            public int calledTeamPoints;
            public int oppondTeamPoints;
            public int calledTeamId;
            public int oppondTeamId;
        }

        public PlayedGameHistory(int i_teamId)
        {
            calledTeamId = i_teamId;
            InitializeComponent();
        }

        private void PlayedGameHistory_Loaded(object sender, RoutedEventArgs e)
        {
            Button[] iA_Buttons =
{
                btn_Close
            };
            Settings.SwitchColorStyleDefaultButton(iA_Buttons);
            Const.SwitchColor(this);
            FillPlayedGameHistory();
        }

        private void FillPlayedGameHistory()
        {
            INIFile tableIni = new INIFile(Table.iniPath);
            Tournament tnmt = new Tournament();
            tnmt.Getter();
            Team calledTeam = new Team();
            calledTeam.Getter(calledTeamId);

            /// <summary>
            ///     <para>
            ///       0,1:  [0/1, 0/1/2, 0/1] -> 1/2 RUN 1/2/3 GAME 1/2 TEAM-POINTS
            ///     </para>
            ///     <para>
            ///       2: [0/1, 0/1/2, 2] -> 1/2 RUN 1/2/3 GAME X GAME ID
            ///     </para>
            ///     <para>
            ///       3: [0/1, 0/1/2, 2] -> 1/2 RUN 1/2/3 GAME X TABLE
            ///     </para>
            ///     <para>
            ///       4: [0/1, 0/1/2, 3] -> 1/2 RUN 1/2/3 GAME X APPONEND TEAM ID
            ///     </para>
            /// </summary>
            //int[,,] gameStandings = GetGameStandings(calledTeam.teamId, tnmt);
            gameData[,] gameData = GetGameData(calledTeam.teamId, tnmt);
            int gamePointsTotal = 0;
            int diffTotal = 0;
            int rowsPerRun = 2 + tnmt.tnmtGameProRunCnt;
            cnvs_playedGameHistory.MinHeight = tnmt.tnmtRunCnt * rowsPerRun * 28 + 200;

            tableLable = new Label[tnmt.tnmtRunCnt * tnmt.tnmtGameProRunCnt, 6];
            int gameDataIdentifyer = -1;

            for (int i = 0; i < tnmt.tnmtRunCnt; i++)
            {
                for (int x = -3; x < tnmt.tnmtGameProRunCnt; x++)
                {
                    Label lbl_runId = new Label();
                    Label lbl_gameId = new Label();
                    Label lbl_calledTeam = new Label();
                    Label lbl_colon = new Label();
                    Label lbl_oppondTeam = new Label();
                    Label lbl_diffCalledTeam = new Label();

                    lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];
                    lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];
                    lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];
                    lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];
                    lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];
                    lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryDefLabel"];

                    if (x >= 0)
                    {
                        if (gameData[i, x].tableId != NOT_PLAYED)
                        {
                            gameDataIdentifyer++;

                            lbl_runId.Content = String.Empty;
                            lbl_gameId.Content = Convert.ToString(gameData[i, x].gameId);
                            lbl_gameId.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_calledTeam.Content = Convert.ToString(gameData[i, x].calledTeamPoints);
                            lbl_calledTeam.HorizontalContentAlignment = HorizontalAlignment.Right;
                            lbl_colon.Content = ":";
                            lbl_colon.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_oppondTeam.Content = (gameData[i,x].oppondTeamPoints < 10 ? " " : "") + Convert.ToString(gameData[i, x].oppondTeamPoints);
                            lbl_oppondTeam.HorizontalContentAlignment = HorizontalAlignment.Left;
                            lbl_diffCalledTeam.Content = Convert.ToString(gameData[i, x].calledTeamPoints - gameData[i, x].oppondTeamPoints);
                            lbl_diffCalledTeam.HorizontalContentAlignment = HorizontalAlignment.Right;

                            lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];

                            lbl_runId.MouseEnter += new MouseEventHandler(MarkUpActRow);
                            lbl_gameId.MouseEnter += new MouseEventHandler(MarkUpActRow);
                            lbl_calledTeam.MouseEnter += new MouseEventHandler(MarkUpActRow);
                            lbl_colon.MouseEnter += new MouseEventHandler(MarkUpActRow);
                            lbl_oppondTeam.MouseEnter += new MouseEventHandler(MarkUpActRow);
                            lbl_diffCalledTeam.MouseEnter += new MouseEventHandler(MarkUpActRow);

                            lbl_runId.MouseLeave += new MouseEventHandler(MarkDownActRow);
                            lbl_gameId.MouseLeave += new MouseEventHandler(MarkDownActRow);
                            lbl_calledTeam.MouseLeave += new MouseEventHandler(MarkDownActRow);
                            lbl_colon.MouseLeave += new MouseEventHandler(MarkDownActRow);
                            lbl_oppondTeam.MouseLeave += new MouseEventHandler(MarkDownActRow);
                            lbl_diffCalledTeam.MouseLeave += new MouseEventHandler(MarkDownActRow);

                            lbl_runId.Uid = Convert.ToString(gameDataIdentifyer);
                            lbl_gameId.Uid = Convert.ToString(gameDataIdentifyer);
                            lbl_calledTeam.Uid = Convert.ToString(gameDataIdentifyer);
                            lbl_colon.Uid = Convert.ToString(gameDataIdentifyer);
                            lbl_oppondTeam.Uid = Convert.ToString(gameDataIdentifyer);
                            lbl_diffCalledTeam.Uid = Convert.ToString(gameDataIdentifyer);

                            tableLable[gameDataIdentifyer, 0] = lbl_runId;
                            tableLable[gameDataIdentifyer, 1] = lbl_gameId;
                            tableLable[gameDataIdentifyer, 2] = lbl_calledTeam;
                            tableLable[gameDataIdentifyer, 3] = lbl_colon;
                            tableLable[gameDataIdentifyer, 4] = lbl_oppondTeam;
                            tableLable[gameDataIdentifyer, 5] = lbl_diffCalledTeam;

                            gamePointsTotal += gameData[i, x].calledTeamPoints;
                            diffTotal += gameData[i, x].calledTeamPoints - gameData[i, x].oppondTeamPoints;
                        } else
                        {
                            lbl_runId.Content = String.Empty;
                            lbl_gameId.Content = "-";
                            lbl_gameId.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_calledTeam.Content = "0";
                            lbl_calledTeam.HorizontalContentAlignment = HorizontalAlignment.Right;
                            lbl_colon.Content = ":";
                            lbl_colon.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_oppondTeam.Content = " 0";
                            lbl_oppondTeam.HorizontalContentAlignment = HorizontalAlignment.Left;
                            lbl_diffCalledTeam.Content = "0";
                            lbl_diffCalledTeam.HorizontalContentAlignment = HorizontalAlignment.Right;

                            lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                        }
                    }
                    else
                    {
                        if (x == -3)
                        {
                            lbl_runId.Content = Convert.ToString(gameData[i,0].runId);
                            lbl_runId.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_gameId.Content = String.Empty;
                            lbl_calledTeam.Content = "Tisch " + (gameData[i,0].tableId == NOT_PLAYED ? "X" : Convert.ToString(gameData[i,0].tableId));
                            lbl_colon.Content = String.Empty;
                            lbl_oppondTeam.Content = String.Empty;
                            lbl_diffCalledTeam.Content = String.Empty;

                            if (gameData[i, 0].tableId != NOT_PLAYED)
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel"];
                            } else
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryRunTableHeaderLabel_NOTPLAYED"];
                            }
                        } else if (x == -2)
                        {
                            Team opponedTeam = new Team();
                            opponedTeam.Getter(gameData[i, 0].oppondTeamId);

                            lbl_runId.Content = String.Empty;
                            lbl_gameId.Content = String.Empty;
                            lbl_calledTeam.Content = calledTeam.teamName;
                            lbl_calledTeam.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl_colon.Content = "gegen";
                            lbl_colon.HorizontalContentAlignment = HorizontalAlignment.Center;
                            if (opponedTeam.teamName != null)
                            {
                                lbl_oppondTeam.Content = opponedTeam.teamName;
                                lbl_oppondTeam.HorizontalContentAlignment = HorizontalAlignment.Center;
                            }
                            else
                            {
                                lbl_oppondTeam.Content = String.Empty;

                            }
                            lbl_diffCalledTeam.Content = String.Empty;

                            if (gameData[i, 0].tableId != NOT_PLAYED)
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel"];
                            } else
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryTeamNamesLabel_NOTPLAYED"];
                            }
                        }
                        else
                        {
                            lbl_runId.Content = String.Empty;
                            lbl_gameId.Content = String.Empty;
                            lbl_calledTeam.Content = "Gewinn-Punkte: " + Convert.ToString(gameData[i, 0].winPoints);
                            lbl_calledTeam.HorizontalContentAlignment = HorizontalAlignment.Right;
                            lbl_colon.Content = String.Empty;
                            lbl_oppondTeam.Content = String.Empty;
                            lbl_diffCalledTeam.Content = String.Empty;

                            if (gameData[i, 0].tableId != NOT_PLAYED)
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
                            } else
                            {
                                lbl_runId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                                lbl_gameId.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                                lbl_calledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                                lbl_colon.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                                lbl_oppondTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                                lbl_diffCalledTeam.Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel_NOTPLAYED"];
                            }
                        }
                    }

                    //Separator sep = new Separator();
                    stp_RunDATA.Children.Add(lbl_runId);
                    //stp_RunDATA.Children.Add(sep);

                    //sep = new Separator();
                    stp_GameNrDATA.Children.Add(lbl_gameId);
                    //stp_GameNrDATA.Children.Add(sep);

                    //sep = new Separator();
                    stp_CalledTeamNameDATA.Children.Add(lbl_calledTeam);
                    //stp_CalledTeamNameDATA.Children.Add(sep);

                    //sep = new Separator();
                    stp_ColonDATA.Children.Add(lbl_colon);
                    //stp_ColonDATA.Children.Add(sep);

                    //sep = new Separator();
                    stp_OppondTeamNameDATA.Children.Add(lbl_oppondTeam);
                    //stp_OppondTeamNameDATA.Children.Add(sep);

                    //sep = new Separator();
                    stp_DiffDATA.Children.Add(lbl_diffCalledTeam);
                    //stp_DiffDATA.Children.Add(sep);
                }
            }
            lbl_oGamePointsTotal.Content = "Spiel-Punkte gesamt: " + (gamePointsTotal < 10 ? "  " : " ") + Convert.ToString(gamePointsTotal);
            lbl_oGamePointsTotal.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbl_oWinPointsTotal.Content ="Gewinn-Punkte gesamt:" + (calledTeam.winPoints < 10 ? "  " : " ") + Convert.ToString(calledTeam.winPoints);
            lbl_oWinPointsTotal.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbl_oDiffTotal.Content = "Gesamt: " + (diffTotal < 10 && diffTotal > -10 ? "  " : " ") + Convert.ToString(diffTotal);
            lbl_oDiffTotal.HorizontalContentAlignment = HorizontalAlignment.Right;
        }

        private void MarkUpActRow(object sender, EventArgs e)
        {
            Label actLabel = (Label)sender;
            for (int i = 0; i < 6; i++)
            {
                tableLable[Convert.ToInt32(actLabel.Uid), i].Style = (Style)Application.Current.Resources["playedGameHistoryGameDataMouseOverLabel"];
            }            
        }

        private void MarkDownActRow(object sender, EventArgs e)
        {
            Label actLabel = (Label)sender;
            for (int i = 0; i < 6; i++)
            {
                tableLable[Convert.ToInt32(actLabel.Uid), i].Style = (Style)Application.Current.Resources["playedGameHistoryGameDataLabel"];
            }

        }

        private gameData[,] GetGameData(int i_calledTeamId, Tournament i_tnmt)
        {
            gameData[,] retVal = new gameData[i_tnmt.tnmtRunCnt, i_tnmt.tnmtGameProRunCnt];

            for (int i = 1; i <= i_tnmt.tnmtRunCnt; i++)
            {
                Table playedTable = new Table();
                playedTable.GetTableByTeam(i_calledTeamId, i);
                

                for (int x = 0; x < i_tnmt.tnmtGameProRunCnt; x++)
                {
                    Game playedGame = new Game();
                    playedGame.Getter(playedTable.tableGameIds[x]);
                    retVal[(i - 1), x].runId = i;
                    retVal[(i - 1), x].winPoints = playedTable.teamsOnTable[0] == i_calledTeamId ? playedTable.winPointsAtGame[0] : playedTable.winPointsAtGame[1];
                    if (playedGame.gameTeams[0] != 0)
                    {
                        retVal[(i - 1), x].calledTeamId = i_calledTeamId;
                        retVal[(i - 1), x].calledTeamPoints = playedGame.gameTeams[0] == i_calledTeamId ? playedGame.gamePoints[0] : playedGame.gamePoints[1];
                        retVal[(i - 1), x].oppondTeamPoints = playedGame.gameTeams[0] == i_calledTeamId ? playedGame.gamePoints[1] : playedGame.gamePoints[0];
                        retVal[(i - 1), x].gameId = playedGame.gameId;
                    }
                    else
                    {

                        retVal[(i - 1), x].calledTeamId = NOT_PLAYED;
                        retVal[(i - 1), x].calledTeamPoints = NOT_PLAYED;
                        retVal[(i - 1), x].oppondTeamPoints = NOT_PLAYED;
                        retVal[(i - 1), x].gameId = NOT_PLAYED;
                    }

                    if (playedTable.tableId != 0)
                    {
                        retVal[(i - 1), x].tableId = playedTable.tableId;
                        retVal[(i - 1), x].oppondTeamId = playedTable.teamsOnTable[0] == i_calledTeamId ? playedTable.teamsOnTable[1] : playedTable.teamsOnTable[0];
                    }
                    else
                    {
                        retVal[(i - 1), x].tableId = NOT_PLAYED;
                        retVal[(i - 1), x].oppondTeamId = NOT_PLAYED;
                    }
                }
            }
            return retVal;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
}
