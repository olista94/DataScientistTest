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
                string category = CategorizeTransaction(transaction.Description, transaction.Amount);

                categorizedTransactions.Add(new CategorizedTransaction
                {
                    Transaction = transaction,
                    Category = category
                });
            }

            return categorizedTransactions;
        }

        private string CategorizeTransaction(string description, int amount)
        {
            description = description.ToLower();

            if (description.Contains("nómina") || description.Contains("salario"))
                return "Salary";
            else if (description.Contains("transferencia entre cuentas") || description.Contains("enviado desde"))
                return "Own Transfers";
            else if (description.Contains("transferencia de") || description.Contains("bizum"))
                return "Third-Party Transfers";
            else if (description.Contains("compra en") || description.Contains("reintegro"))
                return "Shopping";
            else if (description.Contains("cuota") || description.Contains("servicios") || description.Contains("pago mensual") || description.Contains("tarjeta"))
                return "Bills/Services";
            else if (description.Contains("intereses") || description.Contains("comisiones") || description.Contains("liquidación"))
                return "Interests/Fees";
            else if (amount > 0)
                return "Deposits";
            else if (amount < 0)
                return "Expenses";
            else
                return "Others";
        }
    }
}
