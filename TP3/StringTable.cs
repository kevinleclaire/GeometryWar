using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3
{
    /// <summary>
    /// Pour determiner les strings utilisé dans l'interface en anglais ou en francais
    /// </summary>
    public class StringTable
    {
        /// <summary>
        /// Class implementer en Singleton
        /// </summary>
        static private StringTable instance = null;
        /// <summary>
        /// Dictionnaire qui contiennent les strings en francais ou en anglais
        /// </summary>
        private static Dictionary<string, string> dictFrancais = new Dictionary<string, string>();
        private static Dictionary<string, string> dictAnglais = new Dictionary<string, string>();
        public static List<string> text = new List<string>(File.ReadAllLines(@"Data/st.txt"));
        /// <summary>
        /// Pour obtenir la seul instance de la StringTable
        /// </summary>
        /// <returns></returns>
        static public StringTable GetInstance()
        {
            if (instance == null)
            {
                instance = new StringTable();
                foreach (String str in text)
                {
                    dictFrancais.Add(str.Substring(0, str.IndexOf("=")), str.Substring(str.IndexOf(">") + 1, str.IndexOf("-") - (str.IndexOf(">") + 1)));
                    dictAnglais.Add(str.Substring(0, str.IndexOf("=")), str.Substring(str.LastIndexOf("-") + 1, str.Count() - (str.LastIndexOf("-") + 1)));
                }
            }
            return instance;
        }
        /// <summary>
        /// Methode pour obtenir la valeur selon la langue et la clef 
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(Language lang, string key)
        {
            if (lang == Language.French)
            {
                return dictFrancais[key];
            }
            else
            {
                return dictAnglais[key];
            }
        }
        /// <summary>
        /// Methode pour verifier que la string utilisée est correct
        /// </summary>
        /// <param name="textUtilise"></param>
        /// <returns></returns>
        public ErrorCode Parse(string textUtilise)
        {
            if(textUtilise.Contains("==>") && textUtilise.Contains("---") && textUtilise.Contains("ID_TOTAL_TIME") && textUtilise.Contains("ID_LIFE"))
            {
                return ErrorCode.OK;
            }
            else
            {
                return ErrorCode.MISSING_FIELD;
            }
  
        }
    }
}
