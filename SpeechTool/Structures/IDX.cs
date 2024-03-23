using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTool
{
    public class IDX
    {
        public struct StructIDXHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] Signature;            // 4 bytes
            public uint TotalFileSize;          // 4 bytes
            public uint Version;                // 4 bytes
            public uint ArchivesCount;          // 4 bytes
            public uint FilesCount;             // 4 bytes
            public uint Unknown;                // 4 bytes
            public uint OffsetToFileDescriptors;// 4 bytes
            public uint OffsetToNamesTable;     // 4 bytes

            // Total: 32 bytes
        }
    }
}
