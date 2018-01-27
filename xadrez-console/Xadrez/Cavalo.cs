using Tabuleiro;

namespace Xadrez {
    class Cavalo : Peca {
        public Cavalo(Tabuleiro.Tabuleiro Tab, Cor cor) : base(Tab, cor) {
        }

        public override string ToString() {
            return "C";
        }

        public bool PodeMover(Posicao pos) {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);
            //Esquerda pra cima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Esquerda pra baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Norte pra esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Norte pra direita
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Direita pra cima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Direita pra baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sul para esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sul para direita
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            return mat;
        }
    }
}
