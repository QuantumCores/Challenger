using System.Collections.Generic;

namespace Challenger.Domain.FormulaParser.Contracts
{
    public interface IRPN
    {
        List<ISymbol> Output { get; set; }

        List<string> Variables { get; set; }
    }
}
