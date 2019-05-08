using System;
using System.Collections.Generic;
using System.Text;

namespace StickyNote
{
    /// <summary>
    /// This class is used to handle the data of each individual sticky note.
    /// </summary>
    class StickyNote : StickyParent
    {
        private string UserID;
        private DateTime createdAt;
        private int PageNumber;
        private string MetaData;
        private string Data;
        private static int year, month, day, hour, minute, second, millisecond;
        private static int index;

        /// <summary>
        /// Simple constructor to initialize and set the class' attibutes.
        /// </summary>
        /// <param name="id">Sticky note's UserID</param>
        /// <param name="dt">Sticky note's 'CreatedAt' DateTime</param>
        /// <param name="pgNum">The page number the sticky note was created on</param>
        /// <param name="mData">Sticky note's 'MetaData'</param>
        /// <param name="data">The text that was written on the sticky note</param>
        public StickyNote(string id, DateTime dt, int pgNum,string mData, string data)
        {
            this.UserID = id;
            this.createdAt = dt;
            this.PageNumber = pgNum;
            this.MetaData = mData;
            this.Data = data;
        }

        /// <summary>
        /// Used to set the sticky note's MetaData and return it to main.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>Sticky note's MetaData</returns>
        public static string SetMetaData(int i)
        {
            string mData = "";
            for(int j = 0; j < 32; i++, j++)
            {
                mData += array[i];
            }
            return mData;
        }

        /// <summary>
        /// Used to check if any data(text) actually exists inside the sticky note.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <param name="userId">UserID of the sticky note</param>
        /// <param name="seperator">Sticky note's seperator</param>
        /// <returns>True if data actually exists in the sticky note, false otherwise</returns>
        public static bool DataExistsInNote(int i, string userId, char[] seperator)
        {
            string temp = "";
            i = i - 4;
            int k = i - 8;
            for (int j = 0; j < (userId.Length * 2); i++, j++)
            {
                temp += array[i];
            }
            temp = DecodeHexToString(temp);
            //if no data exists, then the userID will be read in creating a new sticky note
            if (temp == userId)
            {
                return false;
            }
            //if no data exists there could just be null values remaining in the byteStream
            else if (NextByteIsNullValue(i))
                return false;
            //if no data exists, a new userId could be being read
            else if(NextByteIsSeperator(k, seperator))
            {
                return false;
            }
            //if all tests fail then there must be data to be read in
            return true;
        }

        /// <summary>
        /// Used to set the data(text) of the sticky note.
        /// </summary>
        /// <param name="seperator">Sticky note's seperator</param>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <param name="numberOfCharsInFile">Number of hexadecimal characters in the file</param>
        /// <returns>Data stored in the sticky note</returns>
        public static string SetData(char[] seperator, int i, int numberOfCharsInFile)
        {
            string dataInHex = "";
            //while there are still characters in the file that are not null or the seperator they should be read in
            //as the data of the sticky note
            while (i < numberOfCharsInFile + 1 && !NextByteIsSeperator(i, seperator) && !NextByteIsNullValue(i))
            {
                dataInHex += array[i];
                i++;
            }
            //used to keep track of the current index
            index = i;
            string data = DecodeHexToString(dataInHex);
            return data;
        }

