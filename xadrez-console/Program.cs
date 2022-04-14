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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimePartida(partida);


                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.ValidaPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.ValidaPosicaoDestino(origem, destino);
                        partida.RealizaJogada(origem, destino);
                    }

                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
