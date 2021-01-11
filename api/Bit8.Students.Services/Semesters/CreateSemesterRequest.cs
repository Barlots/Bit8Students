using FluentValidation;

namespace Bit8.Students.Services.Semesters
{
    public class CreateSemesterRequest
    {
        public string Name { get; set; }
    }

    public class CreateSemesterRequestValidator : AbstractValidator<CreateSemesterRequest>
    {
        public CreateSemesterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}