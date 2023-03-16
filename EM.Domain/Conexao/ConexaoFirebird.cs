using EM.Domain.Entidades;
using EM.Domain.Enums;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using System.Data;

namespace EM.Domain.Conexao
{
    public class ConexaoFirebird
    {
        private static readonly ConexaoFirebird instanciaFireBird = new();
        public ConexaoFirebird() { }

        public static ConexaoFirebird getInstancia()
        {
            return instanciaFireBird;
        }

        public FbConnection getConexao()
        {
            string conn = "User=SYSDBA;Password=masterkey;Database=C:\\Banco_EX\\DB_ALUNOS.FDB;DataSource=localhost;Port=3050;Dialect=3;";
            string conn2 = "User=SYSDBA;Password=masterkey;Database=C:\\Banco_EX\\DB_ALUNOS.FDB;DataSource=localhost;Port=3054;Dialect=3;";

            FbConnection conexao1 = new FbConnection(conn);
            if(conexao1 == null)
            {
                FbConnection conexao2 = new FbConnection(conn2);
                return conexao2;
            } else {
                return conexao1;
            }

            
        }
 
    }
}
