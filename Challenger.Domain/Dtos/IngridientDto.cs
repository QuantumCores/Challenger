using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenger.Domain.Dtos
{
    public class IngridientDto
    {
        public long Id { get; set; }

        public long DishId { get; set; }

        public long ProductId { get; set; }

        public int Size { get; set; }
    }
}
