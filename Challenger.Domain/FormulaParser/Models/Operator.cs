using Challenger.Domain.FormulaParser.Contracts;
using System.Linq.Expressions;

namespace Challenger.Domain.FormulaParser.Models
{
    public class Operator
    {
        public string Symbol { get; }

        // Same as OperationTypes Type but from Linq library
        public ExpressionType ExpressionType { get; }

        public OperatorTypes Type { get; }

        public int Precedence { get; }

        public AssociativityTypes AssociativityType { get; }

        public OperationTypes OperationType { get; }

        public Operator(string symbol, ExpressionType expressionType, OperatorTypes type, int precedence, AssociativityTypes associativity, OperationTypes operationType)
        {
            this.Symbol = symbol;
            this.ExpressionType = expressionType;
            this.Type = type;
            this.Precedence = precedence;
            this.AssociativityType = associativity;
            this.OperationType = operationType;
        }
    }
}
