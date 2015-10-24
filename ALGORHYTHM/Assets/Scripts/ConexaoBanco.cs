using System.Data;
using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Classe C# para acessar objetos SQLite
 * Para usar voce tem que ter certeza de ter COPIADO Mono.Data.SqliteClient.dll para a pasta
 * Assets de seu projeto.
 * Originally created by dklompmaker in 2009
 * Modified 2011 by Alan Chatham
 * Modified 2014 by Shinsuke Sugita
 * Traduzido em portugues e reescrito de JavaScript (UnityScript) para C# por: Alan Antonio Pereira 2015
 *   
*/

public class ConexaoBanco {

	//Variaveis para acesso a query basicas. 
	private string conexao;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader; //leitor de dados
	
	public void AbrirBanco(string caminho)
	{
		//conexao = "URI=file:" + caminho; //nos colocamos o caminho para conectar ao nosso banco
		//conexao = caminho;
		//conexao = "URI=file:" + caminho + ", version=3, password=123"; //mudou nada ;-;
		dbcon = new SqliteConnection(/*conexao*/caminho);
		Debug.Log (dbcon.ConnectionString);
		dbcon.Open();
	}

	public IDataReader QueryBasica(string query/*, bool ler*/) //executa uma query SQLite basica
	{
		dbcmd = dbcon.CreateCommand(); //cria um comando vazio
		dbcmd.CommandText = query; //preenche o comando
		reader = dbcmd.ExecuteReader(); //executa o comando cujo retorna um IDataReader
		//if(ler) //se quisermos retornar os dados do reader
		//{
		return reader; //retorna o nosso reader
		//}
	}
	
	//retora uma ArrayList de 2 dimensoes com todos os dados da tabela requisitada.
	public ArrayList LerTabelaToda(string nomeTabela)
	{
		string query;
		query = "SELECT * FROM " + nomeTabela;
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		ArrayList readLista = new ArrayList();
		while(reader.Read())
		{
			ArrayList lineLista = new ArrayList();
			for(int i = 0; i < reader.FieldCount; i++)
				lineLista.Add (reader.GetValue(i)); //Le os dados em uma linha
			readLista.Add(lineLista); //faz uma lista de todas as linhas
		}
		return readLista; //retorna a lista
	}
	
	//Exclui todo o conteudo de uma tabela
	public void ExcluiConteudoTabela(string nomeTabela)
	{
		string query;
		query = "DELETE FROM " + nomeTabela;
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
	}
	
	//Cria uma tabela com suas devidas colunas
	public void CriarTabela(string nome, ArrayList colunas, ArrayList tipoColunas)
	{
		string query;
		query = "CREATE TABLE IF NOT EXISTS "+ nome + "(" + colunas[0] + " " + tipoColunas[0];
		for(int i=1; i < colunas.Count; i++)
		{
			query += ", " + colunas[i] + " " + tipoColunas[i];
		}
		query+=")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
	}
	
	public string CriarTabelaRetornaQuery(string nome, ArrayList colunas, ArrayList tipoColunas)
	{
		string query;
		//DROP TABLE "+nome+"\n
		query = "CREATE TABLE IF NOT EXISTS "+ nome + "(" + colunas[0] + " " + tipoColunas[0];
		for(int i=1; i < colunas.Count; i++)
		{
			query += ", " + colunas[i] + " " + tipoColunas[i];
		}
		query+=")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		return query;
	}
	
