namespace Challenger.Domain.FormulaParser.Contracts
{
    public enum SymbolTypes
    {
        Undefined = 0,
        Number = 1,
        UnaryOperator = 2,
        BinaryOperator = 3,
        LeftParenthesis = 4,
        RightParenthesis = 5,
        Function = 6,
        Variable = 7,
        Imaginary = 8,
        Coma = 9
    }
}
