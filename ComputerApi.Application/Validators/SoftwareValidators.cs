using ComputerApi.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Application.Validators
{
    public class CreateSoftwareDtoValidator : AbstractValidator<CreateSoftwareDto>
    {
        public CreateSoftwareDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("Version is required")
                .MaximumLength(50).WithMessage("Version cannot exceed 50 characters");
        }
    }

    public class UpdateSoftwareDtoValidator : AbstractValidator<UpdateSoftwareDto>
    {
        public UpdateSoftwareDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("Version is required")
                .MaximumLength(50).WithMessage("Version cannot exceed 50 characters");
        }
    }
}
