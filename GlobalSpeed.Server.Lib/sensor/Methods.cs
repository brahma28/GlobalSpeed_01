using System.Collections;
using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace SpeedLabLogic.Util
{
    public static class Methods
    {
        public static BitArray ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
                return null;

            BitArray ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }
            }
            return ba;
        }

        

        public static long ConvertHexStringToLong(string hexString)
        {
            return Int64.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }

             


        

        public static string GetShortString(string input, int length, string endToken)
        {
            if (input.Length <= length) return input;
            return input.Substring(0, length) + endToken;
        }

        public static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^\w[\w|\.|\-]+@\w[\w|\.|\-]+\.[a-zA-Z]{2,4}$");
        }
    }
}
