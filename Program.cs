using System;
using System.Collections;
using System.IO;

namespace StickyNote
{
    /// <summary>
    /// This class is the driver of the program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to drive the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TestData testData = new TestData();
            StickyParent.setArray(testData.getByteStream());

            StickyData stickyData = new StickyData();
            Console.WriteLine("The Length of the StickyNote file is: " + stickyData.GetFileLength());
            char[] sep = stickyData.GetSeperator();
            string seperator = "";
            foreach (char character in sep)
            {
                seperator += character;
            }
            Console.WriteLine("The Seperator in the StickyNote file is: " + seperator);
            Console.WriteLine();

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("D:\\Users\\whubert\\Desktop\\StickyNoteOutput.txt"))
            {
                file.WriteLine("FILE LENGTH: " + stickyData.GetFileLength());
                file.WriteLine("SEPERATOR: " + seperator);
                    
            }

            int currentIndex = 18;
            int numberOfCharsInFile = (stickyData.GetFileLength() * 2) + 9;

            while (currentIndex < numberOfCharsInFile && !StickyNote.RemainingCharactersNull(currentIndex, numberOfCharsInFile))
            {
                string userID = StickyNote.SetUserId(currentIndex);
                Console.WriteLine("The User ID of the Sticky Note is: " + userID);
                currentIndex += 24;
                DateTime dt = StickyNote.GetCreatedAtTime(currentIndex);
                Console.WriteLine("The DateTime of the Sticky Note is: " + dt);
                currentIndex += 60;
                int pageNumber = StickyNote.SetPageNumber(currentIndex);
                Console.WriteLine("The Page Number of the Sticky Note is: " + pageNumber);
                currentIndex += 12;
                string metaData = StickyNote.SetMetaData(currentIndex);
                Console.WriteLine("The MetaData for the Sticky Note is: " + metaData);
                currentIndex += 58;
                string data = "";
                if (StickyNote.DataExistsInNote(currentIndex, userID, sep))
                {
                    data = StickyNote.SetData(sep, currentIndex, numberOfCharsInFile);
                    Console.WriteLine("The Data in the Sticky Note is: " + data);
                    currentIndex = StickyNote.getIndexWhenFinishedReadingData();
                    currentIndex += 8;
                }
                else
                {
                    data = "STICKY NOTE LEFT BLANK";
                    Console.WriteLine(data);
                    currentIndex -= 4;
                }
                StickyNote stickyNote = new StickyNote(userID, dt, pageNumber,metaData, data);
                stickyData.AddStickyNoteToList(stickyNote);
                Console.WriteLine();
            }

            ArrayList stickyNoteList = new ArrayList();
            stickyNoteList = stickyData.GetStickyNoteList();

            foreach(StickyNote stickyNote in stickyNoteList)
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("D:\\Users\\whubert\\Desktop\\StickyNoteOutput.txt", true))
                {
                    file.WriteLine("USER ID: " + stickyNote.GetUserID());
                    file.WriteLine("CREATED AT: " + stickyNote.GetCreatedAt());
                    file.WriteLine("PAGE NUMBER: " + stickyNote.GetPageNumber());
                    file.WriteLine("META DATA: " + stickyNote.GetMetaData());
                    file.WriteLine("DATA: " + stickyNote.GetData());
                    file.WriteLine();
                }
            }
            Console.WriteLine("Press any key to exit the program...");
        }
    }
}

