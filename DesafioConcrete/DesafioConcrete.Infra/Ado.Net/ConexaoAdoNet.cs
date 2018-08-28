using DesafioConcrete.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioConcrete.Infra.Ado.Net
{
    public class ConexaoAdoNet
    {
        public static SqlConnection Conexao()
        {                       
            string sConnectionString = @"Data Source=SAMUEL-NOTE\SQLSERVERDESENV;Initial Catalog=BD_Desafio_Concrete;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            return objConn;
        }

        public static Usuario RecuperaUsuario(string id)
        {
            using (var objConn = Conexao())
            {
                objConn.Open();
                Usuario usuario = null;
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand(string.Format("select * from usuario where id = '{0}'", id), objConn);

                SqlDataAdapter daUsuario = new SqlDataAdapter(cmd);
                daUsuario.Fill(dataTable);

                foreach (DataRow data in dataTable.Rows)
                {
                    usuario = new Usuario
                    {
                        DataAtualizacao = (DateTime)data["data_atualizacao"],
                        DataCriacao = (DateTime)data["data_criacao"],
                        Id = data["id"].ToString(),
                        Nome = data["nome"].ToString(),
                        Senha = data["senha"].ToString(),
                        Email = data["email"].ToString(),
                        UltimoLogin = (DateTime)data["ultimo_login"],
                        Token = data["token"].ToString()
                    };

                }

                return usuario;
            }
        }

        public static ICollection<Telefone> RecuperaTelefones(string id)
        {
            using (var objConn = Conexao())
            {
                objConn.Open();
                ICollection<Telefone> telefones = new List<Telefone>();
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand(string.Format("select * from telefone where id_usuario = '{0}'", id), objConn);

                SqlDataAdapter daTelefones = new SqlDataAdapter(cmd);
                daTelefones.Fill(dataTable);

                foreach (DataRow data in dataTable.Rows)
                {
                    telefones.Add(new Telefone
                    {                        
                        DDD = (int)data["ddd"],
                        Numero = (int)data["numero"],                        
                    });
                }

                return telefones;
            }
        }
    }
}