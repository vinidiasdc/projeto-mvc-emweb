using EM.Domain.Conexao;
using EM.Domain.Entidades;
using EM.Domain.Enums;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace EM.Repository
{

    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        public List<Aluno> Alunos { get; set; } = new();

        public override void Add(Aluno aluno)
        {
            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();
                FbCommand cmd = new FbCommand($"INSERT INTO ALUNOS (MATRICULA, NOME, SEXO, DATANASCIMENTO, CPF) VALUES({aluno});", _conexao);
                cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public override IEnumerable<Aluno> GetAll()
        {
            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();

                FbCommand cmd = new FbCommand($"SELECT MATRICULA, NOME, SEXO, DATANASCIMENTO, CPF FROM ALUNOS;", _conexao);
                FbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = reader.GetInt32(0);
                    aluno.Nome = reader.GetString(1);
                    aluno.Sexo = (EnumeradorSexo) Enum.Parse(typeof(EnumeradorSexo), reader.GetString(2));
                    aluno.Nascimento = reader.GetDateTime(3);
                    aluno.Cpf = reader.GetString(4);
                    Alunos.Add(aluno);
                }
                return Alunos;
            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public override void Remove(Aluno aluno)
        {
            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();
                FbCommand cmd = new FbCommand($"DELETE FROM ALUNOS WHERE MATRICULA = {aluno.Matricula};", _conexao);
                cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public override void Update(Aluno aluno)
        {
            DataTable dtbAlunoID = new();
            using (FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao())
            {
                _conexao.Open();
                FbDataAdapter fbDados = new FbDataAdapter("UPDATE ALUNOS SET NOME=@NOME, SEXO=@SEXO, DATANASCIMENTO=@DATANASCIMENTO, CPF=@CPF WHERE MATRICULA = @MATRICULA", _conexao);
                fbDados.SelectCommand.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
                fbDados.SelectCommand.Parameters.AddWithValue("@NOME", aluno.Nome);
                fbDados.SelectCommand.Parameters.AddWithValue("@SEXO", aluno.Sexo);
                fbDados.SelectCommand.Parameters.AddWithValue("@DATANASCIMENTO", Convert.ToDateTime(aluno.Nascimento));
                fbDados.SelectCommand.Parameters.AddWithValue("@CPF", aluno.Cpf);
                fbDados.Fill(dtbAlunoID);
            }
            if (dtbAlunoID.Rows.Count == 1)
            {
                aluno.Matricula = Convert.ToInt32(dtbAlunoID.Rows[0][0].ToString());
                aluno.Nome = dtbAlunoID.Rows[0][1].ToString();
                aluno.Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), dtbAlunoID.Rows[0][2].ToString() ?? "Masculino");
                aluno.Nascimento = Convert.ToDateTime(dtbAlunoID.Rows[0][3].ToString());
                aluno.Cpf = dtbAlunoID.Rows[0][4].ToString();
            }
        }

        public override IEnumerable<Aluno> Get(Func<Aluno, bool> predicate)
        {
            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();

                FbCommand cmd = new FbCommand($"SELECT MATRICULA, NOME, SEXO, DATANASCIMENTO, CPF FROM ALUNOS;", _conexao);
                FbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = reader.GetInt32(0);
                    aluno.Nome = reader.GetString(1);
                    aluno.Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader.GetString(2));
                    aluno.Nascimento = reader.GetDateTime(3);
                    aluno.Cpf = reader.GetString(4);
                    Alunos.Add(aluno);
                }
                return Alunos.Where(predicate);
            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }
        public Aluno GetByMatricula(int mat)
        {
            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();

                FbCommand cmd = new FbCommand($"SELECT MATRICULA, NOME, SEXO, DATANASCIMENTO, CPF FROM ALUNOS WHERE MATRICULA = {mat};", _conexao);
                FbDataReader reader = cmd.ExecuteReader();
                
                Aluno aluno = new();
                while (reader.Read())
                {
                    aluno.Matricula = reader.GetInt32(0);
                    aluno.Nome = reader.GetString(1);
                    aluno.Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader.GetString(2));
                    aluno.Nascimento = reader.GetDateTime(3);
                    aluno.Cpf = reader.GetString(4);
                }
                
                return aluno;
                
            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome)
        {

            FbConnection _conexao = ConexaoFirebird.getInstancia().getConexao();
            try
            {
                _conexao.Open();

                FbCommand cmd = new FbCommand($"SELECT MATRICULA, NOME, SEXO, DATANASCIMENTO, CPF FROM ALUNOS WHERE NOME LIKE '%{parteDoNome}%';", _conexao);
                FbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = reader.GetInt32(0);
                    aluno.Nome = reader.GetString(1);
                    aluno.Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader.GetString(2));
                    aluno.Nascimento = reader.GetDateTime(3);
                    aluno.Cpf = reader.GetString(4);
                    Alunos.Add(aluno);
                }
                return Alunos;

            }
            catch (FbException ex)
            {
                throw ex;
            }
            finally
            {
                _conexao.Close();
            }
        }
    
    }
}
