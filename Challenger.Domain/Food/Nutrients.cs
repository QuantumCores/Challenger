﻿namespace Challenger.Domain.Food
{
    public interface Nutrients
    {
        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
