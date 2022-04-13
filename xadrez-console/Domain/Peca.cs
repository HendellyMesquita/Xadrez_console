namespace xadrez_console.Domain
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Tabuleiro = tabuleiro;
            Cor = cor;

            QteMovimentos = 0;
        }

        public void IncrementaMovimento()
        {
            QteMovimentos++;
        }

        public bool VerificaMovimento()
        {
            bool[,] movimentos = MovimentosPossiveis();
            for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                {
                    if (movimentos[linha, coluna])
                        return true;
                }
            }
            return false;
        }
        public bool verificaDestino(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
