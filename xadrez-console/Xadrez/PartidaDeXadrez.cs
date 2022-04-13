using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool FimPartida { get; private set; }
        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            ColocaPecas();
            FimPartida = false;
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.retirarPeca(origem);
            peca.incrementaMovimento();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.colocaPeca(peca, destino);
        }
        private void ColocaPecas()
        {
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Branco), new ConverterPosicao('c', 1).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Branco), new ConverterPosicao('c', 2).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Branco), new ConverterPosicao('d', 2).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Branco), new ConverterPosicao('e', 2).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Branco), new ConverterPosicao('e', 1).toPosicao());
            Tabuleiro.colocaPeca(new Rei(Tabuleiro, Cor.Branco), new ConverterPosicao('d', 1).toPosicao());

            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Preto), new ConverterPosicao('c', 7).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Preto), new ConverterPosicao('c', 8).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Preto), new ConverterPosicao('d', 7).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Preto), new ConverterPosicao('e', 7).toPosicao());
            Tabuleiro.colocaPeca(new Torre(Tabuleiro, Cor.Preto), new ConverterPosicao('e', 8).toPosicao());
            Tabuleiro.colocaPeca(new Rei(Tabuleiro, Cor.Preto), new ConverterPosicao('d', 8).toPosicao());


        }
    }
}
