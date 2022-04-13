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

            Posicao  peca = new Posicao(0, 0);

            // acima
             peca.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // ne
             peca.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // direita
             peca.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // se
             peca.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // abaixo
             peca.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // so
             peca.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // esquerda
             peca.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            // no
             peca.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida( peca) && Mova_se( peca))
            {
                movimentos[ peca.Linha,  peca.Coluna] = true;
            }
            return movimentos;
        }
    }
}
