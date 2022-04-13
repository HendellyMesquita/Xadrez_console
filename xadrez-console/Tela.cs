using System;
using xadrez_console.Domain;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    imprimirPeca(tabuleiro.peca(linha, coluna));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (posicoesPossiveis[linha,coluna])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    imprimirPeca(tabuleiro.peca(linha, coluna));
                    Console.BackgroundColor = fundoOriginal;

                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }

        public static ConverterPosicao lerPosicao()
        {
            string comando = Console.ReadLine();
            char coluna = comando[0];
            int linha = int.Parse(comando[1] + "");
            return new ConverterPosicao(coluna, linha);

        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
                Console.Write("_ ");

            else
            {
                ConsoleColor sistema = Console.ForegroundColor;
                if (peca.Cor == Cor.Branco)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = sistema;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = sistema;
                }
                Console.Write(" ");

            }
        }
    }
}