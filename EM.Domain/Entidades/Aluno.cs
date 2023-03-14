using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EM.Domain.Enums;
using EM.Domain.Interfaces;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace EM.Domain.Entidades
{
    [Table("ALUNOS")]
    public class Aluno : IEntidade
    {
        private Random _random = new Random();

        [Key]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "Campo obirgatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve conter de 3 a 100 caracteres!")]
        public string? Nome { get; set; }
        [AllowNull]
        public string? Cpf { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }
        [Required]
        public EnumeradorSexo Sexo { get; set; }

        public Aluno()
        {
            Matricula = _random.Next(0,100000);
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
            return $"{Matricula},'{Nome}','{Sexo}','{Nascimento.ToString("yyyy-MM-dd")}','{Cpf}'";
        }


    }
}
