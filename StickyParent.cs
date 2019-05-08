using System.Collections.Generic;
using System.Text;

namespace StickyNote
{
    /// <summary>
    /// This class is used to provide the common attributes between the StickyData and StickyNote classes
    /// to both of them.
    /// </summary>
    class StickyParent
    {
        protected static char[] array;

        /// <summary>
        /// Used to set the character array of the input file for child classes.
        /// </summary>
        /// <param name="byteStream">Byte-stream of the input file</param>
        public static void setArray(string byteStream)
        {
            array = byteStream.ToCharArray();
        }
    }
}
