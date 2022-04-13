using System;
using xadrez_console.Domain;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.FimPartida)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tabuleiro);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicao().toPosicao();

                    bool[,] posicoesPossiveis = partida.Tabuleiro.peca(origem).MovimentosPossiveis();
               
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicao().toPosicao();

                    partida.ExecutaMovimento(origem, destino);
                }

                Tela.imprimirTabuleiro(partida.Tabuleiro);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

            //ConverterPosicao convertPosicao = new ConverterPosicao('a', 1);
            //Console.WriteLine(convertPosicao);
            //Console.WriteLine(convertPosicao.toPosicao());
            //Console.ReadLine();
        }
    }
}
