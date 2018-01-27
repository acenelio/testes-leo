using Tabuleiro;

namespace Xadrez {
    class Peao : Peca {
        public Peao(Tabuleiro.Tabuleiro Tab, Cor cor) : base(Tab, cor) {

        }

        public override string ToString() {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos) {
            Peca p = Tab.Peca(pos);
            return p != null && p.Cor != Cor;
            /* se a peça não for nula (quer dizer que tem
             * alguém lá, e se ela for de cor diferente,
             * quer dizer: existe inimigo.*/
        }

        private bool Livre(Posicao pos) {
            return Tab.Peca(pos) == null;
        }
        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branco) {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            if (Cor == Cor.Preto) {

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            return mat;
        }
    }
}
