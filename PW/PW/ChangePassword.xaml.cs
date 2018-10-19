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
        public ChangePassword()
        {
            InitializeComponent();
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
                MessageBox.Show("Das Passwort wurde erfolgreich geändert!",
                "Passwort geändert",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                tbx_iOldPassword.Password = String.Empty;
                tbx_iNewPassword.Password = String.Empty;
                Close();
                Log.Update("Password successfully changed!");
            } else
            {
                MessageBox.Show("Das alte Passwort ist nicht korrekt!",
                                "Passwort ändern nicht möglich",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
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
    }
}
