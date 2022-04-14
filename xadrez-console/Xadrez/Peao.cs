using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.Domain;
using xadrez_console.Xadrez;

namespace xadrez_console.xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool livre(Posicao posicao)
        {
            return Tabuleiro.peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Ciano)
            {
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && livre(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && livre(p2) && Tabuleiro.PosicaoValida(posicao) && livre(posicao) && QteMovimentos == 0)
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }

               // // #jogadaespecial en passant
                //if (Posicao.Linha == 3)
                //{
                //    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                //    if (Tabuleiro.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == partida.vulneravelEnPassant)
                //    {
                //        movimentos[esquerda.Linha - 1, esquerda.Coluna] = true;
                //    }
                //    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                //    if (Tabuleiro.PosicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                //    {
                //        movimentos[direita.Linha - 1, direita.Coluna] = true;
                //    }
                //}
            }
            else
            {
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && livre(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && livre(p2) && Tabuleiro.PosicaoValida(posicao) && livre(posicao) && QteMovimentos == 0)
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }

                //// #jogadaespecial en passant
                //if (Posicao.Linha == 4)
                //{
                //    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                //    if (Tabuleiro.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == partida.vulneravelEnPassant)
                //    {
                //        movimentos[esquerda.Linha + 1, esquerda.Coluna] = true;
                //    }
                //    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                //    if (Tabuleiro.PosicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                //    {
                //        movimentos[direita.Linha + 1, direita.Coluna] = true;
                //    }
                //}
            }

            return movimentos;
        }
    }
}
