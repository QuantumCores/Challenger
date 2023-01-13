using Challenger.Domain.FormulaParser.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Challenger.Domain.FormulaParser.Models
{
    public class Operators
    {
        public static List<Operator> All { get; } = GetAllOperators();

        private static Dictionary<string, Operator> AllByString = All.ToDictionary(o => o.Symbol, o => o);
        private static Dictionary<OperatorTypes, Operator> AllByType = All.ToDictionary(o => o.Type, o => o);

        private static List<Operator> GetAllOperators()
        {
            var result = new List<Operator>();

            result.Add(new Operator("(", ExpressionType.Unbox, OperatorTypes.LeftParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add(new Operator(")", ExpressionType.Unbox, OperatorTypes.RightParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add(new Operator("+", ExpressionType.Add, OperatorTypes.Add, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("-", ExpressionType.Subtract, OperatorTypes.Subtract, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("*", ExpressionType.Multiply, OperatorTypes.Multiply, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("/", ExpressionType.Divide, OperatorTypes.Divide, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator(OperatorTypes.Sign.ToString(), ExpressionType.Switch, OperatorTypes.Sign, 4, AssociativityTypes.Right, OperationTypes.Unary));
            result.Add(new Operator("^", ExpressionType.Power, OperatorTypes.Power, 5, AssociativityTypes.Right, OperationTypes.Binary));

            return result;
        }

        public static Operator Get(string op)
        {
            return AllByString[op];
        }

        public static Operator Get(OperatorTypes type)
        {
            return AllByType[type];
        }
    }
}
