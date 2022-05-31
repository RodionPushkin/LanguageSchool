using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langulag.Classes
{
    class AppData
    {
        public static EF.Entities Context { get; } = new EF.Entities();
        public static string GetLetters(string text)
        {
            return new String(text.ToCharArray().Where(n => !char.IsDigit(n) && char.IsLetter(n)).ToArray());
        }
        public static string GetNumbers(string text)
        {
            return new String(text.ToCharArray().Where(n => char.IsDigit(n) && !char.IsLetter(n)).ToArray());
        }
    }
}
