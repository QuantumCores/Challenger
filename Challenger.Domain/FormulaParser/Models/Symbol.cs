using Challenger.Domain.FormulaParser.Contracts;

namespace Challenger.Domain.FormulaParser.Models
{
    public class Symbol : ISymbol
    {
        public Symbol(string value, SymbolTypes type)
        {
            this.Value = value;
            this.Type = type;
        }

        public string Value { get; set; }

        public SymbolTypes Type { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
