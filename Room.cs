using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbHotel
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsRoomAvalible { get; set; }
        public ConveniencesPrice ConveniencesPrice { get; set; }
    }
}
