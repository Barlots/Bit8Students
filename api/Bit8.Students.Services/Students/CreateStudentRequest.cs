using FluentValidation;

namespace Bit8.Students.Services.Students
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
    }

    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}