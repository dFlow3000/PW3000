using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

namespace Preiswattera_3000
{
    class InfoWindowContent
    {
        public Dictionary<string, string> InfoWindowText;
        public InfoWindowContent()
        {
            InfoWindowText = new Dictionary<string, string>();
        }

        public void FillInfoWindow(Dictionary<string,string> i_dic)
        {
            InfoWindow window = new InfoWindow();
            foreach (KeyValuePair<string, string> info in i_dic)
            {
                TextBlock txbl_info = new TextBlock();
                Thickness SpecialMargin = txbl_info.Margin;
                txbl_info.Text = info.Key;

                switch (info.Value)
                {
                    case InfoStyles.HeaderStyle:
                        txbl_info.HorizontalAlignment = HorizontalAlignment.Center;
                        txbl_info.FontStyle = FontStyles.Oblique;
                        txbl_info.FontWeight = FontWeights.Heavy;
                        txbl_info.FontSize = 24;
                        SpecialMargin.Bottom = 5;
                        txbl_info.Margin = SpecialMargin;
                        window.stp_InfoHeader.Children.Add(txbl_info);
                        break;
                    case InfoStyles.ParaHeader:
                        txbl_info.HorizontalAlignment = HorizontalAlignment.Left;
                        txbl_info.FontStyle = FontStyles.Normal;
                        txbl_info.FontWeight = FontWeights.Bold;
                        txbl_info.FontSize = 20;
                        SpecialMargin.Top = 10;
                        SpecialMargin.Left = 2;
                        SpecialMargin.Bottom = 4;
                        txbl_info.Margin = SpecialMargin;
                        window.stp_InfoStack.Children.Add(txbl_info);
                        break;
                    case InfoStyles.ParaContentKey:
                        txbl_info.HorizontalAlignment = HorizontalAlignment.Left;
                        txbl_info.FontStyle = FontStyles.Normal;
                        txbl_info.FontWeight = FontWeights.SemiBold;
                        txbl_info.FontSize = 16;
                        SpecialMargin.Left = 10;
                        SpecialMargin.Bottom = 4;
                        SpecialMargin.Top = 8;
                        txbl_info.Margin = SpecialMargin;
                        window.stp_InfoStack.Children.Add(txbl_info);
                        break;
                    case InfoStyles.ParaContentValue:
                        txbl_info.HorizontalAlignment = HorizontalAlignment.Left;
                        txbl_info.FontStyle = FontStyles.Normal;
                        txbl_info.FontWeight = FontWeights.Normal;
                        txbl_info.FontSize = 16;
                        SpecialMargin.Left = 12;
                        SpecialMargin.Bottom = 2;
                        txbl_info.Margin = SpecialMargin;
                        window.stp_InfoStack.Children.Add(txbl_info);
                        break;
                    default: break;
                }
            }
            window.Show();
        }
    }

    public class InfoStyles
    {
        public const string HeaderStyle = "HeaderStyle";
        public const string ParaHeader = "ParaHeader";
        public const string ParaContentKey = "ParaContentKey";
        public const string ParaContentValue = "ParaContentValue";
    }
}
