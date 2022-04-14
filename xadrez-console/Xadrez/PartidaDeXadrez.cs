using System.Collections.Generic;
using xadrez_console.Domain;
using xadrez_console.xadrez;

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

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Ciano;
            FimPartida = false;
            Xeque = false;
            PecasEmJogo = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementaMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.colocarPeca(peca, destino);
            if (pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);
            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não se coloque em Xeque");
            }

            if (EmXeque(Advesario(JogadorAtual)))
                Xeque = true;

            else
                Xeque = false;

            if (EmXeque(Advesario(JogadorAtual)))
                FimPartida = true;

            else
            {
                Turno++;
                ProximoPlayer();
            }
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementaMovimento();
            if (pecaCapturada != null)
            {
                Tabuleiro.colocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.colocarPeca(peca, origem);
        }

        private void ProximoPlayer()
        {
            if (JogadorAtual == Cor.Ciano)
                JogadorAtual = Cor.Azul;

            else
                JogadorAtual = Cor.Ciano;
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

        private Cor Advesario(Cor cor)
        {
            if (cor == Cor.Azul)
                return Cor.Ciano;

            else
                return Cor.Azul;
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

            foreach (Peca peca in SeparaPecasEmjogo(Advesario(cor)))
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

            NovaPeca('a', 1, new Torre  (Tabuleiro, Cor.Ciano));
            NovaPeca('b', 1, new Cavalo (Tabuleiro, Cor.Ciano));
            NovaPeca('c', 1, new Bispo  (Tabuleiro, Cor.Ciano));
            NovaPeca('d', 1, new Dama   (Tabuleiro, Cor.Ciano));
            NovaPeca('e', 1, new Rei    (Tabuleiro, Cor.Ciano));
            NovaPeca('f', 1, new Bispo  (Tabuleiro, Cor.Ciano));
            NovaPeca('g', 1, new Cavalo (Tabuleiro, Cor.Ciano));
            NovaPeca('h', 1, new Torre  (Tabuleiro, Cor.Ciano));
            //NovaPeca('e', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('f', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('g', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('h', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('a', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('b', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('c', 2, new Peao   (Tabuleiro, Cor.Ciano));
            //NovaPeca('d', 2, new Peao   (Tabuleiro, Cor.Ciano));
            
            NovaPeca('a', 8, new Torre  (Tabuleiro, Cor.Azul));
            NovaPeca('b', 8, new Cavalo (Tabuleiro, Cor.Azul));
            NovaPeca('c', 8, new Bispo  (Tabuleiro, Cor.Azul));
            NovaPeca('d', 8, new Dama   (Tabuleiro, Cor.Azul));
            NovaPeca('f', 8, new Rei    (Tabuleiro, Cor.Azul));
            NovaPeca('e', 8, new Bispo  (Tabuleiro, Cor.Azul));
            NovaPeca('g', 8, new Cavalo (Tabuleiro, Cor.Azul));
            NovaPeca('h', 8, new Torre  (Tabuleiro, Cor.Azul));
            //NovaPeca('a', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('b', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('c', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('d', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('e', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('f', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('g', 7, new Peao   (Tabuleiro, Cor.Azul));
            //NovaPeca('h', 7, new Peao   (Tabuleiro, Cor.Azul));
        }
    }
}
