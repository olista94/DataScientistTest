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

                // Calcular métricas simples (puedes ajustar esta lógica)
                int totalExpenses = userTransactions
                    .Where(t => t.Category == "Gastos")
                    .Sum(t => t.Transaction.Amount);

                int totalIncome = userTransactions
                    .Where(t => t.Category == "Ingresos")
                    .Sum(t => t.Transaction.Amount);

                // Ranking basado en balance y patrón de ingresos/gastos
                int score = totalIncome - totalExpenses + user.CurrentBalance;

                evaluations.Add(new UserEvaluation
                {
                    OwnerId = user.OwnerId,
                    Ranking = score, // Ordenaremos después
                    Justification = $"Ingresos: {totalIncome / 100.0}€, Gastos: {totalExpenses / 100.0}€, Balance: {user.CurrentBalance / 100.0}€"
                });
            }

            // Ordenar evaluaciones por puntaje descendente y asignar ranking
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
