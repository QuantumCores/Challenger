namespace Challenger.Domain.FormulaParser.Contracts
{
    public interface ISymbol
    {
        string Value { get; set; }

        SymbolTypes Type { get; set; }
    }
}
