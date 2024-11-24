using DataScientistTest.Models;
using System.Collections.Generic;

namespace DataScientistTest.Services
{
    public class TransactionCategorizer
    {
        public List<CategorizedTransaction> Categorize(List<Transaction> transactions)
        {
            var categorizedTransactions = new List<CategorizedTransaction>();

            foreach (var transaction in transactions)
            {
                // Usar el nuevo método CategorizeTransaction para obtener la categoría
                string category = CategorizeTransaction(transaction.Description, transaction.Amount);

                // Agregar la transacción categorizada a la lista
                categorizedTransactions.Add(new CategorizedTransaction
                {
                    Transaction = transaction,
                    Category = category
                });
            }

            return categorizedTransactions;
        }

        // Método de categorización que se usará en cada transacción
        static string CategorizeTransaction(string description, decimal amount)
        {
            // Convertir la descripción a minúsculas para evitar problemas de mayúsculas/minúsculas
            description = description.ToLower();

            // Lógica de categorización:
            if (description.Contains("nómina") || description.Contains("nomina"))
                return "Salary"; // Salarios
            else if (description.Contains("transferencia"))
                return "Transfers"; // Transferencias
            else if (description.Contains("cuota") || description.Contains("servicios") || description.Contains("pago"))
                return "Bills/Services"; // Servicios o cuotas
            else if (amount < 0 && (description.Contains("compra") || description.Contains("tienda")))
                return "Shopping"; // Compras en tiendas
            else if (amount > 0)
                return "Deposits"; // Depósitos
            else
                return "Others"; // Otros casos
        }
    }
}
