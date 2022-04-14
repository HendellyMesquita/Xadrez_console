using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez Partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }


        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool livre(Posicao posicao)
        {
            return Tabuleiro.peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Ciano)
            {
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && livre(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && livre(p2) && Tabuleiro.PosicaoValida(posicao) && livre(posicao) && QteMovimentos == 0)
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
            }
            else
            {
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && livre(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && livre(p2) && Tabuleiro.PosicaoValida(posicao) && livre(posicao) && QteMovimentos == 0)
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && existeInimigo(posicao))
                {
                    movimentos[posicao.Linha, posicao.Coluna] = true;
                }

                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        movimentos[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == Partida.VulneravelEnPassant)
                    {
                        movimentos[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return movimentos;
        }
    }
}
