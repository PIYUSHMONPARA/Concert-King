using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert_King
{
    public class SeatClass : SeatInfo
    {
        public List<SeatInfo> seatInfo { get; set; }
        public SeatClass()
        {
            seatInfo = new List<SeatInfo>();
        }

    }
}
