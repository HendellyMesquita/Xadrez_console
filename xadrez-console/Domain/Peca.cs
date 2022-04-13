namespace xadrez_console.Domain
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimento { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QteMovimento = 0;
        }
        public void incrementaMovimento()
        {
            QteMovimento++;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
