using Challenger.Domain.FormulaParser.Contracts;
using Challenger.Domain.FormulaParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace Challenger.Domain.FormulaParser
{
    public class RPNParser
    {
        public static IRPN Parse(string text)
        {
            var output = new List<ISymbol>();
            var operators = new List<ISymbol>();
            var variables = new Dictionary<string, int>();

            ISymbol previousSymbol = new Symbol("", SymbolTypes.Undefined);

            var calc = text.ToLower();
            for (int i = 0; i < calc.Length; i++)
            {
                var c = calc[i];
                Symbol symbol = null;

                if (c == 32)
                {
                    if (previousSymbol.Type == SymbolTypes.Undefined && previousSymbol.Value != "")
                    {
                        if (Symbols.TryGetValue(previousSymbol.Value, out var tmp))
                        {
                            previousSymbol = tmp;
                            symbol = tmp;
                            i--;
                        }
                        else
                        {
                            FindSymbolForUndefined(ref previousSymbol, operators, output, variables);
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (c == '"')
                {
                    var end = calc.IndexOf('"', i + 1);
                    symbol = new Symbol(calc.Substring(i + 1, end - i - 1), SymbolTypes.Text);
                    previousSymbol = symbol;
                    output.Add(symbol);
                    i = end;
                    continue;
                }

                if (symbol != null || Symbols.TryGetValue(c.ToString(), out symbol))
                {
                    if ((symbol.Value == ">" || symbol.Value == "<") && calc[i + 1] == '=')
                    {
                        symbol.Value += "=";
                        i++;
                    }

                    if (previousSymbol.Type == SymbolTypes.Undefined && previousSymbol.Value != "")
                    {
                        if (symbol.Type == SymbolTypes.Number)
                        {
                            previousSymbol.Value += symbol.Value;
                            continue;
                        }
                        else
                        {
                            FindSymbolForUndefined(ref previousSymbol, operators, output, variables);
                        }
                    }

                    if (symbol.Type == SymbolTypes.Number)
                    {
                        if (previousSymbol.Type == SymbolTypes.Number)
                        {
                            output.Last().Value += symbol.Value;
                        }
                        else
                        {
                            output.Add(symbol);
                        }
                    }
                    else if (symbol.Type == SymbolTypes.LeftParenthesis)
                    {
                        operators.Add(symbol);
                    }
                    else if (symbol.Type == SymbolTypes.RightParenthesis)
                    {
                        if (operators.Count > 0)
                        {
                            while (operators.Count > 0 && operators[operators.Count - 1].Type != SymbolTypes.LeftParenthesis)
                            {
                                Pop(output, operators);
                            }

                            if (operators[operators.Count - 1].Type == SymbolTypes.LeftParenthesis)
                            {
                                operators.RemoveAt(operators.Count - 1);
                            }
                        }
                    }
                    else if (symbol.Type == SymbolTypes.Coma)
                    {
                        if (operators.Count > 0)
                        {
                            while (operators.Count > 0 && operators[^1].Type != SymbolTypes.LeftParenthesis)
                            {
                                Pop(output, operators);
                            }
                        }
                    }
                    else if (symbol.Type == SymbolTypes.BinaryOperator || symbol.Type == SymbolTypes.UnaryOperator)
                    {
                        if (previousSymbol.Type != SymbolTypes.Number && previousSymbol.Type != SymbolTypes.Variable && previousSymbol.Type != SymbolTypes.RightParenthesis
                             && symbol.Value == "-")
                        {
                            symbol.Type = SymbolTypes.UnaryOperator;
                            symbol.Value = OperatorTypes.Sign.ToString();
                        }

                        while (operators.Count > 0 &&
                            (operators.Last().Type == SymbolTypes.Function ||
                            Operators.Get(operators.Last().Value).Precedence > Operators.Get(symbol.Value).Precedence ||
                            ((Operators.Get(operators.Last().Value).Precedence == Operators.Get(symbol.Value).Precedence) && (Operators.Get(operators.Last().Value).AssociativityType == AssociativityTypes.Left))) &&
                            operators[operators.Count - 1].Value != "(")
                        {
                            Pop(output, operators);
                        }
                        operators.Add(symbol);
                    }

                    previousSymbol = symbol;
                }
                else
                {
                    if (previousSymbol.Type == SymbolTypes.Undefined)
                    {
                        previousSymbol.Value += c.ToString();
                    }
                    else if (c == '.' && previousSymbol.Type == SymbolTypes.Number)
                    {
                        previousSymbol.Value += c.ToString();
                    }
                    else
                    {
                        previousSymbol = new Symbol(c.ToString(), SymbolTypes.Undefined);
                    }
                }
            }

            if (previousSymbol.Type == SymbolTypes.Undefined && previousSymbol.Value != "")
            {
                FindSymbolForUndefined(ref previousSymbol, operators, output, variables);
            }

            if (operators.Count != 0)
            {
                while (operators.Count > 0)
                {
                    output.Add(operators[operators.Count - 1]);
                    operators.RemoveAt(operators.Count - 1);
                }
            }

            return new RPN(output, variables.Select(d => d.Key).ToList());
        }

        private static void FindSymbolForUndefined(ref ISymbol previousSymbol, List<ISymbol> operators, List<ISymbol> output, Dictionary<string, int> variables)
        {
            if (previousSymbol.Value == "i")
            {
                previousSymbol = new Symbol(previousSymbol.Value, SymbolTypes.Imaginary);
                output.Add(previousSymbol);
            }
            else if (Functions.All.ContainsKey(previousSymbol.Value))
            {
                previousSymbol = new Symbol(previousSymbol.Value, SymbolTypes.Function);
                operators.Add(previousSymbol);
            }
            else
            {
                previousSymbol = new Symbol(previousSymbol.Value, SymbolTypes.Variable);
                output.Add(previousSymbol);
                if (!variables.ContainsKey(previousSymbol.Value))
                {
                    variables.Add(previousSymbol.Value, 0);
                }
            }
        }

        private static void Pop(List<ISymbol> numbers, List<ISymbol> operators)
        {
            numbers.Add(operators[operators.Count - 1]);
            operators.RemoveAt(operators.Count - 1);
        }
    }
}
