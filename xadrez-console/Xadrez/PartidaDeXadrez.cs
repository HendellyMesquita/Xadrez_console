using System.Collections.Generic;
using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool FimPartida { get; private set; }
        private HashSet<Peca> PecasEmJogo;
        private HashSet<Peca> PecasCapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Ciano;
            FimPartida = false;
            Xeque = false;
            VulneravelEnPassant = null;

            PecasEmJogo = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementaMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementaMovimento();
                Tabuleiro.colocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementaMovimento();
                Tabuleiro.colocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Ciano)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tabuleiro.RetirarPeca(posP);
                    PecasCapturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementaMovimento();
            if (pecaCapturada != null)
            {
                Tabuleiro.colocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.colocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementaMovimento();
                Tabuleiro.colocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementaMovimento();
                Tabuleiro.colocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Ciano)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.colocarPeca(peao, posP);
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

            Peca p = Tabuleiro.peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cor == Cor.Ciano && destino.Linha == 0) || (p.Cor == Cor.Azul && destino.Linha == 7))
                {
                    p = Tabuleiro.RetirarPeca(destino);
                    PecasEmJogo.Remove(p);
                    Peca dama = new Dama(Tabuleiro, p.Cor);
                    Tabuleiro.colocarPeca(dama, destino);
                    PecasEmJogo.Add(dama);
                }
            }
          

            if (EmXeque(Adversario(JogadorAtual)))
                Xeque = true;

            else
                Xeque = false;

            if (EmXeque(Adversario(JogadorAtual)))
                FimPartida = true;

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

        public void ValidaPosicaoOrigem(Posicao posicao)
        {
            if (Tabuleiro.peca(posicao) == null)
                throw new TabuleiroException("Nenhuma paça selecionada");

            if (JogadorAtual != Tabuleiro.peca(posicao).Cor)
                throw new TabuleiroException("Inpossivel selecionar peças do adiversario");

            if (!Tabuleiro.peca(posicao).VerificaMovimento())
                throw new TabuleiroException("Movimentos indisponíveis para peça selecionada");
        }

        public void ValidaPosicaoDestino(Posicao origem, Posicao destino)
        {

            if (!Tabuleiro.peca(origem).VerificaDestino(destino))
                throw new TabuleiroException("Impossivel mover peça para campo selecionado");
        }


        private void ProximoPlayer()
        {
            if (JogadorAtual == Cor.Ciano)
                JogadorAtual = Cor.Azul;

            else
                JogadorAtual = Cor.Ciano;
        }

        public HashSet<Peca> SeparaPecasCapituradas(Cor cor)
        {
            HashSet<Peca> HashsetCapturada = new HashSet<Peca>();
            foreach (Peca pecaCap in PecasCapturadas)
            {
                if (pecaCap.Cor == cor)
                {
                    HashsetCapturada.Add(pecaCap);
                }
            }
            return HashsetCapturada;
        }

        public HashSet<Peca> SeparaPecasEmjogo(Cor cor)
        {
            HashSet<Peca> HashsetEmJogo = new HashSet<Peca>();
            foreach (Peca pecaJogo in PecasEmJogo)
            {
                if (pecaJogo.Cor == cor)
                {
                    HashsetEmJogo.Add(pecaJogo);
                }
            }
            HashsetEmJogo.ExceptWith(SeparaPecasCapituradas(cor));
            return HashsetEmJogo;
        }

        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Ciano)
            {
                return Cor.Azul;
            }
            else
            {
                return Cor.Ciano;
            }
        }

        private Peca ReiCor(Cor cor)
        {
            foreach (Peca peca in SeparaPecasEmjogo(cor))
            {
                if (peca is Rei)
                    return peca;
            }
            return null;
        }

        public bool EmXeque(Cor cor)
        {
            Peca R = ReiCor(cor);

            if (R == null)
            {
                throw new TabuleiroException(cor + "Seu Rei não está mais em campo. XequeMate");
            }

            foreach (Peca peca in SeparaPecasEmjogo(Adversario(cor)))
            {
                bool[,] movimentos = peca.MovimentosPossiveis();
                if (movimentos[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EmXequemate(Cor cor)
        {
            if (!EmXeque(cor))
            {
                return false;
            }
            foreach (Peca peca in SeparaPecasEmjogo(cor))
            {
                bool[,] matposicao = peca.MovimentosPossiveis();
                for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                    {
                        if (matposicao[linha, coluna])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(linha, coluna);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
                    }

                }
            }
            return true;
        }
        public void NovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.colocarPeca(peca, new ConverterPosicao(coluna, linha).toPosicao());
            PecasEmJogo.Add(peca);
        }
        private void colocarPecas()
        {

            NovaPeca('a', 1, new Torre(Tabuleiro, Cor.Ciano));
            NovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Ciano));
            NovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Ciano));
            NovaPeca('d', 1, new Dama(Tabuleiro, Cor.Ciano));
            NovaPeca('e', 1, new Rei(Tabuleiro, Cor.Ciano, this));
            NovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Ciano));
            NovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Ciano));
            NovaPeca('h', 1, new Torre(Tabuleiro, Cor.Ciano));
            NovaPeca('a', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('b', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('c', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('d', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('e', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('f', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('g', 2, new Peao(Tabuleiro, Cor.Ciano, this));
            NovaPeca('h', 2, new Peao(Tabuleiro, Cor.Ciano, this));

            NovaPeca('a', 8, new Torre(Tabuleiro, Cor.Azul));
            NovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Azul));
            NovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Azul));
            NovaPeca('d', 8, new Dama(Tabuleiro, Cor.Azul));
            NovaPeca('e', 8, new Rei(Tabuleiro, Cor.Azul, this));
            NovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Azul));
            NovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Azul));
            NovaPeca('h', 8, new Torre(Tabuleiro, Cor.Azul));
            NovaPeca('a', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('b', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('c', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('d', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('e', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('f', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('g', 7, new Peao(Tabuleiro, Cor.Azul, this));
            NovaPeca('h', 7, new Peao(Tabuleiro, Cor.Azul, this));
        }
    }
}
