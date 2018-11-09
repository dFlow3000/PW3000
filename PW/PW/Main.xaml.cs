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
using System.IO;
using System.Drawing;

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private MainWindow mainWindow;
        public TextBlock[] infoWindowTextblocks= new TextBlock[10];
        public Main(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void Main_Loaded (object sender, RoutedEventArgs e)
        {
            mainWindow.ActionMenue.Visibility = Visibility.Visible;
            mainWindow.cnvs_PWHeader.Visibility = Visibility.Hidden;

            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            INIFile gIni = new INIFile(Game.iniPath);
            INIFile tableIni = new INIFile(Table.iniPath);

            int teamCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtTeamCnt));
            int actRun = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct));
            int runCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCnt));
            int gamePerRunCnt = Convert.ToInt32(tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtGameProRunCnt));
            int overAllGameCnt = teamCnt / 2 * gamePerRunCnt;
            int actGameCnt = 0;
            int filledTableCnt = 0;
            int tableCntOverall = Convert.ToInt32(tableIni.GetValue(Table.fileSec, Table.fsX_tableCnt));
            for(int i = 1; i <= Convert.ToInt32(tableIni.GetValue(Table.fileSec, Table.fsX_tableSecCnt)); i++)
            {
                Table checkFilledTable = new Table();
                checkFilledTable.Getter(i, actRun);
                if (checkFilledTable.teamsOnTable[0] != 0)
                {
                    filledTableCnt++;
                }
            }

            for(int i = 1; i <= Convert.ToInt32(gIni.GetValue(Const.fileSec, Game.fsX_gameCnt)); i++)
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
            // Fill Main 
            // Header Label
            lbl_sMainHeaderTnmtName.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);
            // Actual Section
            if (actRun == 0)
            {
                lbl_oActRun.Content = "Tunier noch nicht gestartet!";
                lbl_ofinishedGamesAct.Visibility = Visibility.Hidden;
                lbl_sfinishedGamesOverall.Visibility = Visibility.Hidden;
                lbl_ofilledTableAct.Visibility = Visibility.Hidden;
                lbl_sTableCntOverall.Visibility = Visibility.Hidden;
                pgb_ActRun.Visibility = Visibility.Hidden;
                lbl_sPGB_ActRun.Visibility = Visibility.Hidden;
            }
            else
            {
                lbl_oActRun.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtRunCntAct);
                lbl_ofinishedGamesAct.Visibility = Visibility.Visible;
                lbl_sfinishedGamesOverall.Visibility = Visibility.Visible;
                lbl_ofilledTableAct.Visibility = Visibility.Visible;
                lbl_sTableCntOverall.Visibility = Visibility.Visible;
                pgb_ActRun.Visibility = Visibility.Visible;
                lbl_sPGB_ActRun.Visibility = Visibility.Visible;

            }
            lbl_ofinishedGamesAct.Content = Convert.ToString(actGameCnt);
            lbl_sfinishedGamesOverall.Content = Convert.ToString(overAllGameCnt);
            lbl_ofilledTableAct.Content = Convert.ToString(filledTableCnt);
            lbl_sTableCntOverall.Content = Convert.ToString(tableCntOverall);
            double value;
            if (actGameCnt > 0)
            {
                value = actGameCnt * 100 / overAllGameCnt;
            } else
            {
                value = 0; 
            }
            pgb_ActRun.Value = value;
            lbl_sPGB_ActRun.Content = actRun + ". Durchgang";
            Run actRunObj = new Run();
            actRunObj.Getter(actRun);
            if (actRun > 0 && actRunObj.completeState == true)
            {
                value = actRun * 100 / runCnt;
            } else
            {
                value = 0;
            }
            pgb_Tournament.Value = value;
            lbl_sPGB_Tournament.Content = tnmtIni.GetValue(Tournament.tnmtSec, Tournament.tnS_tnmtName);

            if(pgb_Tournament.Value == 100)
            {
                
                if (actRunObj.completeState == true)
                {
                    mainWindow.btn_GoToEvaluation.Style = (Style)Application.Current.Resources["FinalEvaReadyButton"];
                }
            }

            // Overall Section
            lbl_oRunCnt.Content = Convert.ToString(runCnt);
            lbl_oGamePerRunCnt.Content = Convert.ToString(gamePerRunCnt);
            lbl_oTeamCnt.Content = Convert.ToString(teamCnt);
            lbl_oTableCnt.Content = tableIni.GetValue(Const.fileSec, Table.fsX_tableCnt);
    


        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_QuitTnmt_Click(object sender, RoutedEventArgs e)
        {
            Tournament tnmt = new Tournament();
            tnmt.Getter();
            try
            {
                if (tnmt.tnmtRunCnt == tnmt.tnmtActRun)
                {

                    if (MessageBox.Show("Wollen Sie das Tunier wirklich beenden?" +
                                       "\nDie Daten und die finale Rangliste werden gespeichert" +
                                       "\nSpeicherort: " + tnmt.tnmtSpecPath,
                                       "Tunier wirklich beenden?",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Evaluation.SaveEva();
                        Log.Info("Tournament: " + tnmt.tnmtName + " QUIT-FINISHED");
                        switchToFinishFolder(tnmt);
                    }
                }
                else
                {
                    if (MessageBox.Show("Das Tunier ist noch nicht abgeschlossen!" +
                                        "\nWollen Sie das Tunier wirklich beenden?" +
                                        "\nDie Daten und die aktuelle Rangliste werden gespeichert" +
                                        "\nSpeicherort: " + tnmt.tnmtSpecPath,
                                        "Tunier wirklich beenden?",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Evaluation.SaveEva();
                        Log.Info("Tournament: " + tnmt.tnmtName + " QUIT-NOT-FINISHED");
                        switchToFinishFolder(tnmt);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Update(ex.ToString());
                //MessageBox.Show("There is a pdfSharp.dll missing!!! Download it!",
                //                "Missing Lib",
                //                MessageBoxButton.OK,
                //                MessageBoxImage.Error);
                mainWindow.MessageBar(MainWindow.ErrorMessage,
                                        "Missing Lib",
                                        "There i a pdfSharp.dll missing!! Download it!!");
            }


        }

        private void switchToFinishFolder(Tournament tnmt)
        {
            string finishedFolderPath = "";
            try
            {
                finishedFolderPath = System.IO.Path.Combine(tnmt.tnmtSpecPath, "Finished_Tournament_Data");
                Directory.CreateDirectory(finishedFolderPath);
                Log.Create("Directory: " + finishedFolderPath);
            }catch 
            {
                Log.Error("creating Directory: " + finishedFolderPath);
            }

            string newLogPath = System.IO.Path.Combine(finishedFolderPath, Log.iniFileName);
            bool logSwitched = false;

            foreach (var srcPath in Directory.GetFiles(Const.iniFolderPath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                if (srcPath == Log.iniPath)
                {
                    logSwitched = true;
                }
                File.Copy(srcPath, srcPath.Replace(Const.iniFolderPath, finishedFolderPath), true);
                Log.Info("SWITCH " + srcPath + " TO " + finishedFolderPath, logSwitched, newLogPath);
                File.Delete(srcPath);
                Log.Delete(srcPath + " at Tournament Quit while Switch", logSwitched, newLogPath);
            }

            Directory.Delete(Const.iniFolderPath);
            Log.Delete(Const.iniFileFolder + "at Tournament Quit after Switch", logSwitched, newLogPath);
            mainWindow.Close();

        }

        private void btn_GoToSettings_Click(object sender, RoutedEventArgs e)
        {
            UserControl settings = new Settings(mainWindow);
            mainWindow.MainContent.Content = settings;
        }

        private void btn_WindowInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowContent infoWinCon = new InfoWindowContent();
            new MainInfo(infoWinCon.InfoWindowText);
            infoWinCon.FillInfoWindow(infoWinCon.InfoWindowText);
        }

        #endregion
    }

    public class MainInfo
    {
        public const string Header = "Turnier-Informationen";
        public const string Para1_Header = "Aktuelle Informationen";
        public const string Para1_Content1Key = "Aktueller Durchgang:";
        public const string Para1_Content1Value = "Zeigt die Nummer des aktuell gespielten Durchgangs.";
        public const string Para1_Content2Key = "Abschgeschlossene Spiele:";
        public const string Para1_Content2Value = "Zeigt wie viele Spiele des aktuellen Durchgangs bereits erfasst wurden.";
        public const string Para1_Content3Key = "Oberer Fortschrittsbalken:";
        public const string Para1_Content3Value = "Zeigt Fortschritt des aktuell gespielten Durchgangs.";
        public const string Para1_Content4Key = "Unterer Fortschrittsbalken:";
        public const string Para1_Content4Value = "Zeigt Fortschritt des gesamten Turniers.";
        public const string Para2_Header = "Allgemeine Informationen";
        public const string Para2_Content1Key = "Anzahl Durchgänge:";
        public const string Para2_Content1Value ="Zeigt die für das Turnier festgelegte Anzahl an Durchgängen.";
        public const string Para2_Content2Key = "Anzahl Spiele pro Durchgang:";
        public const string Para2_Content2Value = "Zeigt die für das Turnier festgelegte Anzahl an Spielen pro Durchgang.";
        public const string Para2_Content3Key = "Anzahl Teams:";
        public const string Para2_Content3Value = "Zeigt die aktuelle Anzahl angemeldeter Teams.";
        public const string Para2_Content4Key = "Anzahl Tische:";
        public const string Para2_Content4Value = "Zeigt die benötigte Anzahl Tische um mit aktueller Anzahl angemeldeter Teams Tunier umzusetzen.";
        public const string Para3_Header = "Turnier beenden";
        public const string Para3_Content1Value = "Beendet das Turnier und speichert aktuellen Turnierstand in einem Ordner entsprechend des Turniernamens";

        public Bitmap[] imageSource = new Bitmap[4];

        public void ImageSourceFiller (Bitmap[] i_imageArray)
        {
            i_imageArray[0] = Properties.Resources.info;
        }

        public MainInfo(Dictionary<string, string> i_InfoWindowText)
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
            i_InfoWindowText.Add(Para2_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para2_Content1Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content1Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Content2Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content2Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Content3Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content3Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para2_Content4Key, InfoStyles.ParaContentKey);
            i_InfoWindowText.Add(Para2_Content4Value, InfoStyles.ParaContentValue);
            i_InfoWindowText.Add(Para3_Header, InfoStyles.ParaHeader);
            i_InfoWindowText.Add(Para3_Content1Value, InfoStyles.ParaContentValue);
        }

    }
}
