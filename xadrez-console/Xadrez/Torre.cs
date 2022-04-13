using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }
        public override string ToString()
        {
            return "T";
        }
        private bool mova_se(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);


            //cima
            Posicao.DefinaValores(posicao.Linha - 1, posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                    break;

                posicao.Linha = posicao.Linha - 1;
            }

            //direita
            Posicao.DefinaValores(posicao.Linha, posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                    break;

                posicao.Coluna = posicao.Coluna + 1;
            }

            // baixo
            Posicao.DefinaValores(posicao.Linha + 1, posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                    break;

                posicao.Linha = posicao.Linha + 1;
            }

            //  esquerda
            Posicao.DefinaValores(posicao.Linha, posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.peca(posicao) != null && Tabuleiro.peca(posicao).Cor != Cor)
                    break;

                posicao.Coluna = posicao.Coluna - 1;
            }

            return movimentos;
        }
    }
}
