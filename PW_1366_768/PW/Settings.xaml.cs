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

namespace Preiswattera_3000
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        private MainWindow mainWindow;
        private INIFile tnmtIni = new INIFile(Tournament.iniPath);
        public Settings(MainWindow i_mainWindow)
        {
            InitializeComponent();
            mainWindow = i_mainWindow;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            Tournament tnmt = new Tournament();
            tnmt.Getter();

            lbl_oTnmtName.Content = tnmt.tnmtName;
            lbl_oRunCnt.Content = tnmt.tnmtRunCnt;
            lbl_oGamePerRunCnt.Content = tnmt.tnmtGameProRunCnt;
        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void btn_EditTnmtSettings_Click(object sender, RoutedEventArgs e)
        {
            SwitchEditMode(true);
            tbx_iTnmtName.Text = Convert.ToString(lbl_oTnmtName.Content);
            tbx_iRunCnt.Text = Convert.ToString(lbl_oRunCnt.Content);

        }

        private void btn_EditTnmtSettings_Save_Click(object sender, RoutedEventArgs e)
        {
            Tournament tnmt = new Tournament();
            tnmt.Getter();
            bool switchDir = false;
            string oldPath = "";

            if (tnmt.tnmtName != tbx_iTnmtName.Text)
            {
                switchDir = true;

                oldPath = tnmtIni.GetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath);
            }
            tnmt.tnmtName = tbx_iTnmtName.Text;
            tnmt.tnmtRunCnt = Convert.ToInt32(tbx_iRunCnt.Text);
            tnmt.Setter();

            Log.Update("Tnmt-Name: " + lbl_oTnmtName.Content + "|" + tnmt.tnmtName);
            Log.Update("Tnmt-RunCnt: " + lbl_oRunCnt.Content + "|" + Convert.ToString(tnmt.tnmtRunCnt));

            if (switchDir)
            {
                string specificTnmntPath = System.IO.Path.Combine(Const.CurDirPath, tbx_iTnmtName.Text);
                try
                {
                    tnmtIni.SetValue(Const.fileSec, Tournament.fsX_SpecTnmtPath, specificTnmntPath);

                    Directory.Move(oldPath, specificTnmntPath);
                    Log.Update("Move " + oldPath + " after Tnmnt-Name Update to " + specificTnmntPath);
                    Directory.Delete(oldPath);
                    Log.Delete("Old Data after Tnmt-Name Update " + oldPath);
                } catch
                {
                    Log.Error("Switching Tournament-Folder failed! Old Path:" + oldPath + " | new Path:" + specificTnmntPath);
                    //MessageBox.Show("Bei der Änderung des Turniernamens kam es zu einem Fehler!" +
                    //                "\nBitte überprüfen Sie die Ordner:\n" + oldPath + "\n -> \n" + specificTnmntPath,
                    //                "Fehler bei Änderung des Turniernamens",
                    //                MessageBoxButton.OK,
                    //                MessageBoxImage.Error);
                    mainWindow.MessageBar(MainWindow.ErrorMessage,
                                           "Fehler bei Änderung des Turniernamens",
                                           "Bei der Änderung des Turniernamens kam es zu einem Fehler!" +
                                           "\nBitte überprüfen Sie die Ordner:\n" + oldPath + "\n -> \n" + specificTnmntPath);
                }
            }

            Settings_Loaded(sender, e);
            SwitchEditMode(false);
        }

        private void btn_EditTnmtSettings_Clear_Click(object sender, RoutedEventArgs e)
        {
            Settings_Loaded(sender, e);
            SwitchEditMode(false);
        }

        private void SwitchEditMode (bool value)
        {
            Visibility a = new Visibility();
            Visibility b = new Visibility();
            if (value)
            {
                a = Visibility.Hidden;
                b = Visibility.Visible;
            } else
            {
                a = Visibility.Visible;
                b = Visibility.Hidden;
            }
            lbl_oTnmtName.Visibility = a;
            lbl_oRunCnt.Visibility = a;
            lbl_oGamePerRunCnt.Visibility = a;
            tbx_iTnmtName.Visibility = b;
            tbx_iRunCnt.Visibility = b;
            btn_EditTnmtSettings_Save.Visibility = b;
            btn_EditTnmtSettings_Clear.Visibility = b;
            btn_EditTnmtSettings.Visibility = a;
        }

        private void btn_EditPassword_Click(object sender, RoutedEventArgs e)
        {
            Window changePassword = new ChangePassword();
            changePassword.Show();
        }

        private void btn_EditColorGreen_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Background = Settings.BackgroundSetUp(Const.Green.green1, Const.Green.green2, Const.Green.green3);
            SwitchColorStyleActionMenue(mainWindow, Const.Green.color);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_ColorMode, Const.Green.color);
        }

        private void btn_EditColorRed_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Background = Settings.BackgroundSetUp(Const.Red.red1, Const.Red.red2, Const.Red.red3);
            SwitchColorStyleActionMenue(mainWindow, Const.Red.color);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_ColorMode, Const.Red.color);
        }

        public void btn_EditColorBlue_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Background = Settings.BackgroundSetUp(Const.Blue.blue1, Const.Blue.blue2, Const.Blue.blue3);
            SwitchColorStyleActionMenue(mainWindow, Const.Blue.color);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_ColorMode, Const.Blue.color);
        }
        
        private void btn_EditColorGray_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Background = Settings.BackgroundSetUp(Const.Gray.gray1, Const.Gray.gray2, Const.Gray.gray3);
            SwitchColorStyleActionMenue(mainWindow, Const.Gray.color);
            tnmtIni.SetValue(Const.fileSec, Tournament.fsX_ColorMode, Const.Gray.color);
        }

        #endregion

        #region Utilitiy - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static LinearGradientBrush BackgroundSetUp(Color c1, Color c2, Color c3)
        {
            LinearGradientBrush lgbrush = new LinearGradientBrush();
            lgbrush.StartPoint = new Point(0.5, 0);
            lgbrush.EndPoint = new Point(0.5, 1);
            lgbrush.GradientStops.Add(new GradientStop(c1, 0.513));
            lgbrush.GradientStops.Add(new GradientStop(c2, 1));
            lgbrush.GradientStops.Add(new GradientStop(c3, 0));

            return lgbrush;
        }

        public static void SwitchColorStyleActionMenue(MainWindow i_mainWindow, string i_ColorMode)
        {
            for (int i = 0; i < i_mainWindow.actionMenueButton.Length; i++)
            {
                if (i_mainWindow.actionMenueButton[i].Uid != i_mainWindow.NO_TEAM_ADDING)
                {
                    i_mainWindow.actionMenueButton[i].Style = (Style)Application.Current.Resources["ActionMenueButton_" + i_ColorMode];
                } else
                {
                    i_mainWindow.actionMenueButton[i].Style = (Style)Application.Current.Resources["DisabledButton"];
                }
            }
        }

        public static void SwitchColorStyleDefaultButton(Button[] i_btn)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            string colorMode = tnmtIni.GetValue(Const.fileSec, Tournament.fsX_ColorMode);
            for (int i = 0; i < i_btn.Length; i++)
            {
                i_btn[i].Style = (Style)Application.Current.Resources["DefaultIAButton_" + colorMode];
            }
        }
        #endregion 
    }
}
