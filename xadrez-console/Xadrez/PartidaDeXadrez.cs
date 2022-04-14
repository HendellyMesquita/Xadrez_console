using System.Collections.Generic;
using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaFinal { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            PartidaFinal = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementaMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementaMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementaMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tabuleiro.RetirarPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.Decrementaovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.Decrementaovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.Decrementaovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não se coloque em Xeque");
            }

            Peca p = Tabuleiro.Peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(Tabuleiro, p.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }

            if (EmXeque(Adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (EmXequemate(Adversario(JogadorAtual)))
            {
                PartidaFinal = true;
            }
            else
            {
                Turno++;
                ProximoPlayer();
            }

            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }

        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tabuleiro.Peca(pos).VerificaMovimento())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).VerificaDestino(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void ProximoPlayer()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        
        public HashSet<Peca> SeparaPecasCapituradas(Cor Cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == Cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        
        public HashSet<Peca> SeparaPecasEmjogo(Cor Cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == Cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(SeparaPecasCapituradas(Cor));
            return aux;
        }

        private Cor Adversario(Cor Cor)
        {
            if (Cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca ReiCor(Cor Cor)
        {
            foreach (Peca x in SeparaPecasEmjogo(Cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EmXeque(Cor Cor)
        {
            Peca R = ReiCor(Cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da Cor " + Cor + " no tabuleiro!");
            }
            foreach (Peca x in SeparaPecasEmjogo(Adversario(Cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EmXequemate(Cor Cor)
        {
            if (!EmXeque(Cor))
            {
                return false;
            }
            foreach (Peca x in SeparaPecasEmjogo(Cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EmXeque(Cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void NovaPeca(char Coluna, int Linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(Coluna, Linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            NovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            NovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            NovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            NovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            NovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            NovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            NovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            NovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            NovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            NovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            NovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            NovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            NovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            NovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            NovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            NovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            NovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            NovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            NovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            NovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
