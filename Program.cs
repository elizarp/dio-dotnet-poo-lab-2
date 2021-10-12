﻿using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("	<<<< Obrigado por utilizar nossos serviços. >>>>");
			Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			Console.Write("	Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());
            Console.WriteLine("\n	<<<< Série excluida com sucesso >>>>");
			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("	Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("	Digite o id da série que deseja atualizar: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

			Console.Write("	Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());
			while (entradaGenero <= 0 || entradaGenero > 13)
			{
				Console.WriteLine("\n	<<<< Não existe essa opção cadastrada! >>>>\n");
				Console.Write("	Digite o gênero entre as opções acima: ");
				entradaGenero = int.Parse(Console.ReadLine());
			}

			Console.Write("	Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("	Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());
			int ano = DateTime.Now.Year;
			while (entradaAno <= 0 || entradaAno > ano)
			{
				Console.WriteLine("\n	<<<< Você precisa digitar um ano válido >>>>");
				Console.Write("\n	Digite novamente o ano de Início da Série: ");
				entradaAno = int.Parse(Console.ReadLine());
			}

			Console.Write("	Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();
			Console.WriteLine("\n	<<<< Série atualizada com sucesso!! >>>>");

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);
			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("	<<<< Listar séries >>>>");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("  Você ainda não cadastou nenhuma série...\n" + "  digite a opção << 2 >> e cadastre uma nova.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("	<<<< Inserir nova série >>>>\n");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
           
			Console.Write("	Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());
			while (entradaGenero <= 0 || entradaGenero > 13)
			{
                Console.WriteLine("\n	<<<< Não existe essa opção cadastrada! >>>>\n");
				Console.Write("	Digite o gênero entre as opções acima: ");
				entradaGenero = int.Parse(Console.ReadLine());
			}
			Console.Write("	Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("	Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());			
			int ano = DateTime.Now.Year;
			while (entradaAno <= 0 || entradaAno > ano)
            {
                Console.WriteLine("\n	<<<< Você precisa digitar um ano válido >>>>");
				Console.Write("\n	Digite novamente o ano de Início da Série: ");
				entradaAno = int.Parse(Console.ReadLine());
			}

			Console.Write("	Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Console.WriteLine("\n	<<<< Série cadastrada com sucesso >>>>");

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("================================================");
			Console.WriteLine("	DIO Séries a seu dispor!!!");
			Console.WriteLine("================================================");

			
			Console.WriteLine("	1- Listar séries");
			Console.WriteLine("	2- Inserir nova série");
			Console.WriteLine("	3- Atualizar série");
			Console.WriteLine("	4- Excluir série");
			Console.WriteLine("	5- Visualizar série");
			Console.WriteLine("	C- Limpar Tela");
			Console.WriteLine("	X- Sair");

			Console.Write("	Informe a opção desejada: ");

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
