using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nocksoft.IO.ConfigFiles;
namespace Preiswattera_3000
{
    class Crypto
    {
        #region Crypto_Alphabet ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private static char[] cryp = new char[26] { '^', 'ß', '!', '"', '+', '$', '%', '&', '/', '(', ')', '>', '°', '?', '*', '§', '~', '{', '-', '_', '.', ';', ',', ':', '#', ']'};
        private static char[] comp = new char[26] { 'p', 'b', 'c', 'f', 'h', 'l', 'z', 'j', 'k', 'm', 'n', 'o', 'q', 'a', 'r', 's', 'd', 't', 'u', 'v', 'g', 'w', 'x', 'e', 'y', 'i'};
        
        private static char[] crypNum = new char[26] { 'I', 'B', 'D', 'A', 'F', 'C', 'G', 'E', 'H', 'J',
                                                       'F', 'I', 'B', 'D', 'A', 'H', 'J',
                                                       'I', 'C', 'G', 'E', 'B', 'D', 'A', 'F', 'C'};
        private static int pFix = 25;

        private static int ConvertCharToInt(char i_char)
        {
            return i_char - '0';
        }

        #endregion

        #region Encrypt ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static bool Encrypt (string i_string)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            Random rnd = new Random();
            string tryString = tnmtIni.GetValue(Const.fileSec, Tournament.fsX_allKey); 
            char[] i_charArray = i_string.ToLower().ToCharArray();
            char[] o_charArray = new char[i_charArray.Length];
            bool noNr;

            for( int i = 0; i < i_charArray.Length; i++)
            {
                noNr = false;
                for(int y = 0; y < comp.Length; y++)
                {
                    if (i_charArray[i] == comp[y])
                    {
                        o_charArray[i] = cryp[y];
                        noNr = true;
                        break;
                    }
                }
                if (!noNr)
                {
                    o_charArray[i] = crypNum[ConvertCharToInt(i_charArray[i])];
                }

            }

            i_string = "";
            foreach(char c in o_charArray)
            {
                int rndm = rnd.Next(25);
                i_string += c;
                i_string += rndm % 2 == 0 ? cryp[rndm] : crypNum[rndm];
            }
            for(int i = 0; i < pFix; i++)
            {
                int rndm = rnd.Next(25);
                i_string += rndm % 2 == 0 ? cryp[rndm] : crypNum[rndm];
            }

            i_charArray = tryString.ToCharArray();
            o_charArray = i_string.ToCharArray();
            if (tryString.Length == i_string.Length) {
                for (int i = 0; i < i_charArray.Length - pFix; i++) {
                    if (i % 2 == 0)
                    {
                        if(i_charArray[i] != o_charArray[i])
                        {
                            return false;
                        }
                    }
                }
                return true;
            } else
            {
                return false;
            }

        }
        #endregion

        #region Decrypt ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static string Decrypt(string i_string)
        {
            Random rnd = new Random();
            char[] i_charArray = i_string.ToLower().ToCharArray();
            char[] o_charArray = new char[i_charArray.Length];
            bool noNr;
            for (int i = 0; i < i_charArray.Length; i++)
            {
                noNr = false;
                for (int y = 0; y < comp.Length; y++)
                {
                    if (i_charArray[i] == comp[y])
                    {
                        o_charArray[i] = cryp[y];
                        noNr = true;
                        break;
                    }
                }

                if (!noNr)
                {
                    o_charArray[i] = crypNum[ConvertCharToInt(i_charArray[i])];
                }
            }

            i_string = "";
            foreach (char c in o_charArray)
            {
                int rndm = rnd.Next(25);
                i_string += c;
                i_string += rndm % 2 == 0 ? cryp[rndm] : crypNum[rndm];
            }
            for(int i = 0; i < pFix; i++)
            {
                int rndm = rnd.Next(25);
                i_string += rndm % 2 == 0 ? cryp[rndm] : crypNum[rndm];
            }

            return i_string;

        }
        #endregion
    }
}
