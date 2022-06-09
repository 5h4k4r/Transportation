﻿using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IDiscountCodeRepository
{
    Task<List<DiscountCodeDto>> ListDiscountCodes(ListDiscountCodesRequest model);

    Task<DiscountCodeDto?> DiscountCodeDetail(uint id);

    Task<int> ListDiscountCodesCount(ListDiscountCodesRequest model);

    Task<int> ListDiscountCodeUsersCount(ListDiscountCodesRequest model, uint codeId);
    Task<DiscountCodeUserRepositoryDto> ListDiscountCodeUsers(ListDiscountCodesRequest model, uint codeId);

    Task<DiscountCodeUserRepositoryDto>
        ListTasksByUser(ListDiscountCodesRequest model, uint discountCodeId, uint userId);

    void CreateDiscountCode(DiscountCodeDto discountCode);
}