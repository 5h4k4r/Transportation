using System.ComponentModel.DataAnnotations;

namespace Api.Helpers;

public static class UserHelper
{
    public static string PreparePhoneNumber([Required] string model)
    {
        if (model[0] != '+')
            model = "+" + model;


        return model;
    }
}