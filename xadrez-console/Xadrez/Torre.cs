using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro Tabuleiro, Cor cor) : base(Tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool Mova_se(Posicao Posicao)
        {
            Peca peca = Tabuleiro.Peca(Posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao Posicao = new Posicao(0, 0);

            // acima
            Posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(Posicao) && Mova_se(Posicao))
            {
                movimentos[Posicao.Linha, Posicao.Coluna] = true;
                if (Tabuleiro.Peca(Posicao) != null && Tabuleiro.Peca(Posicao).Cor != Cor)
                {
                    break;
                }
                Posicao.Linha = Posicao.Linha - 1;
            }

            // abaixo
            Posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(Posicao) && Mova_se(Posicao))
            {
                movimentos[Posicao.Linha, Posicao.Coluna] = true;
                if (Tabuleiro.Peca(Posicao) != null && Tabuleiro.Peca(Posicao).Cor != Cor)
                {
                    break;
                }
                Posicao.Linha = Posicao.Linha + 1;
            }

            // direita
            Posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(Posicao) && Mova_se(Posicao))
            {
                movimentos[Posicao.Linha, Posicao.Coluna] = true;
                if (Tabuleiro.Peca(Posicao) != null && Tabuleiro.Peca(Posicao).Cor != Cor)
                {
                    break;
                }
                Posicao.Coluna = Posicao.Coluna + 1;
            }

            // esquerda
            Posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(Posicao) && Mova_se(Posicao))
            {
                movimentos[Posicao.Linha, Posicao.Coluna] = true;
                if (Tabuleiro.Peca(Posicao) != null && Tabuleiro.Peca(Posicao).Cor != Cor)
                {
                    break;
                }
                Posicao.Coluna = Posicao.Coluna - 1;
            }

            return movimentos;
        }
    }
}
