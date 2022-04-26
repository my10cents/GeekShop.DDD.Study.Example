using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Application.ViewModels
{
    public class ProductModel
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime RecordDate { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Image { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor minimo de {1}")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor minimo de {1} ")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor minimo de {1} ")]
        public decimal Width { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor minimo de {1} ")]
        public decimal Depth { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
