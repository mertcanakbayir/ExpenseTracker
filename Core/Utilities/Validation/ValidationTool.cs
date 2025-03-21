using FluentValidation;

namespace Core.Utilities.Validation
{
    public class ValidationTool
    {
        public static List<string> Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                return result.Errors.Select(error => error.ErrorMessage).ToList();
            }
            return new List<string>();
        }
    }
}
