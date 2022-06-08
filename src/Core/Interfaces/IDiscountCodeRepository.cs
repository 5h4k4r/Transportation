using Core.Models.Base;
using Core.Models.Requests;
using Core.Models.Repositories;

namespace Core.Interfaces;

public interface IDiscountCodeRepository
{
    Task<List<DiscountCodeDto>> ListDiscountCodes(DiscountCodeRequest model);

    Task<DiscountCodeDto?> DiscountCodeDetail(uint id);

    Task<int> ListDiscountCodeCount(DiscountCodeRequest model);

    Task<int> ListDiscountCodeUsersCount(DiscountCodeRequest model, uint codeId);
    Task<DiscountCodeUserRepositoryDto> ListUsers(DiscountCodeRequest model, uint codeId);

    Task<DiscountCodeUserRepositoryDto> ListTasksByUser(DiscountCodeRequest model, uint discountCodeId, uint userId);

    void CreateDiscountCode(DiscountCodeDto discountCode);

}