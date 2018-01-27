using System;
using Tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez {
    class PartidaDeXadrez {
        public Tabuleiro.Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> Pecas;//Coleção que obedece regras de conjunto.
        private HashSet<Peca> PecasCapturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro.Tabuleiro(8, 8);
            Turno = 1;
            terminada = false;
            Xeque = false;
            JogadorAtual = Cor.Branco;
            //Antes de colocar peças, instanciar conjuntos.
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao inicial, Posicao final) {
            Peca p = Tab.RetirarPeca(inicial);
            p.IncrementarQteMovimentos();
            Peca PecaCapturada = Tab.RetirarPeca(final);
            Tab.ColocarPecas(p, final);
            if (PecaCapturada != null) {
                PecasCapturadas.Add(PecaCapturada);
            }
            return PecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca PecaCapturada) {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if (PecaCapturada != null) {
                Tab.ColocarPecas(PecaCapturada, destino);
                PecasCapturadas.Remove(PecaCapturada);
            }
            Tab.ColocarPecas(p, origem);
        }

        public void RealizarJogada(Posicao origem, Posicao destino) {
            Peca PecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, PecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else {
                Xeque = false;
            }

            if (TesteChequeMate(Adversaria(JogadorAtual))) {
                terminada = true;
            }
            else {
                Turno++;
                MudarJogador();
            }
        }

        public bool TesteChequeMate(Cor cor) {
            if (!EstaEmXeque(cor)) {
                return false;
            }
            //colocar o foreach varrendo as peças.
            foreach (Peca x in PecasEmJogo(cor)) {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++) {
                    for (int j = 0; j < Tab.Colunas; j++) {
                        if (mat[i, j] == true) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca PecaCapturada = ExecutarMovimento(origem, destino);
                            /*fazemos o movimento para a posição i,j e vamos verificar
                             * se isso tira o rei do cheque, caso nenhuma posição
                             * tirar o rei do cheque, aí meu filho, acabou.*/
                            bool testecheque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, PecaCapturada);
                            if (!testecheque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Peca> Capturadas(Cor cor) {
            HashSet<Peca> Aux = new HashSet<Peca>();
            foreach (Peca x in PecasCapturadas) {
                if (x.Cor == cor) {
                    Aux.Add(x);
                }
            }
            return Aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor) {
            HashSet<Peca> Aux = new HashSet<Peca>();
            foreach (Peca x in Pecas) {
                if (x.Cor == cor) {
                    Aux.Add(x);
                }
            }
            Aux.ExceptWith(Capturadas(cor));
            return Aux;
        }

        private void MudarJogador() {
            if (JogadorAtual == Cor.Preto) {
                JogadorAtual = Cor.Branco;
            }
            else {
                JogadorAtual = Cor.Preto;
            }
        }

        public bool EstaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna] == true) {
                    return true;
                }
            }
            return false;
        }

        private Peca Rei(Cor cor) {
            foreach (Peca x in PecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        private Cor Adversaria(Cor cor) {
            if (cor == Cor.Branco) {
                return Cor.Preto;
            }
            else {
                return Cor.Branco;
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos) {
            if (Tab.Peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor) {
                throw new TabuleiroException("Esta peça não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis()) {
                throw new TabuleiroException("Não existem movimentos possíveis para a peça escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!Tab.Peca(origem).MovimentoPossivel(destino)) {//Se for FALSE, ocorre a exceção.
                throw new TabuleiroException("Posição inválida!");
            }
        }

        public void ColocarNovaPeca(char Coluna, int Linha, Peca Peca) {
            Tab.ColocarPecas(Peca, new PosicaoXadrez(Coluna, Linha).ToPosicao());
            Pecas.Add(Peca);
        }

        private void ColocarPecas() {
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branco));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preto));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preto));
        }

    }
}
