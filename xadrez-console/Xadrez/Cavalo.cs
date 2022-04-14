using xadrez_console.Domain;

namespace xadrez_console.Xadrez
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

        private bool Mova_se(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }

            return movimentos;
        }
    }
}
