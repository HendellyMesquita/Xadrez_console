using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class Rei : Peca
    {

        private PartidaDeXadrez partida;

        public Rei(Tabuleiro Tabuleiro, Cor Cor, PartidaDeXadrez partida) : base(Tabuleiro, Cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool Mova_se(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QteMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // ne
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // se
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // so
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }
            // no
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && Mova_se(posicao))
            {
                movimentos[posicao.Linha, posicao.Coluna] = true;
            }

            // #jogadaespecial roque
            if (QteMovimentos == 0 && !partida.Xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao RoquePequeno = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TorreParaRoque(RoquePequeno))
                {
                    Posicao verificacasa1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao verificacasa2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tabuleiro.Peca(verificacasa1) == null && Tabuleiro.Peca(verificacasa2) == null)
                    {
                        movimentos[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao RoqueGrande = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TorreParaRoque(RoqueGrande))
                {
                    Posicao verificacasa1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao verificacasa2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao verificacasa3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.Peca(verificacasa1) == null && Tabuleiro.Peca(verificacasa2) == null && Tabuleiro.Peca(verificacasa3) == null)
                    {
                        movimentos[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }


            return movimentos;
        }
    }
}
