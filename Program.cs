using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace DbHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            InitPricesIfNotExists();

            using (var db = new HotelContext())
            {
                // удаляем все строки в таблице
                foreach (var room in db.Rooms)
                {
                    db.Rooms.Remove(room);
                }
                // создаем комнаты и сохраняем в бд
                List<ConveniencesPrice> prices = db.ConveniencesPrices.ToList();
                Room room1 = new Room
                {
                    RoomNumber = 100,
                    Capacity = 1,
                    IsRoomAvalible = true,
                    ConveniencesPrice = prices[0]
                };
                Room room2 = new Room
                {
                    RoomNumber = 101,
                    Capacity = 2,
                    IsRoomAvalible = true,
                    ConveniencesPrice = prices[1]
                };
                Room room3 = new Room
                {
                    RoomNumber = 102,
                    Capacity = 3,
                    IsRoomAvalible = false,
                    ConveniencesPrice = prices[1]
                };
                Room room4 = new Room
                {
                    RoomNumber = 103,
                    Capacity = 2,
                    IsRoomAvalible = true,
                    ConveniencesPrice = prices[0]
                };
                Room room5 = new Room
                {
                    RoomNumber = 104,
                    Capacity = 3,
                    IsRoomAvalible = true,
                    ConveniencesPrice = prices[0]
                };
                Room room6 = new Room
                {
                    RoomNumber = 105,
                    Capacity = 2,
                    IsRoomAvalible = true,
                    ConveniencesPrice = prices[2]
                };


                db.AddRange(room1, room2, room3, room4, room5, room6);

                db.SaveChanges();

                // получаем объекты из бд и выводим на консоль
                var rooms = db.Rooms.ToList();
                Console.WriteLine("Rooms list:");
                foreach (Room r in rooms)
                {
                    Console.WriteLine($"Id : {r.Id}" +
                        $" Номер комнаты: {r.RoomNumber} " +
                        $"Цена за сутки : {r.ConveniencesPrice.Price} " +
                        $"Тип занятости: {r.IsRoomAvalible}" +
                        $" Вместимость : {r.Capacity}");
                }

                foreach (var room in db.Rooms.Where(r => r.IsRoomAvalible))
                {
                    Console.WriteLine("Номер комнаты: {0}, класс: {1}", room.RoomNumber, room.ConveniencesPrice.Convenience);
                }
            }

        }

        static void CreateDatabase()
        {
            new HotelContext().CreateDbIfNotExist();
        }

        static void InitPricesIfNotExists()
        {
            using (var hotelContext = new HotelContext())
            {
                if (hotelContext.ConveniencesPrices.ToList().Count() == 0)
                {
                    var EconomPrice = new ConveniencesPrice()
                    {
                        Convenience = ConveniencesEnumeration.ECONOM,
                        Price = 1500
                    };
                    var BusinessPrice = new ConveniencesPrice()
                    {
                        Convenience = ConveniencesEnumeration.BUSINESS,
                        Price = 3500
                    };
                    var LuxaryPrice = new ConveniencesPrice()
                    {
                        Convenience = ConveniencesEnumeration.LUXARY,
                        Price = 6500
                    };

                    hotelContext.ConveniencesPrices.AddRange(EconomPrice, BusinessPrice, LuxaryPrice);
                    hotelContext.SaveChanges();
                }
            }
        }
    }
}
