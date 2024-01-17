using CN_Converter.Common;

namespace CN_Converter.Formatters
{
    class CNCStringsFormatters
    {
        // Method to format a DEC string
        public static string FormatDecString(string inputString, char splitCharacter)
        {
            if (inputString == "N/A") return inputString;
            if (splitCharacter == '\0') return inputString;

            // Formatting the string
            switch (inputString.Length)
            {
                case 1:
                case 2:
                case 3:
                    {
                        return inputString;
                    }

                default:
                    {
                        string formattedString = inputString;
                        char firstSymbol = formattedString[0];
                        formattedString = formattedString.TrimStart('-');

                        int formattedStrLength = formattedString.Length;

                        int triadNum = formattedStrLength / 3;
                        if ((formattedStrLength % 3) == 0) triadNum--;

                        int j = formattedStrLength;
                        int k = 0;

                        string tmp1, tmp2;

                        for (int i = 0; i < triadNum; i++)
                        {
                            tmp1 = formattedString.Substring(0, j - 3);
                            tmp2 = formattedString.Substring(tmp1.Length, k + 3);

                            formattedString = tmp1 + splitCharacter + tmp2;

                            j -= 3;
                            k += 4;
                        }
                        if (firstSymbol == '-')
                        {
                            return '-' + formattedString;
                        }
                        else return formattedString;
                    }
            }
        }

        // Method to format a HEX string
        public static string FormatHexString(string inputString, HexStringGroupingOptions splitOption)
        {
            if (inputString == "N/A") return inputString;

            string formatStr = inputString.ToUpper();

            // Resulting string
            string resultingString = string.Empty;
            int j = 0;
            int zeroQty = 0;

            switch (splitOption)
            {
                case HexStringGroupingOptions.NoGrouping: return formatStr;

                case HexStringGroupingOptions.Byte:
                    {
                        // Adding a 0 if the number of symbols is not even
                        if (formatStr.Length % 2 != 0)
                        {
                            formatStr = "0" + formatStr;
                        }

                        // Number of bytes in the string
                        int bytesQty = formatStr.Length / 2;
                        for (int i = 0; i < bytesQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 2);
                            j += 2;
                            if (i != bytesQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                case HexStringGroupingOptions.Word:
                    {
                        // Filling the string with 0 if necessary
                        zeroQty = formatStr.Length % 4 != 0 ? 4 - formatStr.Length % 4 : 0;
                        formatStr = formatStr.PadLeft(formatStr.Length + zeroQty, '0');

                        // Number of words in the string
                        int wordsQty = formatStr.Length / 4;

                        for (int i = 0; i < wordsQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 4);
                            j += 4;
                            if (i != wordsQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                case HexStringGroupingOptions.DoubleWord:
                    {
                        // Filling the string with 0 if necessary
                        zeroQty = formatStr.Length % 8 != 0 ? 8 - formatStr.Length % 8 : 0;
                        formatStr = formatStr.PadLeft(formatStr.Length + zeroQty, '0');

                        // Number of double words in the string
                        int doubleWordsQty = formatStr.Length / 8;
                        j = 0;
                        for (int i = 0; i < doubleWordsQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 8);
                            j += 8;
                            if (i != doubleWordsQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                default: return string.Empty;
            }
        }

        // Method to format a BIN string
        public static string FormatBinString(string inputString, BinStrSizeOptions sizeOption)
        {
            if (inputString == "N/A") return inputString;

            string formatStr = inputString;

            string resultingString = string.Empty;
            int j = 0, zeroQty = 0;
            switch (sizeOption)
            {
                case (BinStrSizeOptions.NoGrouping): return formatStr;

                case (BinStrSizeOptions.HalfByte):
                    {
                        // Filling the string with 0 if necessary
                        zeroQty = formatStr.Length % 4 != 0 ? 4 - formatStr.Length % 4 : 0;
                        formatStr = formatStr.PadLeft(formatStr.Length + zeroQty, '0');

                        // Number of words in the string
                        int wordsQty = formatStr.Length / 4;

                        for (int i = 0; i < wordsQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 4);
                            j += 4;
                            if (i != wordsQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                case (BinStrSizeOptions.Byte):
                    {
                        // Filling the string with 0 if necessary
                        zeroQty = formatStr.Length % 8 != 0 ? 8 - formatStr.Length % 8 : 0;
                        formatStr = formatStr.PadLeft(formatStr.Length + zeroQty, '0');

                        // Number of double words in the string
                        int doubleWordsQty = formatStr.Length / 8;
                        j = 0;
                        for (int i = 0; i < doubleWordsQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 8);
                            j += 8;
                            if (i != doubleWordsQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                case (BinStrSizeOptions.Word):
                    {
                        // Filling the string with 0 if necessary
                        zeroQty = formatStr.Length % 16 != 0 ? 16 - formatStr.Length % 16 : 0;
                        formatStr = formatStr.PadLeft(formatStr.Length + zeroQty, '0');

                        // Number of double words in the string
                        int doubleWordsQty = formatStr.Length / 16;
                        j = 0;
                        for (int i = 0; i < doubleWordsQty; i++)
                        {
                            resultingString += formatStr.Substring(j, 16);
                            j += 16;
                            if (i != doubleWordsQty - 1)
                            {
                                resultingString += " ";
                            }
                        }
                        return resultingString;
                    }

                default: return string.Empty;
            }
        }
    }
}
