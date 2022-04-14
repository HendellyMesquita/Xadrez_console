using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{

    class Bispo : Peca
    {

        public Bispo(Tabuleiro Tabuleiro, Cor Cor) : base(Tabuleiro, Cor)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool Mova_se(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // NO
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            }

            // NE
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // SE
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // SO
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            }

            return movimentos;
        }
    }
}
