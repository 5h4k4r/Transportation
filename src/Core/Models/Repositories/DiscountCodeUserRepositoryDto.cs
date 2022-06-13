using Core.Models.Base;
namespace Core.Models.Repositories;

public class DiscountCodeUserRepositoryDto
{
    public List<DiscountCodeUserDto> DiscountCodeUser { get; set; }

    public int? TotalAmount { get; set; }
    
    public int? TotalCount { get; set; }
    
    public DiscountCodeDto? DiscountCode { get; set; }
}