using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianStatisticsCore.Extensions
{
    public static class StringExtensions
    {
        public static int WordCount(this String value)
        {
            int count = 0;
            int i = 0;

            //Loop until the end of the string is reached
            while (i < value.Length)
            {
                //Iterate over whitespace until a word is found
                while (i < value.Length && char.IsWhiteSpace(value[i]))
                {
                    i++;
                }

                //Iterate over the found word
                while (i < value.Length && !char.IsWhiteSpace(value[i]))
                {
                    i++;
                }

                count++;
            }

            return count;
            
        }
    }
}
