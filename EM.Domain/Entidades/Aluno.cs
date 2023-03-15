using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EM.Domain.Enums;
using EM.Domain.Interfaces;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EM.Domain.Entidades
{

    public class Aluno : IEntidade
    {

        [Key]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "Informe seu nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve conter de 3 a 100 caracteres!")]
        public string? Nome { get; set; }
        [AllowNull]
        public string? Cpf { get; set; }
        [Required(ErrorMessage = "Informe sua data de nascimento")]
        public DateTime Nascimento { get; set; }
        [Required]
        public EnumeradorSexo Sexo { get; set; }

        public Aluno()
        {
            Nascimento = DateTime.Today;
        }

        public override bool Equals(object? obj)
        {
            return obj is Aluno aluno &&
                   Matricula == aluno.Matricula &&
                   Nome == aluno.Nome &&
                   Cpf == aluno.Cpf &&
                   Nascimento == aluno.Nascimento &&
                   Sexo == aluno.Sexo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Matricula, Nome, Cpf, Nascimento, Sexo);
        }

        public override string ToString()
        {
            return $"GEN_ID(GEN_TBALUNOS, 1),'{Nome}','{Sexo}','{Nascimento:yyyy-MM-dd}','{Cpf}'";
        }


    }
}
