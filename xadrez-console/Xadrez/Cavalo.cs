using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.Domain;

namespace xadrez_console.xadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro Tabuleiro, Cor Cor) : base(Tabuleiro, Cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.definirValores(posicao.Linha - 1, posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha - 1, posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha + 2, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha + 2, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && podeMover(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }

            return movimentos;
        }
    }
}