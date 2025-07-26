public class Professor
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Subject { get; set; }
  public string PhotoPath { get; set; }
}

public class ProfessorValidator : AbstractValidator<Professor>
{
  public ProfessorValidator()
  {
    RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
    RuleFor(p => p.Subject).NotEmpty().WithMessage("Subject is required.");
  }
}