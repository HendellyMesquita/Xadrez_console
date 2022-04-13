using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro Tabuleiro, Cor Cor) : base(Tabuleiro, Cor)
        {
        }

        public override string ToString()
        {
            return "T";
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

            // acima
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.posicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha = posicao.Linha - 1;
            }

            // abaixo
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.posicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha = posicao.Linha + 1;
            }

            // direita
            posicao.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.posicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna + 1;
            }

            // esquerda
            posicao.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.posicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna = posicao.Coluna - 1;
            }

            return movimentos;
        }
    }
}
