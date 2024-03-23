using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTool
{
    public class EVT
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StructEVTHeader
        {
            public byte majorRev;
            public byte minorRev;
            public byte release;
            public byte prerelease;
            public uint csisOffset;
            public byte projectId;
            public byte datId;
            public byte bolloRev;
            public byte csisResolved;
            public ushort saveIncrement;
            public ushort generateID;
            public ushort numEvents;
            public byte numGlobalMatchParms;
            public byte pad1;
            public ushort eventFilterLength;
            public ushort eventFilterPriority;
        }
    }
}
