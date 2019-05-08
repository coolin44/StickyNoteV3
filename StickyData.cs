using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StickyNote
{
    /// <summary>
    /// This class is used to hadle the data of the list of sticky notes.
    /// </summary>
    class StickyData : StickyParent
    {
        private int FileLength;
        private char[] Seperator;
        private ArrayList StickyNoteList;

        /// <summary>
        /// Simple constructor to initialize and set the class' attributes.
        /// </summary>
        public StickyData()
        {
            this.FileLength = FindFileLength();
            this.Seperator = FindSeperator();
            this.StickyNoteList = new ArrayList();
        }

        /// <summary>
        /// Used to add a sticky note to the list of sticky notes.
        /// </summary>
        /// <param name="stickyNote"></param>
        public void AddStickyNoteToList(StickyNote stickyNote)
        {
            StickyNoteList.Add(stickyNote);
        }

        /// <summary>
        /// Used to find the amount of hexadecimal bits that are in the file after the first 4 bits.
        /// </summary>
        /// <returns>Length of the file in terms of hexadecimal bits</returns>
        public int FindFileLength()
        {
            string lengthInHex = "";
            for (int i = 2; i < 10; i++)
            {
                lengthInHex += array[i];
            }
            int length = Convert.ToInt32(StickyNote.DecodeHexToString(lengthInHex));
            return length;
        }

        /// <summary>
        /// Used to find the seperator of the file.
        /// </summary>
        /// <returns>Seperator of the file</returns>
        public static char[] FindSeperator()
        {
            char[] seperator = new char[2];
            seperator[0] = array[10];
            seperator[1] = array[11];
            return seperator;
        }

        /// <summary>
        /// Used to return the length of the file in terms of hexadecimal bits.
        /// </summary>
        /// <returns>Length of the file in terms of hexadecimal bits</returns>
        public int GetFileLength()
        {
            return this.FileLength;
        }

        /// <summary>
        /// Used to get the seperator of the file
        /// </summary>
        /// <returns>Seperator of the file</returns>
        public char[] GetSeperator()
        {
            return this.Seperator;
        }

        /// <summary>
        /// Used to get the list of sticky notes
        /// </summary>
        /// <returns>List of all of the sticky notes</returns>
        public ArrayList GetStickyNoteList()
        {
            return this.StickyNoteList;
        }
    }
}
