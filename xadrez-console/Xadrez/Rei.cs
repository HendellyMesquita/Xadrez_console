using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool Mova_se(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }



        private bool TorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QteMovimentos == 0;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // acima
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // ne
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // direita
            posicao.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // se
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // abaixo
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // so
            posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // esquerda
            posicao.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // no
            posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }

            //#jogadaespecial
            if (QteMovimentos == 0 && !Partida.Xeque)
            {
                Posicao RoquePequeno = new Posicao(posicao.Linha, posicao.Coluna + 3);
                if (TorreParaRoque(RoquePequeno))
                {
                    Posicao verificacasa1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao verificacasa2 = new Posicao(posicao.Linha, posicao.Coluna + 2);

                    if (Tabuleiro.peca(verificacasa1) == null && Tabuleiro.peca(verificacasa2) == null)
                    {
                        movimentos[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }
            }
            Posicao RoqueGrande = new Posicao(posicao.Linha, posicao.Coluna + 4);
            if (TorreParaRoque(RoqueGrande))
            {
                Posicao verificacasa1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                Posicao verificacasa2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                Posicao verificacasa3 = new Posicao(posicao.Linha, posicao.Coluna - 3);

                if (Tabuleiro.peca(verificacasa1) == null && Tabuleiro.peca(verificacasa2) == null && Tabuleiro.peca(verificacasa3) == null)
                {
                    movimentos[posicao.Linha, posicao.Coluna + 2] = true;
                }
            }
            return movimentos;
        }
    }
}

