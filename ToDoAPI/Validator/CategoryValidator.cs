using FluentValidation;
using ToDoAPI.Models;

namespace ToDoAPI.Validator
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is not empty");
        }
    }
}
