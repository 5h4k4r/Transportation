// ReSharper disable InconsistentNaming

namespace Core.Models.Common;

public enum Currency
{
    IQD = 1,
    IRT = 10,
#pragma warning disable CA1069
    IRR = 10,
#pragma warning restore CA1069
}

public static class CurrencySymbol
{
    public static string IRT = "IRR";
    public static string IQD = "IQD";
    public static string IRR = "IRT";
}