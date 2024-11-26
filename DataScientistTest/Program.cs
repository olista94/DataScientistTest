using System;
using System.IO;
using DataScientistTest.Models;
using DataScientistTest.Services;
using DataScientistTest.Utils;
using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace DataScientistTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Rutas del archivo de entrada y salida
            // IMPORTANTE: Colocar el archivo en la carpeta raíz del proyecto y MODIFICAR la ruta
            string inputFilePath = @"C:\Users\Usuario\source\repos\DataScientistTest\DataScientistTest\DataScientistTest\1-Data_Test_A.xlsx";
            string outputFilePath = @"C:\Users\Usuario\source\repos\DataScientistTest\DataScientistTest\DataScientistTest\Resultados_Test.xlsx";

            try
            {

                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine("El archivo Excel de entrada no fue encontrado.");
                    return;
                }

                List<Transaction> transactions;
                List<User> users;
                List<CategorizedTransaction> categorizedTransactions;
                List<UserEvaluation> userEvaluations;

                Console.WriteLine("Leyendo el archivo Excel...");
                using (var package = new ExcelPackage(new FileInfo(inputFilePath)))
                {
                    // Leer datos de las hojas del archivo Excel
                    transactions = ExcelReader.LoadTransactions(package);
                    users = ExcelReader.LoadUsers(package);
                }

                Console.WriteLine("Categorizando transacciones...");
                var transactionCategorizer = new TransactionCategorizer();
                categorizedTransactions = transactionCategorizer.Categorize(transactions);

                Console.WriteLine("Evaluando usuarios...");
                var userEvaluator = new UserEvaluator();
                userEvaluations = userEvaluator.Evaluate(users, categorizedTransactions);

                Console.WriteLine("Guardando resultados en un nuevo archivo Excel...");
                ExcelWriter.SaveResults(outputFilePath, categorizedTransactions, userEvaluations);

                Console.WriteLine($"El archivo con los resultados ha sido guardado en: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la ejecución:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
