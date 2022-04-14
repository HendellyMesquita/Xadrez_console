using System;
using System.Collections.Generic;
using xadrez_console.Domain;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    class Tela
    {

        public static void ImprimePartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            imprimePecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);

            if (!partida.FimPartida)
            {
                Console.WriteLine(partida.JogadorAtual + ". Sua Vez");
                if (partida.Xeque)
                    Console.WriteLine("Em Xeque");
            }
            else
            {
                Console.WriteLine("XEQUEMATE");
                Console.WriteLine("VENCEDOR" + partida.JogadorAtual);
            }
        }

        private static void imprimePecasCapturadas(PartidaDeXadrez partida)
        {
            ConsoleColor aux = Console.ForegroundColor;

            Console.WriteLine("Peças capturadas: ");
            Console.Write("Azuis: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            ImprimeConjunto(partida.SeparaPecasCapituradas(Cor.Azul));
            Console.ForegroundColor = aux;

            Console.WriteLine();
            Console.Write("Ciano: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            ImprimeConjunto(partida.SeparaPecasCapituradas(Cor.Ciano));
            Console.ForegroundColor = aux;
            Console.WriteLine();

        }

        private static void ImprimeConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca pesacap in conjunto)
            {
                Console.Write(pesacap + " ");
            }
            Console.Write("]");

        }

        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");
                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    imprimirPeca(tab.peca(linha, coluna));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H ");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoePossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");
                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    if (posicoePossiveis[linha, coluna])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(linha, coluna));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H ");
            Console.BackgroundColor = fundoOriginal;
        }

        public static ConverterPosicao lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new ConverterPosicao(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;

            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Ciano)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write(" ");
            }
        }

    }
}
