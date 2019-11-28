using System;
using System.Collections.Generic;
using System.Text;

namespace CoreMoveFiles
{
    public interface IMoveLocation
    {
        public string SourceLocation { get; set; }
        public string DestinationLocation { get; set; }

    }
}
