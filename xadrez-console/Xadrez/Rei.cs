using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class Rei : Peca
    {

        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        private bool Mova_se(Posicao  posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao  posicao = new Posicao(0, 0);

            // acima
             posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // ne
             posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // direita
             posicao.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // se
             posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // abaixo
             posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // so
             posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // esquerda
             posicao.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            // no
             posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida( posicao) && Mova_se( posicao))
            {
                movimentos[ posicao.Linha,  posicao.Coluna] = true;
            }
            return movimentos;
        }
    }
}
