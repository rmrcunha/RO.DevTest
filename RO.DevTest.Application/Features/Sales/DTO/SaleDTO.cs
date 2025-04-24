using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.DTO;

public class SaleDTO
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? CustomerName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}
