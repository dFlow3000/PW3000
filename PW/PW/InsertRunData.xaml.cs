﻿using System;
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
using System.Reflection;

namespace PW
{
    /// <summary>
    /// Interaktionslogik für InsertRunData.xaml
    /// </summary>
    public partial class InsertRunData : UserControl
    {
        int runId = 0;
        MainWindow mainWindow;
        bool team1Selected, team2Selected, tableSelected, createdRun = false;
        //Aufboren für diff
        int[,] prevTableValues = new int[5, 2];
        int[] prevTableValueGamePointsTotal = new int[2];

        public InsertRunData(MainWindow i_mainWindow, int i_runId)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
            runId = i_runId;
        }

        public void InsertRunData_Loaded(object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            INIFile tIni = new INIFile(Team.iniPath);
            INIFile tableIni = new INIFile(Table.iniPath);

            if (Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)) <  runId)
            {
                Run prevRun = new Run();
                prevRun.Getter(Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct)));
                if (prevRun.completeState)
                {
                    Run newRun = new Run(runId, true);
                    createdRun = true;
                }
            } else
            {
                Run actRun = new Run();
                actRun.Getter(runId);
            }


            int tableCnt = Convert.ToInt32(tIni.GetValue(Const.fileSec, Team.fsX_teamCnt)) / 2;
            FillTeamCmbx(tIni);
            lbl_oRunNumber.Content = Convert.ToString(runId);


            allowGameInput();

            for (int i = 1; i <= tableCnt; i++)
            {
                 Table newTable = new Table(i, runId);
            }
 
            FillTableCmbx(tableIni);

        }

        private void FillTableCmbx(INIFile i_ini)
        {
            cmbx_selectTable.Items.Clear();

            List<string> allTable = new List<string>();
            for(int i = 1; i <= Convert.ToInt32(i_ini.GetValue(Const.fileSec, Table.fsX_tableCnt)); i++)
            {
                allTable.Add(Convert.ToString(i) + ". Tisch");
            }

            foreach(string table in allTable)
            {
                cmbx_selectTable.Items.Add(table);
            }
        }

        private void FillTeamCmbx(INIFile i_ini)
        {
            cmbx_selectTeam1.Items.Clear();
            cmbx_selectTeam2.Items.Clear();

            List<string> allTeams = new List<string>();
            for(int i = 1; i <= Convert.ToInt32(i_ini.GetValue(Const.fileSec, Team.fsX_teamCnt)); i++)
            {
                allTeams.Add(Convert.ToString(i) + " - " + i_ini.GetValue(Team.teamSec + Convert.ToInt32(i), Team.tS_teamName));
            }

            foreach(string teamName in allTeams)
            {
                cmbx_selectTeam1.Items.Add(teamName);
                cmbx_selectTeam2.Items.Add(teamName);
            }

        }

        private void btn_MainMenue_Click(object sender, RoutedEventArgs e)
        {
            UserControl main = new Main(mainWindow);
            mainWindow.MainContent.Content = main;
        }

        private void cmbx_selectTeam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_selectTeam1.SelectedIndex != -1)
            {
                Team selectedTeam = new Team();
                selectedTeam.Getter(cmbx_selectTeam1.SelectedIndex + 1);
                Log.Info("Team Getter Id " + Convert.ToString(cmbx_selectTeam1.SelectedIndex + 1));


                Player player1 = new Player();
                player1.Getter(selectedTeam.teamPlayer[0]);
                Player player2 = new Player();
                player2.Getter(selectedTeam.teamPlayer[1]);

                lbl_oTeam1Player1.Content = player1.playerFirstname + " " + player1.playerLastname;
                lbl_oTeam1Player2.Content = player2.playerFirstname + " " + player2.playerLastname;

                if (cmbx_selectTeam1.SelectedIndex == cmbx_selectTeam2.SelectedIndex)
                {
                    MessageBox.Show("Das gleiche Team wurde als Gegner ausgewählt!" +
                                    "\nWählen Sie eine anderen Gegener!",
                                    "Team wurde doppelt gewählt",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Stop);
                    team1Selected = false;
                }
                else
                {
                    team1Selected = true;


                    INIFile tableIni = new INIFile(Table.iniPath);
                    int tableSecCnt = Convert.ToInt32(tableIni.GetValue(Const.fileSec, Table.fsX_tableSecCnt));

                    for (int i = 1; i <= tableSecCnt; i++)
                    {
                        Table newTable = new Table();
                        newTable.Getter(i, runId);
                        Log.Info("Table Getter Id " + Convert.ToString(i) + " | runId " + Convert.ToString(runId));
                        if (cmbx_selectTeam1.SelectedIndex != -1)
                        {
                            if (newTable.teamsOnTable[0] == cmbx_selectTeam1.SelectedIndex + 1 ||
                               newTable.teamsOnTable[1] == cmbx_selectTeam1.SelectedIndex + 1)
                            {
                                MessageBox.Show("Das Team " + cmbx_selectTeam1.SelectedValue + " wurde bereits ausgewählt!" +
                                                "\n Tisch " + newTable.tableId,
                                                "Team bereits auf einem anderen Tisch",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Stop);
                                team1Selected = false;
                                break;

                            }
                            else
                            {
                                team1Selected = true;
                            }
                        }

                    }
                }
                allowGameInput(true);
            }

        }

        private void cmbx_selectTeam2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_selectTeam2.SelectedIndex != -1)
            {
                Team selectedTeam = new Team();
                selectedTeam.Getter(cmbx_selectTeam2.SelectedIndex + 1);
                Log.Info("Team Getter Id " + Convert.ToString(cmbx_selectTeam2.SelectedIndex + 1));

                Player player1 = new Player();
                player1.Getter(selectedTeam.teamPlayer[0]);
                Player player2 = new Player();
                player2.Getter(selectedTeam.teamPlayer[1]);

                lbl_oTeam2Player1.Content = player1.playerFirstname + " " + player1.playerLastname;
                lbl_oTeam2Player2.Content = player2.playerFirstname + " " + player2.playerLastname;

                if (cmbx_selectTeam1.SelectedIndex == cmbx_selectTeam2.SelectedIndex)
                {
                    MessageBox.Show("Das gleiche Team wurde als Gegner ausgewählt!" +
                                    "\nWählen Sie eine anderen Gegener!",
                                    "Team wurde doppelt gewählt",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Stop);
                    team2Selected = false;
                }
                else
                {
                    team2Selected = true;


                    INIFile tableIni = new INIFile(Table.iniPath);
                    int tableSecCnt = Convert.ToInt32(tableIni.GetValue(Const.fileSec, Table.fsX_tableSecCnt));

                    for (int i = 1; i <= tableSecCnt; i++)
                    {
                        Table newTable = new Table();
                        newTable.Getter(i, runId);
                        Log.Info("Table Getter Id " + Convert.ToString(i) + " | runId " + Convert.ToString(runId));
                        if (cmbx_selectTeam2.SelectedIndex != -1)
                        {
                            if (newTable.teamsOnTable[0] == cmbx_selectTeam2.SelectedIndex + 1 ||
                                newTable.teamsOnTable[1] == cmbx_selectTeam2.SelectedIndex + 1)
                            {
                                MessageBox.Show("Das Team " + cmbx_selectTeam2.SelectedValue + " wurde bereits ausgewählt!" +
                                                "\n Tisch " + newTable.tableId,
                                                "Team bereits auf einem anderen Tisch",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Stop);
                                team2Selected = false;
                                break;
                            }
                            else
                            {
                                team2Selected = true;
                            }
                        }
                    }
                }

                allowGameInput(true);
            }
        }

        private void btn_ToRun_Click(object sender, RoutedEventArgs e)
        {
            switchTbx(false);
            UserControl runMenue = new RunMenue(mainWindow);
            mainWindow.MainContent.Content = runMenue;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if(allowGameInput())
            {
                if (CheckInput())
                {
                    Game[] gamesOnTable = Game.FillTableWithGames(cmbx_selectTeam1.SelectedIndex + 1, cmbx_selectTeam2.SelectedIndex + 1);
                    Table newTable = new Table(cmbx_selectTable.SelectedIndex + 1, runId);
                    Team team1 = new Team();
                    team1.Getter(cmbx_selectTeam1.SelectedIndex + 1);
                    Team team2 = new Team();
                    team2.Getter(cmbx_selectTeam2.SelectedIndex + 1);

                    string[,] gamepoints = new string[,] { { tbx_1GamePoints1Team.Text, tbx_1GamePoints2Team.Text},
                                                           { tbx_2GamePoints1Team.Text, tbx_2GamePoints2Team.Text},
                                                           { tbx_3GamePoints1Team.Text, tbx_3GamePoints2Team.Text} };

                    for(int i = 0; i < gamesOnTable.Length; i++)
                    {
                        newTable.tableGameIds[i] = gamesOnTable[i].gameId;
                        gamesOnTable[i].gamePoints[0] = Convert.ToInt32(gamepoints[i,0]);
                        team1.gamePointsTotal += gamesOnTable[i].gamePoints[0];
                        gamesOnTable[i].gamePoints[1] = Convert.ToInt32(gamepoints[i,1]);
                        team2.gamePointsTotal += gamesOnTable[i].gamePoints[1];

                        if (gamesOnTable[i].gamePoints[0] > gamesOnTable[i].gamePoints[1])
                        {
                            team1.gamePointsTotalDiff += gamesOnTable[i].gamePoints[0] - gamesOnTable[i].gamePoints[1];
                        } else
                        {
                            team2.gamePointsTotalDiff += gamesOnTable[i].gamePoints[1] - gamesOnTable[i].gamePoints[0];
                        }

                        gamesOnTable[i].dpndRun = runId;
                        gamesOnTable[i].Setter();
                    }

                    team1.winPoints += Convert.ToInt32(tbx_iWinPointsTeam1.Text);
                    team2.winPoints += Convert.ToInt32(tbx_iWinPointsTeam2.Text);

                    team1.Setter();
                    team2.Setter();

                    newTable.winPointsAtGame[0] = Convert.ToInt32(tbx_iWinPointsTeam1.Text);
                    newTable.winPointsAtGame[1] = Convert.ToInt32(tbx_iWinPointsTeam2.Text);

                    newTable.teamsOnTable[0] = team1.teamId;
                    newTable.teamsOnTable[1] = team2.teamId;

                    newTable.Setter();

                    allowGameInput(true);

                    cmbx_selectTable.SelectedIndex = cmbx_selectTable.SelectedIndex;
                    showGatheredInfo(newTable);
                    cmbx_selectTeam1.SelectedIndex = -1;
                    cmbx_selectTeam2.SelectedIndex = -1;
                    cmbx_selectTeam1.Visibility = Visibility.Hidden;
                    cmbx_selectTeam2.Visibility = Visibility.Hidden;

                    INIFile tnmtIni = new INIFile(Tournament.iniPath);
                    INIFile gIni = new INIFile(Game.iniPath);

                    int teamCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt));
                    int actRun = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct));
                    int runCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt));
                    int gamePerRunCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
                    int overAllGameCnt = teamCnt / 2 * gamePerRunCnt;
                    int actGameCnt = 0;
                    for (int i = 1; i <= Convert.ToInt32(gIni.GetValue(Const.fileSec, Game.fsX_gameCnt)); i++)
                    {
                        if (Convert.ToInt32(gIni.GetValue(Game.gameSec + Convert.ToString(i), Game.gS_dpndRun)) == actRun)
                        {
                            actGameCnt++;
                        }
                    }
                    if (actGameCnt == overAllGameCnt && actRun != 0)
                    {
                        tnmtIni.SetValue(Tournament.runSec + Convert.ToString(actRun), Tournament.rS_runComplete, Convert.ToString(true));
                    }


                }
                else
                {
                    MessageBox.Show("Es wurden nicht alle Eingaben getätigt!",
                    "Ergänzen sie alle Eingaben zu Gewinn-Punkten und Spiel-Punkten.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                }
            } else
            {
                MessageBox.Show("Wählen Sie erst die Teams und den Tisch aus!",
                "Team und Tisch auswählen",
                MessageBoxButton.OK,
                MessageBoxImage.Stop);
            }
        }

        private bool CheckInput()
        {
            if (tbx_iWinPointsTeam1.Text != String.Empty  &&
                tbx_iWinPointsTeam2.Text != String.Empty  &&
                tbx_1GamePoints1Team.Text != String.Empty &&
                tbx_1GamePoints2Team.Text != String.Empty &&
                tbx_2GamePoints1Team.Text != String.Empty &&
                tbx_2GamePoints2Team.Text != String.Empty &&
                tbx_3GamePoints1Team.Text != String.Empty &&
                tbx_3GamePoints2Team.Text != String.Empty   )
            {
                if (CheckMaxMinPoints(tbx_1GamePoints1Team) &&
                    CheckMaxMinPoints(tbx_1GamePoints2Team) &&
                    CheckMaxMinPoints(tbx_2GamePoints1Team) &&
                    CheckMaxMinPoints(tbx_2GamePoints2Team) &&
                    CheckMaxMinPoints(tbx_3GamePoints1Team) &&
                    CheckMaxMinPoints(tbx_3GamePoints2Team) &&
                    CheckMaxMinPoints(tbx_iWinPointsTeam1, true) &&
                    CheckMaxMinPoints(tbx_iWinPointsTeam2, true)   )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        private void cmbx_selectTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tableSelected = true;
            bool allreadyfilled = true;

            Table selectedTable = new Table(cmbx_selectTable.SelectedIndex + 1, runId);
            foreach( int gameId in selectedTable.tableGameIds)
            {
                if(gameId == 0)
                {
                    allreadyfilled = false;
                    cmbx_selectTeam1.Visibility = Visibility.Visible;
                    cmbx_selectTeam2.Visibility = Visibility.Visible;
                    btn_Edit_GameData.Visibility = Visibility.Hidden;
                    break;
                } else
                {
                    cmbx_selectTeam1.Visibility = Visibility.Hidden;
                    cmbx_selectTeam2.Visibility = Visibility.Hidden;
                    btn_Edit_GameData.Visibility = Visibility.Visible;
                    btn_Save.Visibility = Visibility.Hidden;
                    lbl_oTeam1Number.Visibility = Visibility.Visible;
                    lbl_oTeam2Number.Visibility = Visibility.Visible;
                    lbl_oTeam1Number.Content = Convert.ToString(selectedTable.teamsOnTable[0]);
                    lbl_oTeam2Number.Content = Convert.ToString(selectedTable.teamsOnTable[1]);
                }
            }

            if (allreadyfilled)
            {
                showGatheredInfo(selectedTable);
            }
            else
            {

                allowGameInput(true);
            }
        }

        private void btn_Edit_GameData_Click(object sender, RoutedEventArgs e)
        {
            pwbx_EditPassword.Visibility = Visibility.Visible;
            btn_Edit_GameData_Autorisation.Visibility = Visibility.Visible;
            btn_Edit_GameData.Visibility = Visibility.Hidden;

        }

        private void btn_Edit_GameData_Autorisation_Click(object sender, RoutedEventArgs e)
        {
            if (Crypto.Encrypt(pwbx_EditPassword.Password.ToString()))
            {
                btn_Edit_GameData_Save.Visibility = Visibility.Visible;
                btn_Edit_GameData_Clear.Visibility = Visibility.Visible;
                btn_Edit_GameData_Autorisation.Visibility = Visibility.Hidden;
                pwbx_EditPassword.Visibility = Visibility.Hidden;
                pwbx_EditPassword.Password = String.Empty;

                // Enable Textboxes
                tbx_1GamePoints1Team.IsEnabled = true;
                tbx_1GamePoints2Team.IsEnabled = true;
                tbx_2GamePoints1Team.IsEnabled = true;
                tbx_2GamePoints2Team.IsEnabled = true;
                tbx_3GamePoints1Team.IsEnabled = true;
                tbx_3GamePoints2Team.IsEnabled = true;
                tbx_iWinPointsTeam1.IsEnabled = true;
                tbx_iWinPointsTeam2.IsEnabled = true;

                // [0,0] WinPoints Team1
                prevTableValues[0, 0] = Convert.ToInt32(tbx_iWinPointsTeam1.Text);
                // [0,1] WinPoints Team2
                prevTableValues[0, 1] = Convert.ToInt32(tbx_iWinPointsTeam2.Text);
                // [1,0] Game1Points Team1
                prevTableValues[1, 0] = Convert.ToInt32(tbx_1GamePoints1Team.Text);
                // [1,1] Game1Points Team2
                prevTableValues[1, 1] = Convert.ToInt32(tbx_1GamePoints2Team.Text);
                // [2,0] Game2Points Team1
                prevTableValues[2, 0] = Convert.ToInt32(tbx_2GamePoints1Team.Text);
                // [2,1] Game2Points Team2
                prevTableValues[2, 1] = Convert.ToInt32(tbx_2GamePoints2Team.Text);
                // [3,0] Game3Points Team1
                prevTableValues[3, 0] = Convert.ToInt32(tbx_3GamePoints1Team.Text);
                // [3,1] Game3Points Team2
                prevTableValues[3, 1] = Convert.ToInt32(tbx_3GamePoints2Team.Text);
                // [4,0] DiffPoints Team 1
                prevTableValues[4, 0] = 0;
                // [4,1] DiffPoints Team 2
                prevTableValues[4, 1] = 0;


                for (int i = 1; i <= 3; i++)
                {
                    prevTableValueGamePointsTotal[0] += prevTableValues[i, 0];
                    prevTableValueGamePointsTotal[1] += prevTableValues[i, 1];

                    if (prevTableValues[i, 0] > prevTableValues[i,1])
                    {
                        // [4,0] DiffPoints Team 1
                        prevTableValues[4, 0] += prevTableValues[i, 0] - prevTableValues[i, 1];
                    } else
                    {
                        // [4,1] DiffPoints Team 2
                        prevTableValues[4,1] += prevTableValues[i, 1] - prevTableValues[i, 0];
                    }

                }

            } else
            {
                MessageBox.Show("Das eingegebene Passwort ist flasch!",
                                "Bearbeitung nicht erlaubt!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Stop);
                pwbx_EditPassword.Password = String.Empty;
            }
        }

        private void btn_Edit_GameData_Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                INIFile tnmtIni = new INIFile(Tournament.iniPath);
                int maxGamePerRound = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
                int[] newTableValueGamePointsTotal = new int[2];
                Table updateTable = new Table();
                updateTable.Getter(cmbx_selectTable.SelectedIndex + 1, runId);

                Game[] updateGames = new Game[maxGamePerRound];
                Game addGame = new Game();

                TextBox[,] updateInput = new TextBox[3, 2] { { tbx_1GamePoints1Team, tbx_1GamePoints2Team },
                                                         { tbx_2GamePoints1Team, tbx_2GamePoints2Team },
                                                         { tbx_3GamePoints1Team, tbx_3GamePoints2Team } };
                Team updateTeam1 = new Team();
                updateTeam1.Getter(updateTable.teamsOnTable[0]);
                Team updateTeam2 = new Team();
                updateTeam2.Getter(updateTable.teamsOnTable[1]);

                int diffTeam1 = 0;
                int diffTeam2 = 0;

                for (int i = 0; i < maxGamePerRound; i++)
                {
                    addGame.Getter(updateTable.tableGameIds[i]);
                    updateGames[i] = addGame;
                    updateGames[i].gamePoints[0] = Convert.ToInt32(updateInput[i, 0].Text);
                    newTableValueGamePointsTotal[0] += Convert.ToInt32(updateInput[i, 0].Text);
                    updateGames[i].gamePoints[1] = Convert.ToInt32(updateInput[i, 1].Text);
                    newTableValueGamePointsTotal[1] += Convert.ToInt32(updateInput[i, 1].Text);

                    if (updateGames[i].gamePoints[0] > updateGames[i].gamePoints[1])
                    {
                        diffTeam1 += updateGames[i].gamePoints[0] - updateGames[i].gamePoints[1];
                    }
                    else
                    {
                        diffTeam2 += updateGames[i].gamePoints[1] - updateGames[i].gamePoints[0];
                    }


                    updateGames[i].Setter();
                }

                updateTable.winPointsAtGame[0] = Convert.ToInt32(tbx_iWinPointsTeam1.Text);
                updateTable.winPointsAtGame[1] = Convert.ToInt32(tbx_iWinPointsTeam2.Text);
                updateTable.Setter();

                if (updateTeam1.gamePointsTotal > 0)
                {
                    updateTeam1.gamePointsTotal -= prevTableValueGamePointsTotal[0];
                }
                updateTeam1.gamePointsTotal += newTableValueGamePointsTotal[0];
                if (updateTeam1.winPoints > 0)
                {
                    updateTeam1.winPoints -= prevTableValues[0, 0];
                }
                updateTeam1.winPoints += Convert.ToInt32(tbx_iWinPointsTeam1.Text);
                if (updateTeam1.gamePointsTotalDiff > 0)
                {
                    updateTeam1.gamePointsTotalDiff -= prevTableValues[4, 0];
                }
                updateTeam1.gamePointsTotalDiff += diffTeam1;
                updateTeam1.Setter();

                if (updateTeam2.gamePointsTotal > 0)
                {
                    updateTeam2.gamePointsTotal -= prevTableValueGamePointsTotal[1];
                }
                updateTeam2.gamePointsTotal += newTableValueGamePointsTotal[1];
                if (updateTeam2.winPoints > 0)
                {
                    updateTeam2.winPoints -= prevTableValues[0, 1];
                }
                updateTeam2.winPoints += Convert.ToInt32(tbx_iWinPointsTeam2.Text);
                if (updateTeam1.gamePointsTotalDiff > 0)
                {
                    updateTeam1.gamePointsTotalDiff -= prevTableValues[4, 1];
                }
                updateTeam2.gamePointsTotalDiff += diffTeam2;
                updateTeam2.Setter();


                cmbx_selectTable.SelectedIndex = cmbx_selectTable.SelectedIndex;
                showGatheredInfo(updateTable);
                cmbx_selectTeam1.SelectedIndex = -1;
                cmbx_selectTeam2.SelectedIndex = -1;
                cmbx_selectTeam1.Visibility = Visibility.Hidden;
                cmbx_selectTeam2.Visibility = Visibility.Hidden;
                btn_Edit_GameData.Visibility = Visibility.Visible;
                btn_Edit_GameData_Save.Visibility = Visibility.Hidden;
                btn_Edit_GameData_Clear.Visibility = Visibility.Hidden;

            }
            else
            {
                MessageBox.Show("Es wurden nicht alle Eingaben getätigt!",
                "Ergänzen sie alle Eingaben zu Gewinn-Punkten und Spiel-Punkten.",
                MessageBoxButton.OK,
                MessageBoxImage.Stop);
            }
        }

        private void btn_Edit_GameData_Clear_Click(object sender, RoutedEventArgs e)
        {
            Table selectedTable = new Table(cmbx_selectTable.SelectedIndex + 1, runId);
            showGatheredInfo(selectedTable);
            btn_Edit_GameData.Visibility = Visibility.Visible;
            btn_Edit_GameData_Clear.Visibility = Visibility.Hidden;
            btn_Edit_GameData_Save.Visibility = Visibility.Hidden;
        }

        private bool CheckMaxMinPoints(TextBox i_tbx, bool justMin = false)
        {
            bool retVal = true;
            if (i_tbx.Text != String.Empty)
            {
                if (Convert.ToInt32(i_tbx.Text) > 15 && !justMin)
                {
                    MessageBox.Show("Die maximal erreichbare Punktzahl beträgt 15 Punkte!",
                                    "Maximale Punktzahl überschritten",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);
                    i_tbx.Text = String.Empty;
                    retVal = false;
                }
                else if (Convert.ToInt32(i_tbx.Text) < 0)
                {
                    MessageBox.Show("Die minimal erreichbare Punktzahl beträgt 0 Punkte!",
                                    "Minimale Punktzahl unterschritten",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    i_tbx.Text = String.Empty;
                    retVal = false;
                }
            }

            return retVal;
        }

        private void tbx_1GamePoints1Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_1GamePoints2Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_2GamePoints1Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_2GamePoints2Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_3GamePoints1Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_3GamePoints2Team_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender);
        }

        private void tbx_iWinPointsTeam1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender, true);
        }

        private void tbx_iWinPointsTeam2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CheckMaxMinPoints((TextBox)sender, true);
        }

        private void showGatheredInfo(Table selectedTable)
        {
            // Get Games
            Game firstGame = new Game();
            firstGame.Getter(selectedTable.tableGameIds[0]);
            Game secondGame = new Game();
            secondGame.Getter(selectedTable.tableGameIds[1]);
            Game thirdGame = new Game();
            thirdGame.Getter(selectedTable.tableGameIds[2]);
            // Get Teams
            Team team1 = new Team();
            team1.Getter(selectedTable.teamsOnTable[0]);
            Team team2 = new Team();
            team2.Getter(selectedTable.teamsOnTable[1]);
            // Get Player Team 1
            Player player1Team1 = new Player();
            player1Team1.Getter(team1.teamPlayer[0]);
            Player player2Team1 = new Player();
            player2Team1.Getter(team1.teamPlayer[1]);
            // Get Player Team 2 
            Player player1Team2 = new Player();
            player1Team2.Getter(team2.teamPlayer[0]);
            Player player2Team2 = new Player();
            player2Team2.Getter(team2.teamPlayer[1]);

            tbx_1GamePoints1Team.Text = Convert.ToString(firstGame.gamePoints[0]);
            tbx_1GamePoints2Team.Text = Convert.ToString(firstGame.gamePoints[1]);

            tbx_2GamePoints1Team.Text = Convert.ToString(secondGame.gamePoints[0]);
            tbx_2GamePoints2Team.Text = Convert.ToString(secondGame.gamePoints[1]);

            tbx_3GamePoints1Team.Text = Convert.ToString(thirdGame.gamePoints[0]);
            tbx_3GamePoints2Team.Text = Convert.ToString(thirdGame.gamePoints[1]);

            tbx_iWinPointsTeam1.Text = Convert.ToString(selectedTable.winPointsAtGame[0]);
            tbx_iWinPointsTeam2.Text = Convert.ToString(selectedTable.winPointsAtGame[1]);

            lbl_oTeam1Player1.Content = player1Team1.playerFirstname + " " + player1Team1.playerLastname;
            lbl_oTeam1Player2.Content = player2Team1.playerFirstname + " " + player2Team1.playerLastname;

            lbl_oTeam2Player1.Content = player1Team2.playerFirstname + " " + player1Team2.playerLastname;
            lbl_oTeam2Player2.Content = player2Team2.playerFirstname + " " + player2Team2.playerLastname;

            tbx_iWinPointsTeam1.IsEnabled = false;
            tbx_iWinPointsTeam2.IsEnabled = false;
            tbx_1GamePoints1Team.IsEnabled = false;
            tbx_1GamePoints2Team.IsEnabled = false;
            tbx_2GamePoints1Team.IsEnabled = false;
            tbx_2GamePoints2Team.IsEnabled = false;
            tbx_3GamePoints1Team.IsEnabled = false;
            tbx_3GamePoints2Team.IsEnabled = false;
        }

        private bool allowGameInput(bool clear = false)
        {
            if (tableSelected)
            {
                cmbx_selectTeam1.IsEnabled = true;
                cmbx_selectTeam2.IsEnabled = true;
                if (team1Selected && team2Selected && tableSelected)
                {
                    if (clear)
                    {
                        switchTbx(true);
                    }
                    return true;

                }
                else
                {
                    if (clear)
                    {
                        switchTbx(false);
                    }
                    return false;
                }
            } else
            {
                cmbx_selectTeam1.IsEnabled = false;
                cmbx_selectTeam2.IsEnabled = false;
                switchTbx(false);
                return false;
            }
        }

        private void switchTbx (bool value)
        {
            tbx_iWinPointsTeam1.IsEnabled = value;
            tbx_iWinPointsTeam2.IsEnabled = value;
            tbx_1GamePoints1Team.IsEnabled = value;
            tbx_1GamePoints2Team.IsEnabled = value;
            tbx_2GamePoints1Team.IsEnabled = value;
            tbx_2GamePoints2Team.IsEnabled = value;
            tbx_3GamePoints1Team.IsEnabled = value;
            tbx_3GamePoints2Team.IsEnabled = value;

            tbx_iWinPointsTeam1.Text = String.Empty;
            tbx_iWinPointsTeam2.Text = String.Empty;
            tbx_1GamePoints1Team.Text = String.Empty;
            tbx_1GamePoints2Team.Text = String.Empty;
            tbx_2GamePoints1Team.Text = String.Empty;
            tbx_2GamePoints2Team.Text = String.Empty;
            tbx_3GamePoints1Team.Text = String.Empty;
            tbx_3GamePoints2Team.Text = String.Empty;
            lbl_oTeam1Player1.Content = String.Empty;
            lbl_oTeam1Player2.Content = String.Empty;
            lbl_oTeam2Player1.Content = String.Empty;
            lbl_oTeam2Player2.Content = String.Empty;
        }
    }
}
