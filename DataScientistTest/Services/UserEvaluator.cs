using DataScientistTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataScientistTest.Services
{
    public class UserEvaluator
    {
        public List<UserEvaluation> Evaluate(List<User> users, List<CategorizedTransaction> categorizedTransactions)
        {
            var evaluations = new List<UserEvaluation>();

            foreach (var user in users)
            {
                // Filtrar transacciones del usuario
                var userTransactions = categorizedTransactions
                    .Where(t => t.Transaction.AccountId == user.AccountId)
                    .ToList();

                // Calcular gastos
                int totalExpenses = userTransactions
                    .Where(t => t.Category == "Shopping" || t.Category == "Bills/Services" || t.Category == "Interests/Fees")
                    .Sum(t => t.Transaction.Amount);

                // Calcular ingresos
                int totalIncome = userTransactions
                    .Where(t => t.Category == "Salary" || t.Category == "Deposits" || t.Category == "Own Transfers")
                    .Sum(t => t.Transaction.Amount);

                // Ranking basado en balance y patrón de ingresos/gastos
                int score = totalIncome - totalExpenses;

                evaluations.Add(new UserEvaluation
                {
                    OwnerId = user.OwnerId,
                    Ranking = score,
                    Justification = $"Ingresos: {totalIncome}, Gastos: {totalExpenses}, Balance: {user.CurrentBalance}"
                });
            }

            // Ordenar puntaje descendente y asignar ranking
            var orderedEvaluations = evaluations
                .OrderByDescending(e => e.Ranking)
                .Select((e, index) => new UserEvaluation
                {
                    OwnerId = e.OwnerId,
                    Ranking = index + 1,
                    Justification = e.Justification
                })
                .ToList();

            return orderedEvaluations;
        }
    }
}
