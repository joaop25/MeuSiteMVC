public class Professor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Preference { get; set; }
    public Image Foto { get; set; }
}

public class ProfessorValidator : AbstractValidator<Professor>
{
    public ProfessorValidator() {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome do professor é necessário.");
        RuleFor(x => x.Preference).NotEmpty().WithMessage("Preference do professor é necessário.");
    }
}