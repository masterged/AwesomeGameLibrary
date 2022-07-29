using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AwesomeGameLibrary.Web.Errors;

public static class ErrorCollectionExtensions
{
    public static ModelStateDictionary ToModelState(this IEnumerable<Error> errors)
    {
        var modelStateDict = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDict.AddModelError(error.Code, error.Description);
        }

        return modelStateDict;
    }
}