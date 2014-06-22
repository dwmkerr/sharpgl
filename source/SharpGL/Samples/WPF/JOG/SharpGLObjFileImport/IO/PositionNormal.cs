using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLObjFileImport
{
    public class PositionNormal
    {
        public int Position { get; set; }
        public int NormalPosition { get; set; }

        public PositionNormal(int pos, int normPos)
        {
            Position = pos;
            NormalPosition = normPos;
        }
    }
}
