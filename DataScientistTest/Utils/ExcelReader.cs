using System;
using System.Collections.Generic;
using OfficeOpenXml;
using DataScientistTest.Models;

namespace DataScientistTest.Utils
{
    public class ExcelReader
    {
        // Método para leer la hoja "Transactions"
        public static List<Transaction> LoadTransactions(ExcelPackage package)
        {
            var transactions = new List<Transaction>();
            var worksheet = package.Workbook.Worksheets["Transactions"];

            if (worksheet == null)
                throw new Exception("La hoja 'Transactions' no existe en el archivo Excel.");

            string[] dateFormats = { "M/d/yy H:mm", "M/d/yyyy H:mm", "yyyy-MM-dd HH:mm:ss" };

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                DateTime createdDate;
                string dateText = worksheet.Cells[row, 3].Text;

                if (!DateTime.TryParseExact(dateText, dateFormats, null, System.Globalization.DateTimeStyles.None, out createdDate))
                {
                    Console.WriteLine($"Error al analizar la fecha: {dateText}");
                    createdDate = DateTime.MinValue;
                }

                transactions.Add(new Transaction
                {
                    AccountId = worksheet.Cells[row, 1].Text,
                    Amount = int.Parse(worksheet.Cells[row, 2].Text),
                    CreatedDate = createdDate,
                    Currency = worksheet.Cells[row, 4].Text,
                    Description = worksheet.Cells[row, 5].Text
                });
            }

            return transactions;
        }

        // Método para leer la hoja "Users"
        public static List<User> LoadUsers(ExcelPackage package)
        {
            var users = new List<User>();
            var worksheet = package.Workbook.Worksheets["Users"];

            if (worksheet == null)
                throw new Exception("La hoja 'Users' no existe en el archivo Excel.");

            string[] dateFormats = { "M/d/yy H:mm", "M/d/yyyy H:mm", "yyyy-MM-dd HH:mm:ss" };

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                DateTime lastBalanceUpdate;
                string dateText = worksheet.Cells[row, 5].Text;

                if (!DateTime.TryParseExact(dateText, dateFormats, null, System.Globalization.DateTimeStyles.None, out lastBalanceUpdate))
                {
                    Console.WriteLine($"Error al analizar la fecha: {dateText}");
                    lastBalanceUpdate = DateTime.MinValue;
                }

                users.Add(new User
                {
                    OwnerId = worksheet.Cells[row, 1].Text,
                    AccountId = worksheet.Cells[row, 2].Text,
                    Currency = worksheet.Cells[row, 3].Text,
                    CurrentBalance = int.Parse(worksheet.Cells[row, 4].Text),
                    LastBalanceUpdate = lastBalanceUpdate,
                    Country = worksheet.Cells[row, 6].Text
                });
            }

            return users;
        }

    }
}
