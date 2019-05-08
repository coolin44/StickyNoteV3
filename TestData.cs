using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace StickyNote
{
    /// <summary>
    /// This class is used to load the input file and convert it into a byte-stream.
    /// </summary>
    class TestData
    {
        private string ByteStream;
        private const string PATH = "D:\\Users\\whubert\\Desktop\\Data.txt";

        /// <summary>
        /// Constructer to initialize and set the class' attributes.
        /// </summary>
        public TestData()
        {
            this.ByteStream = GetDataFromFile();
        }

        /// <summary>
        /// Used to retrieve the data from the input file.
        /// </summary>
        /// <returns></returns>
        public static string GetDataFromFile()
        {
            string data;
            using (StreamReader streamReader = new StreamReader(TestData.PATH, Encoding.UTF8))
            {
                data = streamReader.ReadToEnd();
            }
            return data;
        }

        /// <summary>
        /// Used to return the byte-stream.
        /// </summary>
        /// <returns>Byte-stream of the data from the input file</returns>
        public string getByteStream()
        {
            return this.ByteStream;
        }
    }
}
