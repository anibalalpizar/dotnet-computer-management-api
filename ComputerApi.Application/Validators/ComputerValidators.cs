using ComputerApi.Application.DTOs;
using FluentValidation;

namespace ComputerApi.Application.Validators
{
    public class CreateComputerDtoValidator : AbstractValidator<CreateComputerDto>
    {
        public CreateComputerDtoValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required")
                .MaximumLength(100).WithMessage("Brand cannot exceed 100 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid computer type");

            RuleFor(x => x.ManufacturingYear)
                .GreaterThanOrEqualTo(1990).WithMessage("Manufacturing year must be 1990 or later")
                .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage("Manufacturing year cannot be in the future");
        }
    }

    public class UpdateComputerDtoValidator : AbstractValidator<UpdateComputerDto>
    {
        public UpdateComputerDtoValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required")
                .MaximumLength(100).WithMessage("Brand cannot exceed 100 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid computer type");

            RuleFor(x => x.ManufacturingYear)
                .GreaterThanOrEqualTo(1990).WithMessage("Manufacturing year must be 1990 or later")
                .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage("Manufacturing year cannot be in the future");
        }
    }
}
