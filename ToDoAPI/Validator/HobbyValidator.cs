using FluentValidation;
using ToDoAPI.Models;

namespace ToDoAPI.Validator
{
    public class HobbyValidator : AbstractValidator<HobbyModel>
    {
        public HobbyValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
