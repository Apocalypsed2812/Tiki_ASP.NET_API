using FluentValidation;
using ToDoAPI.Models;

namespace ToDoAPI.Validator
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is not empty");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is not empty")
                .GreaterThan(0).WithMessage("Price must greater than 0");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is not empty")
                .GreaterThan(0).WithMessage("Quantity must greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is not empty");
        }
    }
}
