using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteMVC.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "O campo {0} não está em um formato válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Disciplina { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está em formato incorreto")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Professor")]
        public IFormFile? ImagemUpload { get; set; }

        public string? Imagem { get; set; }
    }
}