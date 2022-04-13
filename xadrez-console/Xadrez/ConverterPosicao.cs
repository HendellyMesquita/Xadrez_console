using xadrez_console.Domain;

namespace xadrez_console.Xadrez
{
    class ConverterPosicao
    {

        public char Coluna { get; set; }
        public int Linha { get; set; }

        public ConverterPosicao(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
