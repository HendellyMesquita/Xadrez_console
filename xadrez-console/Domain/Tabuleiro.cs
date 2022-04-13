using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_console.Domain
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca peca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }
        public bool existePeca(Posicao posicao)
        {
            validarPosicao(posicao);
            return peca(posicao) != null;
        }

        public void colocaPeca(Peca peca, Posicao posicao)
        {
            if (existePeca(posicao))
            {
                throw new TabuleiroException("Posição invalida, escolha um campo vazio");
            }
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
        public Peca retirarPeca(Posicao posicao)
        {
            if(peca(posicao) == null)
            {
                return null;
            }
            Peca auxilar = peca(posicao);
            auxilar.Posicao = null;
            Pecas[posicao.Linha, posicao.Coluna] = null;
            return auxilar;

        }
        public bool PosicaoValida(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }
        public void validarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição Inexistente!, escolha uma posição válida");
            }
        }
    }
}
