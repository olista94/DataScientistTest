using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using DataScientistTest.Models;

namespace DataScientistTest.Utils
{
    public class ExcelWriter
    {
        public static void SaveResults(
            string filePath,
            List<CategorizedTransaction> categorizedTransactions,
            List<UserEvaluation> userEvaluations)
        {
            using (var package = new ExcelPackage())
            {
                // Hoja de transacciones categorizadas
                var transactionSheet = package.Workbook.Worksheets.Add("CategorizedTransactions");
                transactionSheet.Cells[1, 1].Value = "AccountId";
                transactionSheet.Cells[1, 2].Value = "Amount";
                transactionSheet.Cells[1, 3].Value = "CreatedDate";
                transactionSheet.Cells[1, 4].Value = "Currency";
                transactionSheet.Cells[1, 5].Value = "Description";
                transactionSheet.Cells[1, 6].Value = "Category";

                // Escribir transacciones categorizadas
                int row = 2;
                foreach (var ct in categorizedTransactions)
                {
                    transactionSheet.Cells[row, 1].Value = ct.Transaction.AccountId;
                    transactionSheet.Cells[row, 2].Value = ct.Transaction.Amount;
                    transactionSheet.Cells[row, 3].Value = ct.Transaction.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                    transactionSheet.Cells[row, 4].Value = ct.Transaction.Currency;
                    transactionSheet.Cells[row, 5].Value = ct.Transaction.Description;
                    transactionSheet.Cells[row, 6].Value = ct.Category;
                    row++;
                }

                // Hoja de evaluaciones de usuarios
                var userSheet = package.Workbook.Worksheets.Add("UserEvaluations");
                userSheet.Cells[1, 1].Value = "OwnerId";
                userSheet.Cells[1, 2].Value = "Ranking";
                userSheet.Cells[1, 3].Value = "Justification";

                // Escribir evaluaciones de usuarios
                row = 2;
                foreach (var ue in userEvaluations)
                {
                    userSheet.Cells[row, 1].Value = ue.OwnerId;
                    userSheet.Cells[row, 2].Value = ue.Ranking;
                    userSheet.Cells[row, 3].Value = ue.Justification;
                    row++;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
