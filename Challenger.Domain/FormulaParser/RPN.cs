using Challenger.Domain.FormulaParser.Contracts;
using System.Collections.Generic;

namespace Challenger.Domain.FormulaParser
{
    public class RPN : IRPN
    {
        public RPN(List<ISymbol> output, List<string> variables)
        {
            this.Output = output;
            this.Variables = variables;
        }

        public List<ISymbol> Output { get; set; } = new List<ISymbol>();

        public List<string> Variables { get; set; } = new List<string>();
    }
}
