using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SpeechTool
{
    public class CSI
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StructCSIHeader
        {
            public uint Signature;              // 4 bytes
            public byte FileFormatVersion;      // 1 byte
            public byte CSISXLibraryMajorVersion; // 1 byte
            public byte CSISXLibraryMinorVersion; // 1 byte
            public byte CSISXLibraryPatchVersion; // 1 byte
            public byte Platform;               // 1 byte
            public byte ResolvedFlag;           // 1 byte
            public ushort FunctionsCount;       // 2 bytes
            public ushort ClassesCount;         // 2 bytes
            public ushort GlobalVariablesCount; // 2 bytes
            public ushort SystemLevelCRC;       // 2 bytes
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Padding;              // 2 bytes
            public uint FunctionsPointer;       // 4 bytes
            public uint ClassesPointer;         // 4 bytes
            public uint GlobalVariablesPointer; // 4 bytes
            public uint NextEntryPointer;       // 4 bytes
            public uint PreviousEntryPointer;   // 4 bytes

            // Total: 40 bytes
        }
    }
}
