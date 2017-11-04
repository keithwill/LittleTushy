using System;
using System.Collections.Generic;
using System.Text;

namespace LittleTushy
{
    internal static  class StringExtensionMethods
    {

        internal static byte[] ToUTF8Bytes(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}