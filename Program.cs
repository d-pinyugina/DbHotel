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
                    IsRoomAvalible = false,
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
                    IsRoomAvalible = false,
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

                Console.WriteLine("1. Вывести список свободных комнат (номер, класс), отсортированных по стоимости аренды:");
                foreach (var room in db.Rooms.Where(r => r.IsRoomAvalible))
                {
                    Console.WriteLine("Номер комнаты: {0}, класс: {1}", room.RoomNumber, room.ConveniencesPrice.Convenience);
                }

                Console.WriteLine("2.Рассчитать ежедневную прибыль отеля:");
                decimal DailyProfit = db.Rooms
                    .Where(r => !r.IsRoomAvalible)
                    .Sum(r => r.ConveniencesPrice.Price);
                Console.WriteLine(DailyProfit);

                Console.WriteLine("3.Рассчитать загруженность отеля (отношение числа сданных к общему числу комнат):");
                float TotalRoomsAmount = db.Rooms.Count();
                float OccupiedRooms = db.Rooms
                    .Where(r => !r.IsRoomAvalible)
                    .Count();
                float HotelOccupied = (OccupiedRooms / TotalRoomsAmount) * 100;
                Console.WriteLine(HotelOccupied + "%");

                Console.WriteLine("4.Вывести пары: {класс, общее число комнат}:");
                Dictionary<ConveniencesEnumeration, int> dictionary = db.Rooms
                    .AsEnumerable()
                    .GroupBy(r => r.ConveniencesPrice.Convenience)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var entry in dictionary)
                {
                    Console.WriteLine("Класс: {0}, Кол-во комнат: {1}", entry.Key, entry.Value);
                }

                Console.WriteLine("5.Извлечь самую дорогую свободную комнату, пометить как занятую:");
                Console.WriteLine("6.Найти самую дешёвую в расчёте на человека комнату и превратить её в Люксовую:");
                Console.WriteLine("7.Найти самый занятый постояльцами класс (отношение числа занятых комнат к общему числу комнат в классе) и добавить в отель новую комнату этого класса:");
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