	//Basico Insert into com apenas os valores
	public void Incluir(string nomeTabela, ArrayList valores)
	{
		string query;
		query = "INSERT INTO " + nomeTabela + " VALUES (" + valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", " + valores[i];
		}
		query+= ")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
	}

	public void AlterarPorID(string nomeTabela, ArrayList colunas, ArrayList valores, string colunaID, string valorID)
	{
		string query;
		query = "UPDATE " + nomeTabela + " SET "+colunas[0]+"="+ valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", "+colunas[i]+"="+ valores[i];
		}
		query+= " WHERE "+colunaID+"="+valorID;
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		Debug.Log (query);
		reader = dbcmd.ExecuteReader();
	}

	public void AlterarPorIDComposto(string nomeTabela, ArrayList colunas, ArrayList valores, string colunaID, string colunaFK, string valorID, string valorFK)
	{
		string query;
		query = "UPDATE " + nomeTabela + " SET "+colunas[0]+"="+ valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", "+colunas[i]+"="+ valores[i];
		}
		query += " WHERE " + colunaID + "=" + valorID + " AND " + colunaFK + "=" + valorFK; //UPDATE tabela SET coluna=valor WHERE chave=valorChave AND chave2 = valorChave2
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		Debug.Log (query);
		reader = dbcmd.ExecuteReader();
	}
	
	public string IncluirRetornaQuery(string nomeTabela, ArrayList valores)
	{
		string query;
		query = "INSERT INTO " + nomeTabela + " VALUES (" + valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", " + valores[i];
		}
		query+= ")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		return query;
	}
	
	//Incluir em Colunas Especificas
	public void IncluirEspecifico(string nomeTabela, ArrayList colunas, ArrayList valores)
	{
		string query;
		query = "INSERT INTO " + nomeTabela + "(" + colunas[0];
		for(int i=1; i < colunas.Count; i++)
		{
			query += ", " + colunas[i];
		}
		query+=") VALUES(" + valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", " + valores[i];
		}
		query+=")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
	}
	
	//Incluir em Colunas Especificas, retorna ID do ultimo registro inserido
	public ArrayList IncluirEspecificoRetornaId(string nomeTabela, string nomeColunaId, ArrayList colunas, ArrayList valores)
	{
		//ArrayList listaRetorno = new ArrayList();
		
		string query;
		query = "INSERT INTO " + nomeTabela + "(" + colunas[0];
		for(int i=1; i < colunas.Count; i++)
		{
			query += ", " + colunas[i];
		}
		query+=") VALUES(" + valores[0];
		for(int i=1; i < valores.Count; i++)
		{
			query+= ", " + valores[i];
		}
		query+=")";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		
		//Query para pegar ultimo registro
		/*
		 * 
		query = "select * from "+ nomeTabela +" order by "+ nomeColunaId +" desc limit 1";
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		while(reader.Read())
		{
			ArrayList lineLista = new ArrayList();
			for(int i = 0; i < reader.FieldCount; i++)
				lineLista.Add (reader.GetValue(i)); //Le os dados em uma linha, um registro de um Objeto no banco
			listaRetorno.Add(lineLista); //faz uma lista de todas as linhas, de todos os Objetos recebidos;
		}

		 */
		return LerTabelaToda(nomeTabela+" order by "+ nomeColunaId +" desc limit 1");
	}
	
	//Essa funçao le uma unica coluna
	//whereColuna e a Coluna, whereOperador e o operdor de comparaçao que quer usar
	//e o whereValor e o valor a se comparar.
	//Seleciona um unico registro;
	public ArrayList SelecionaUnicoWhere(string nomeTabela, string itemToSelect, string whereColuna, string whereOperador, string whereValor)
	{
		string query;
		query = "SELECT " + itemToSelect + " FROM " + nomeTabela + " WHERE " + whereColuna + whereOperador + whereValor; 
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query; 
		reader = dbcmd.ExecuteReader();
		//var readArray = new Array();
		ArrayList readArray = new ArrayList();
		//while(reader.Read()) { 
//			//readArray.Push(reader.GetString(0)); // Fill array with all matches
//			string japanese = reader.GetString(0);
//			Debug.Log(japanese);
//			readArray.Add(japanese); // Fill array with all matches
//			string url = reader.GetString(1);
//			Debug.Log(url);
//			readArray.Add(url); // Fill array with all matches
		//}
		while(reader.Read())
		{
			ArrayList lineLista = new ArrayList();
			for(int i = 0; i < reader.FieldCount; i++)
				lineLista.Add (reader.GetValue(i)); //Le os dados em uma linha
		}
		return readArray; // return matches
	}
	
	//fecha tudo relacionado a conexao
	public void FecharBanco()
	{
		reader.Close (); //limpa todos dados do leitor
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbcon.Close ();
		dbcon = null;
	}

}//FIM ConexaoBanco.cs
