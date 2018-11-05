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
    /// Interaktionslogik für ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        #region Message-Bar
        #region Message Types
        public const int ErrorMessage = 1000;
        public const int InfoMessage = 2000;
        public const int WarnMessage = 3000;
        public const int AskMessage = 4000;
        #endregion
        public bool isClicked = false;
        public bool selection = false;
        #endregion

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Loaded(object sender, RoutedEventArgs e)
        {
            Button[] iA_Btn =
            {
                btn_Save,
                btn_Clear
            };
            Settings.SwitchColorStyleDefaultButton(iA_Btn);
            Const.SwitchColor(this);
        }

        #region Button - Function ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Saves new password after checking the actual one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);

            if (Crypto.Encrypt(tbx_iOldPassword.Password.ToString()))
            {
                string newPassword = Crypto.Decrypt(tbx_iNewPassword.Password.ToString());
                tnmtIni.SetValue(Const.fileSec, Tournament.fsX_allKey, newPassword);
                //MessageBox.Show("Das Passwort wurde erfolgreich geändert!",
                //"Passwort geändert",
                //MessageBoxButton.OK,
                //MessageBoxImage.Information);
                MessageBar(InfoMessage,
                            "Passwort geändert",
                            "Das Passwort wurde erfolgreich geändert!");
                btn_Ok.Uid = "InfoMessage";
                tbx_iOldPassword.Password = String.Empty;
                tbx_iNewPassword.Password = String.Empty;
                Log.Update("Password successfully changed!");
            } else
            {
                //MessageBox.Show("Das alte Passwort ist nicht korrekt!",
                //                "Passwort ändern nicht möglich",
                //                MessageBoxButton.OK,
                //                MessageBoxImage.Error);
                MessageBar(ErrorMessage,
                            "Passwortänderung nicht möglich",
                            "Das alte Passwort ist nicht korrekt!");
                btn_Ok.Uid = "ErrorMessage";
                tbx_iOldPassword.Password = String.Empty;
                tbx_iNewPassword.Password = String.Empty;
                Log.Update("Password-Change failed!");
            }

        }

        /// <summary>
        /// Closes the password change window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Message - Function +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public bool MessageBar(int i_MessageType, string i_MessageHeader, string i_MessageText)
        {
            bool retVal = true;
            cnvs_MessageBar.Visibility = Visibility.Visible;
            lbl_oMessageHeader.Content = i_MessageHeader;
            lbl_oMessageText.Content = i_MessageText;
            switch (i_MessageType)
            {

                case ErrorMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF4D4D"));
                    bar_ErroImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case InfoMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFAFE6FF"));
                    bar_InfoImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                case WarnMessage:
                    rec_Bar.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFAF"));
                    bar_WarnImg.Visibility = Visibility.Visible;
                    btn_Ok.Visibility = Visibility.Visible;
                    break;
                default: break;
            }
            return retVal;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;
            cnvs_MessageBar.Visibility = Visibility.Hidden;
            btn_Ok.Visibility = Visibility.Hidden;
            if (thisButton.Uid == "InfoMessage")
            {
                Close();
            };
            thisButton.Uid = String.Empty;
        }
        #endregion
    }
}
