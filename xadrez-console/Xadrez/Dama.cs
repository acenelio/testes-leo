using Tabuleiro;

namespace Xadrez {
    class Dama : Peca {
        public Dama(Tabuleiro.Tabuleiro Tab, Cor cor) : base(Tab, cor) {

        }

        public override string ToString() {
            return "D";
        }

        private bool PodeMover(Posicao pos) {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);
            //Norte
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            }

            //Nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            }

            //Leste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            }

            //Sudeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            }

            //Sul
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            }

            //Sudoeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }

            //Oeste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            }

            //Noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.VerificarPosicao(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }
            return mat;
        }
    }
}
