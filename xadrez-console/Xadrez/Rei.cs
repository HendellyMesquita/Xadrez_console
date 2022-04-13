using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    public class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }
        public override string ToString()
        {
            return "R";
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
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            // cima direita
            Posicao.DefinaValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            //direita
            Posicao.DefinaValores(posicao.Linha, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            // baixo direita
            Posicao.DefinaValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            // baixo
            Posicao.DefinaValores(posicao.Linha + 1, posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            // baixo esquerda
            Posicao.DefinaValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            //  esquerda
            Posicao.DefinaValores(posicao.Linha, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            //cima direita
            Posicao.DefinaValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && mova_se(posicao))
                movimentos[posicao.Linha, posicao.Coluna] = true;

            return movimentos;
        }


    }
}
