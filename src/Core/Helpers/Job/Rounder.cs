using System.Text.Json;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;

namespace Api.Helpers.JobController;

internal static class Rounder
{
    private const string Table = "rounder";

    public static async Task<RounderDto?> Get(Currency currency, IUnitOfWork unitOfWork)
    {
        var rounder =
            JsonSerializer.Deserialize<RounderDto>(await unitOfWork.Cache.GetKey<string>($"{Table}_{currency}"));
        if (rounder != null) return rounder;
        rounder = await unitOfWork.Tasks.GetLatestRounder(currency);
        await unitOfWork.Cache.SetKey($"{Table}_{currency}", JsonSerializer.Serialize(rounder), TimeSpan.FromDays(1));

        return rounder;
    }
}