        /// <summary>
        /// Used to check if the next byte to be read in is a null value.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>True if the next byte is a null value, false otherwise</returns>
        public static bool NextByteIsNullValue(int i)
        {
            //2020 could exist in a hex string and not be null, however 202020 must be null
            if (array[i] == '2' && array[i + 1] == '0')
            {
                if (array[i + 2] == '2' && array[i + 3] == '0')
                {
                    if(array[i+4] == '2' && array[i+5] == '0')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Used to check if only null characters remain in the hexadecimal string.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <param name="numberOfCharsInFile">Number of hexadecimal characters in the file</param>
        /// <returns>True if the only remaining charactes are of null value, false otherwise</returns>
        public static bool RemainingCharactersNull(int i, int numberOfCharsInFile)
        {
            for(int j = i; j < numberOfCharsInFile; j++)
            {
                if (array[j] != '2' || array[j] != '0')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Used to retrieve the index back to main when finished reading data.
        /// </summary>
        /// <returns>Current index of the hexadecimal file</returns>
        public static int getIndexWhenFinishedReadingData()
        {
            return index;
        }

        /// <summary>
        /// Used to check if the next byte to be read in is the seperator.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <param name="seperator">Sticky note's seperator</param>
        /// <returns>True if the next byte to be read in is the sticky note's seperator, false otherwise</returns>
        public static bool NextByteIsSeperator(int i, char[] seperator)
        {
            if (array[i] == seperator[0])
            {
                if (array[i + 1] == seperator[1])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Used to set the sticky note's UserID.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>Sticky note's UserID</returns>
        public static string SetUserId(int i)
        {
            string userId = "";
            for (int j = 0; j < 24; i++, j++)
            {
                switch (array[i])
                {
                    //if 0 is read in and the value is even and greater than 1 then the complete userID is already read in
                    case '0':
                        if(j % 2 == 0 && j > 1)
                        {
                            return DecodeHexToString(userId);
                        }
                        else
                        {
                            userId += array[i];
                            break;
                        }
                    default:
                        userId += array[i];
                        break;
                }
            }
            return DecodeHexToString(userId);
        }

        /// <summary>
        /// Used to retrieve and set the CreatedAt DateTime of the sticky note.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>Sticky note's CreatedAt DateTime</returns>
        public static DateTime GetCreatedAtTime(int i)
        {
            string dateTimeInHex = "";
            for (int j = 0; j < 52; i++, j++)
            {
                dateTimeInHex += array[i];
            }
            string dateTime = DecodeHexToString(dateTimeInHex);
            DateTime dt = setDateTime(dateTime);
            return dt;
        }

        /// <summary>
        /// Used to set the sticky note's CreatedAt DateTime through the method above.
        /// </summary>
        /// <param name="dateTime">Hexadecimal string of sticky note's CreatedAt DateTime</param>
        /// <returns>Sticky note's CreatedAt DateTime</returns>
        public static DateTime setDateTime(string dateTime)
        {
            int count = 0;
            char[] dateTimeArray = dateTime.ToCharArray();
            string tempString = "";
            for (int i = 0; i < dateTime.Length; i++)
            {
                switch (count)
                {
                    case 0:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            year = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 1:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            month = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 2:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            day = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 3:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            hour = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 4:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            minute = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 5:
                        tempString += dateTimeArray[i];
                        if (NextCharNotNumber(dateTimeArray, i))
                        {
                            second = Convert.ToInt32(tempString);
                            tempString = "";
                            count++;
                            i++;
                        }
                        break;
                    case 6:
                        tempString += dateTimeArray[i];
                        if (NextCharNull(dateTimeArray, i))
                        {
                            millisecond = Convert.ToInt32(tempString);
                            //DateTime constructor can only take milliseconds between 0 and 999
                            while (millisecond > 999)
                            {
                                millisecond = millisecond / 10;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            DateTime dt = new DateTime(year, month, day, hour, minute, second, millisecond);
            return dt;
        }

        /// <summary>
        /// Used to find the pageNumber of the sticky note and return it back to main.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>PageNumber of the sticky note</returns>
        public static int SetPageNumber(int i)
        {
            string pageNumberAsString = "";
            for (int j = 0; j < 2; j++, i++)
            {
                pageNumberAsString += array[i];
            }
            try
            {
                int pageNumber = Convert.ToInt32(pageNumberAsString);
                return pageNumber;
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Format Exception caught setting Page Number");
                Console.WriteLine("Page Number auto set to 1");
                int pageNumber = 1;
                return pageNumber;
            }
        }

        /// <summary>
        /// Used to return the UserID to main.
        /// </summary>
        /// <returns>Sticky note's UserID</returns>
        public string GetUserID()
        {
            return this.UserID;
        }

        /// <summary>
        /// Used to return the sticky note's CreatedAt DateTime to main.
        /// </summary>
        /// <returns>Sticky note's CreatedAt DateTime</returns>
        public DateTime GetCreatedAt()
        {
            return this.createdAt;
        }

        /// <summary>
        /// Used to return the sticky note's pageNumber to main.
        /// </summary>
        /// <returns>Sticky note's pageNumber</returns>
        public int GetPageNumber()
        {
            return this.PageNumber;
        }

        /// <summary>
        /// Used to return the sticky note's MetaData to main.
        /// </summary>
        /// <returns>Sticky note's MetaData</returns>
        public string GetMetaData()
        {
            return this.MetaData;
        }

        /// <summary>
        /// Used to return the sticky note's data(text) to main
        /// </summary>
        /// <returns>Sticky note's data</returns>
        public string GetData()
        {
            return this.Data;
        }

        /// <summary>
        /// Used to check the next character when setting the sticky note's CreatedAt DateTime.
        /// </summary>
        /// <param name="dateTimeArray">Hexadecimal string of the DateTime</param>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>True if the next character to be read in is not a number, false otherwise</returns>
        public static bool NextCharNotNumber(char[] dateTimeArray, int i)
        {
            if (dateTimeArray[i + 1] == '-' || dateTimeArray[i + 1] == '.')
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used for the last field being read in through the setDateTime method.
        /// </summary>
        /// <param name="dateTimeArray">Hexadecimal string of the DateTime</param>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>True if the next character to be read in is null, false otherwise</returns>
        public static bool NextCharNull(char[] dateTimeArray, int i)
        {
            if (i == dateTimeArray.Length - 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used to check when to stop reading in the sticky note's UserID from the hexadecimal file.
        /// </summary>
        /// <param name="i">Current index of the hexadecimal file</param>
        /// <returns>True if the next character to be read in is of null value, false otherwise</returns>
        public static bool NextCharNullValue(int i)
        {
            if (array[i + 1] == '0' || array[i + 1] == '3')
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used to decode a hexadecimal string into a text string.
        /// </summary>
        /// <param name="hexString">Hexadecimal string</param>
        /// <returns>Decoded hexadecimal string</returns>
        public static string DecodeHexToString(string hexString)
        {
            byte[] bytes = HexStringToBytes(hexString);
            string decoded = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
            return decoded;
        }

        /// <summary>
        /// Used to convert the origional hexadecimal string into a byte array.
        /// </summary>
        /// <param name="hexString">Hexadecimal string</param>
        /// <returns>Byte array of the origional input hexadecimal file</returns>
        public static byte[] HexStringToBytes(string hexString)
        {
            if (hexString == null)
                throw new ArgumentNullException("hexString");
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("hexString must have an even length", "hexString");
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }
            return bytes;
        }
    }
}
