using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nocksoft.IO.ConfigFiles;
namespace PW
{
    class Crypto
    {
        private static char[] cryp = new char[26] { '^', 'ß', '!', '"', '+', '$', '%', '&', '/', '(',
                                 ')', '>', '°', '?', '*', '§', '~', '{', '-', '_',
                                 '.', ';', ',', ':', '#', ']'};
        private static char[] comp = new char[26] {'a','b','c','d','e','f','g','h','i','j',
                                 'k','l','m','n','o','p','q','r','s','t',
                                 'u','v','w','x','y','z'};
        
        public static bool Encrypt (string i_string)
        {
            INIFile tnmtIni = new INIFile(Tournament.iniPath);
            Random rnd = new Random();
            string tryString = tnmtIni.GetValue(Const.fileSec, Tournament.fsX_allKey); 
            char[] i_charArray = i_string.ToLower().ToCharArray();
            char[] o_charArray = new char[i_charArray.Length];

            for( int i = 0; i < i_charArray.Length; i++)
            {
                for(int y = 0; y < comp.Length; y++)
                {
                    if (i_charArray[i] == comp[y])
                    {
                        o_charArray[i] = cryp[y];
                        break;
                    }
                }
            }

            i_string = "";
            foreach(char c in o_charArray)
            {
                i_string += c;
                i_string += cryp[rnd.Next(25)];
            }

            i_charArray = tryString.ToCharArray();
            o_charArray = i_string.ToCharArray();
            if (tryString.Length == i_string.Length) {
                for (int i = 0; i < i_charArray.Length; i++) {
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

    }
}
