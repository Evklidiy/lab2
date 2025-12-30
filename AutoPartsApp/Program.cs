using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoPartsApp.Models;

namespace AutoPartsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Подключение идет через appsettings.json внутри контекста
            using var db = new AutoPartsWarehouseContext();

            Console.WriteLine("Вариант 41: Система управления складом автозапчастей");

            // 2.1. Выборка всех данных из таблицы стороны «один» (1 шт.)
            // Берем одного поставщика
            Console.WriteLine("\n--- 2.1 Выборка поставщика ---");
            var supplier = db.Suppliers.FirstOrDefault();
            if (supplier != null)
                Console.WriteLine($"Поставщик: {supplier.Name}, Рейтинг: {supplier.Rating}");

            // 2.2. Выборка данных стороны «один» с фильтрацией (1 шт.)
            // Запчасти дороже 10.00
            Console.WriteLine("\n--- 2.2 Фильтрация запчастей (Цена > 10) ---");
            var expensivePart = db.AutoParts.Where(p => p.SalePrice > 10).FirstOrDefault();
            if (expensivePart != null)
                Console.WriteLine($"Запчасть: {expensivePart.Name}, Цена: {expensivePart.SalePrice}");

            // 2.3. Группировка данных (1 шт.)
            // Сгруппировать остатки на складе по запчасти и посчитать общее количество
            Console.WriteLine("\n--- 2.3 Группировка остатков (Total Qty) ---");
            var stockSummary = db.Stocks
                .GroupBy(s => s.PartId)
                .Select(g => new { PartId = g.Key, TotalQuantity = g.Sum(x => x.Quantity) })
                .FirstOrDefault();
            if (stockSummary != null)
                Console.WriteLine($"PartID: {stockSummary.PartId}, Всего на складе: {stockSummary.TotalQuantity}");

            // 2.4. Выборка из двух таблиц (Join) (1 шт.)
            // Соединяем Поставки и Поставщиков
            Console.WriteLine("\n--- 2.4 Join Поставки и Поставщики ---");
            var supplyInfo = (from s in db.Supplies
                              join sup in db.Suppliers on s.SupplierId equals sup.SupplierId
                              select new { sup.Name, s.SupplyDate, s.Status })
                              .FirstOrDefault();
            if (supplyInfo != null)
                Console.WriteLine($"Поставщик: {supplyInfo.Name}, Дата: {supplyInfo.SupplyDate}, Статус: {supplyInfo.Status}");

            // 2.5. Выборка из двух таблиц с фильтрацией (1 шт.)
            // Продажи определенного клиента и какие запчасти он купил
            Console.WriteLine("\n--- 2.5 Join + Filter (Продажи VW Golf) ---");
            var saleInfo = (from s in db.Sales
                            join sp in db.SalePositions on s.SaleId equals sp.SaleId
                            join p in db.AutoParts on sp.PartId equals p.PartId
                            where s.CarModel == "VW Golf"
                            select new { s.ClientName, PartName = p.Name, sp.Quantity })
                            .FirstOrDefault();
            if (saleInfo != null)
                Console.WriteLine($"Клиент: {saleInfo.ClientName} купил {saleInfo.Quantity} шт. {saleInfo.PartName}");

            // 2.6. Вставка данных в таблицу «Один» (1 шт.)
            Console.WriteLine("\n--- 2.6 Вставка новой запчасти ---");
            var newPart = new AutoPart
            {
                Article = "NEW-999",
                Name = "Wiper Blades",
                Manufacturer = "Valeo",
                PurchasePrice = 10,
                SalePrice = 20
            };
            db.AutoParts.Add(newPart);
            db.SaveChanges();
            Console.WriteLine($"Добавлена запчасть: {newPart.Name}, ID: {newPart.PartId}");

            // 2.7. Вставка данных в таблицу «Многие» (1 шт.)
            // Добавляем запись о наличии этой запчасти на складе
            Console.WriteLine("\n--- 2.7 Вставка записи на склад ---");
            var newStock = new Stock
            {
                PartId = newPart.PartId,
                Quantity = 50,
                LocationShelf = "Shelf Z-9"
            };
            db.Stocks.Add(newStock);
            db.SaveChanges();
            Console.WriteLine($"Запчасть размещена на складе. ID записи: {newStock.StockId}");

            // 2.8. Удаление данных из таблицы «Один» (1 шт.)
            Console.WriteLine("\n--- 2.8 Удаление запчасти (Один) ---");
            // Находим только что созданную запчасть. Благодаря Cascade Delete удалятся и записи со склада.
            var partToDelete = db.AutoParts.FirstOrDefault(p => p.Article == "NEW-999");
            if (partToDelete != null)
            {
                db.AutoParts.Remove(partToDelete);
                db.SaveChanges();
                Console.WriteLine("Запчасть удалена.");
            }

            // 2.9. Удаление данных из таблицы «Многие» (1 шт.)
            Console.WriteLine("\n--- 2.9 Удаление позиции продажи ---");
            var salePosToDelete = db.SalePositions.FirstOrDefault();
            if (salePosToDelete != null)
            {
                db.SalePositions.Remove(salePosToDelete);
                db.SaveChanges();
                Console.WriteLine($"Позиция продажи ID {salePosToDelete.SalePositionId} удалена.");
            }

            // 2.10. Обновление записей (1 шт.)
            Console.WriteLine("\n--- 2.10 Обновление цены продажи ---");
            var partToUpdate = db.AutoParts.FirstOrDefault();
            if (partToUpdate != null)
            {
                decimal oldPrice = partToUpdate.SalePrice;
                partToUpdate.SalePrice += 5;
                db.SaveChanges();
                Console.WriteLine($"Цена на {partToUpdate.Name} изменена с {oldPrice} на {partToUpdate.SalePrice}");
            }

            Console.WriteLine("\nГотово. Нажмите клавишу...");
            Console.ReadKey();
        }
    }
}