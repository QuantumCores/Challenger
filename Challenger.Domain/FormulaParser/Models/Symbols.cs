using Challenger.Domain.FormulaParser.Contracts;
using System.Collections.Generic;

namespace Challenger.Domain.FormulaParser.Models
{
    public class Symbols
    {
        private static readonly Dictionary<string, Symbol> All = GetAllSymbols();
        private static readonly Dictionary<string, Symbol> Added = new Dictionary<string, Symbol>();

        private static Dictionary<string, Symbol> GetAllSymbols()
        {
            var res = new Dictionary<string, Symbol>();

            res.Add("1", new Symbol("1", SymbolTypes.Number));
            res.Add("2", new Symbol("2", SymbolTypes.Number));
            res.Add("3", new Symbol("3", SymbolTypes.Number));
            res.Add("4", new Symbol("4", SymbolTypes.Number));
            res.Add("5", new Symbol("5", SymbolTypes.Number));
            res.Add("6", new Symbol("6", SymbolTypes.Number));
            res.Add("7", new Symbol("7", SymbolTypes.Number));
            res.Add("8", new Symbol("8", SymbolTypes.Number));
            res.Add("9", new Symbol("9", SymbolTypes.Number));
            res.Add("0", new Symbol("0", SymbolTypes.Number));

            //var d = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            //res.Add(d, new Symbol(d, SymbolTypes.Number));

            res.Add("+", new Symbol("+", SymbolTypes.BinaryOperator));
            res.Add("-", new Symbol("-", SymbolTypes.BinaryOperator));
            res.Add("*", new Symbol("*", SymbolTypes.BinaryOperator));
            res.Add("/", new Symbol("/", SymbolTypes.BinaryOperator));
            res.Add("^", new Symbol("^", SymbolTypes.BinaryOperator));

            res.Add("<", new Symbol("<", SymbolTypes.BinaryOperator));
            res.Add(">", new Symbol(">", SymbolTypes.BinaryOperator));
            res.Add("<=", new Symbol("<=", SymbolTypes.BinaryOperator));
            res.Add(">=", new Symbol(">=", SymbolTypes.BinaryOperator));
            res.Add("==", new Symbol("==", SymbolTypes.BinaryOperator));
            res.Add("!=", new Symbol("!=", SymbolTypes.BinaryOperator));

            res.Add("(", new Symbol("(", SymbolTypes.LeftParenthesis));
            res.Add(")", new Symbol(")", SymbolTypes.RightParenthesis));
            res.Add(",", new Symbol(",", SymbolTypes.Coma));

            

            return res;
        }

        public static bool TryGetValue(string v, out Symbol symbol)
        {
            if (All.TryGetValue(v, out var s))
            {
                symbol = new Symbol(s.Value, s.Type);
                return true;
            }

            symbol = null;
            return false;
        }

        public static bool AddSymbol(string value, SymbolTypes type)
        {
            if (!All.TryGetValue(value, out var s))
            {
                var tmp = new Symbol(value, type);
                All.Add(value, tmp);
                Added.Add(value, tmp);
                return true;
            }

            return false;
        }
    }
}
