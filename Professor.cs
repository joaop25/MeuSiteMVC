public class Professor : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string SSN { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public string Email { get; set; }

    public byte[] Photo { get; set; }
}// O Validation para o Fluent Validation será no formato: 
public class ProfessorValidator : AbstractValidator<Professor>
{
    public ProfessorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.SSN).NotEmpty().WithMessage("SSN is required.");

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Email).EmailAddress().WithMessage("A valid email is required.");
    }
}