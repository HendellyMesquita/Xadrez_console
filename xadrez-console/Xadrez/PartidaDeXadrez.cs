using System;
using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool FimPartida { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Ciano;
            FimPartida = false;
            colocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.retirarPeca(origem);
            peca.IncrementaMovimento();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.colocarPeca(peca, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            ProximoPlayer();
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
            
            if (!Tabuleiro.peca(origem).verificaDestino(destino))
                throw new TabuleiroException("Impossivel mover peça para campo selecionado");
        }


        private void colocarPecas()
        {
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Ciano), new ConverterPosicao('c', 1).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Ciano), new ConverterPosicao('c', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Ciano), new ConverterPosicao('d', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Ciano), new ConverterPosicao('e', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Ciano), new ConverterPosicao('e', 1).toPosicao());
            Tabuleiro.colocarPeca(new Rei(Tabuleiro, Cor.Ciano), new ConverterPosicao('d', 1).toPosicao());

            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Azul), new ConverterPosicao('e', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Azul), new ConverterPosicao('c', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Azul), new ConverterPosicao('d', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Azul), new ConverterPosicao('c', 8).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Tabuleiro, Cor.Azul), new ConverterPosicao('e', 8).toPosicao());
            Tabuleiro.colocarPeca(new Rei(Tabuleiro, Cor.Azul), new ConverterPosicao('d', 8).toPosicao());
        }
    }
}
