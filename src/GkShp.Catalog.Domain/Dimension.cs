using GkShp.Core.DomainTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Domain
{
    public class Dimension
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimension(decimal height, decimal width, decimal depth)
        {
            Validations.ValidateIfLessThan(height, 1, "The field Height can't be less or equal than 0");
            Validations.ValidateIfLessThan(width, 1, "The field Width can't be less or equal than 0");
            Validations.ValidateIfLessThan(depth, 1, "The field Depth can't be less or equal than 0");
            Height = height;
            Width = width;
            Depth = depth;
        }

        public string FormatedDescription()
        {
            return $"WxHxD:{Width} x {Height} x {Depth}";
        }

        public override string ToString()
        {
            return FormatedDescription();
        }
    }
}
