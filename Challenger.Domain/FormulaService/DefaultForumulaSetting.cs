namespace Challenger.Domain.FormulaService
{
    public class DefaultForumulaSetting
    {
        public string Name { get; set; }

        public FormulaTypeEnum Type{ get; set; }

        public string Formula { get; set; }

        public string Description { get; set; }

        public bool Aggregate { get; set; }
    }
}
