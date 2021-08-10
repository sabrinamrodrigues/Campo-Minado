using System;
using System.Collections;

namespace Ivory.TesteEstagio.CampoMinado
{
    class Program
    {
        public static char[,] atualizaMatriz(ArrayList arrayTabuleiro)
        {
            char[,] matriz = new char[9, 9];
            int posicao = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matriz[i, j] = (char)arrayTabuleiro[posicao];
                    posicao += 1;
                }
            }
            return matriz;
        }
        static void Main(string[] args)
        {
            var campoMinado = new CampoMinado();

            int coluna = 0, linha = 0, contaFechados = 0, status = 0;
            string tabuleiro, escolha;
            char[,] matriz = new char[9, 9];

            Console.WriteLine("Início do jogo\n=========");


            while (status != 2) //Enquanto não for game over
            {
                ArrayList arrayTabuleiro = new ArrayList();
                tabuleiro = campoMinado.Tabuleiro; //a string tabuleiro recebe o tabuleiro
                foreach (char i in tabuleiro)
                {
                    arrayTabuleiro.Add(i);
                    if (i != '*' && i != '-' && i != '0' && i != '1' && i != '2' && i != '3' && i != '4' && i != '5' && i != '6' && i != '7' && i != 8)
                    {
                        arrayTabuleiro.Remove(i);
                    }
                }
                matriz = atualizaMatriz(arrayTabuleiro);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Console.Write(matriz[i, j]);
                    }
                    Console.WriteLine();
                }


                //Recebe linha e coluna
                Console.WriteLine("Linha (1 a 9): ");
                try
                { //capturar exceção caso digite qualquer coisa menos os valores 1 a 9
                    linha = Convert.ToInt16(Console.ReadLine());
                    if (linha < 1 | linha > 9)
                    { //se digitou número inteiro mas não está nessa faixa
                        Console.WriteLine("Por favor, digite um valor inteiro dentro da faixa de 1 a 9.");
                    }
                    else
                    {
                        Console.WriteLine("Coluna (1 a 9): ");

                        coluna = Convert.ToInt16(Console.ReadLine());
                        if (coluna < 1 | coluna > 9)
                        { //se digitou número inteiro mas não está nessa faixa
                            Console.WriteLine("Por favor, digite um valor inteiro dentro da faixa de 1 a 9.");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Por favor, digite um valor válido. (Inteiro de 1 a 9)");
                }

                if (linha > 10 || linha < 1)
                {
                    linha = 1;
                }

                if (coluna > 10 || coluna < 1)
                {
                    coluna = 1;
                }


                //Filtrando a linha e coluna que o usuário digitou
                //se a posição atual não está marcada
                linha = linha - 1;
                coluna = coluna - 1;

                if (matriz[linha, coluna] == '-') //se a posição não foi aberta
                {
                    campoMinado.Abrir(linha + 1, coluna + 1);
                    //se a posição atual está no canto superior esquerdo
                    if (linha == 0 && coluna == 0)
                    {
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna + 1] == '-')
                            contaFechados += 1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor for '1'
                        if (contaFechados == 0 && (matriz[linha, coluna + 1] == '1' && matriz[linha + 1, coluna] == '1' && matriz[linha + 1, coluna + 1] == '1'))
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }

                    //se a posição atual está no canto inferior esquerdo*
                    if (linha == 9 && coluna == 0)
                    {
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados += 1;
                        if (matriz[linha - 1, coluna + 1] == '-')
                            contaFechados += 1;
                        //se não tem fechados ao redor do valor, e todos os valores ao redor forem '1'
                        if (contaFechados == 0 && ((matriz[linha - 1, coluna] == '1') && matriz[linha - 1, coluna + 1] == '1' & matriz[linha, coluna + 1] == '1'))
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }
                    //se a posição atual está no canto superior direito*
                    if (linha == 0 && coluna == 8)
                    {//contaFechados
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna - 1] == '-')
                            contaFechados += 1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor for '1'
                        if (contaFechados == 0 && (matriz[linha, coluna - 1] == '1') && (matriz[linha + 1, coluna - 1] == '1') && (matriz[linha + 1, coluna] == '1'))
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                                status = campoMinado.JogoStatus; //Atualiza status do jogo constantemente

                            }
                            else
                                Console.WriteLine("Certo, lembre-se que toda a vizinhança fechada diagonal, vertical e horizontal dos '1' vizinhos desta bomba pode ser aberta :D");

                        }

                        contaFechados = 0;
                    }

                    //se a posição atual está no canto inferior direito*
                    if (linha == 8 && coluna == 8)
                    {
                        if (matriz[linha - 1, coluna - 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados += 1;
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados += 1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && (matriz[linha - 1, coluna - 1] == '1') && (matriz[linha - 1, coluna] == '1') && matriz[linha, coluna - 1] == '1')
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }
                    //se a posição atual está no canto superior fora os cantos esquerdos e direitos*
                    if (linha == 1 && coluna > 1 && coluna < 9)
                    {
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna + 1] == '-')
                            contaFechados += 1;
                        if (matriz[linha + 1, coluna - 1] == '-')
                            contaFechados += 1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && matriz[linha, coluna - 1] == '1' && matriz[linha, coluna + 1] == '1' && matriz[linha + 1, coluna - 1] == '1' && matriz[linha + 1, coluna] == '1' && matriz[linha + 1, coluna + 1] == '1')
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }

                        }
                        contaFechados = 0;
                    }
                    //se a posição atual está no canto inferior fora os cantos esquerdos e direitos*
                    if (linha == 9 && coluna > 1 && coluna < 9)
                    {
                        if (matriz[linha - 1, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna + 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados = +1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && (matriz[linha - 1, coluna - 1] == '1') && (matriz[linha - 1, coluna] == '1') && matriz[linha - 1, coluna + 1] == '1' && matriz[linha, coluna - 1] == '1' && matriz[linha, coluna + 1] == '1')
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;

                    }
                    //se a posição atual está no canto esquerdo fora os cantos superiores e inferiores*
                    if (linha > 1 && linha < 9 && coluna == 1)
                    {
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna + 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna + 1] == '-')
                            contaFechados = +1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && ((matriz[linha - 1, coluna] == '1') && matriz[linha - 1, coluna + 1] == '1' && matriz[linha, coluna + 1] == '1' && matriz[linha + 1, coluna] == '1' && matriz[linha + 1, coluna + 1] == '1'))
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }
                    //se a posição atual está no canto direito fora os cantos superiores e inferiores*
                    if (linha > 1 && linha < 9 && coluna == 9)
                    {
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna - 1] == '-')
                            contaFechados = +1;
                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && (matriz[linha - 1, coluna - 1] == '1') & (matriz[linha - 1, coluna] == '1') & matriz[linha - 1, coluna + 1] == '1' & matriz[linha, coluna - 1] == '1' & matriz[linha, coluna + 1] == '1' & matriz[linha + 1, coluna - 1] == '1' & matriz[linha + 1, coluna] == '1' & matriz[linha + 1, coluna + 1] == '1')
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }
                    //de outra forma, fora dos cantos

                    if (linha > 1 && linha < 8 && coluna > 1 && coluna < 8)
                    {
                        if (matriz[linha - 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna + 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha - 1, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha, coluna + 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna - 1] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna] == '-')
                            contaFechados = +1;
                        if (matriz[linha + 1, coluna + 1] == '-')
                            contaFechados = +1;

                        //se não tem fechados ao redor do valor, e todos valores ao redor forem '1'
                        if (contaFechados == 0 && (matriz[linha - 1, coluna - 1] == '1') && (matriz[linha - 1, coluna] == '1') && matriz[linha - 1, coluna + 1] == '1' && matriz[linha, coluna - 1] == '1' && matriz[linha, coluna + 1] == '1' && matriz[linha + 1, coluna - 1] == '1' && matriz[linha + 1, coluna] == '1' && matriz[linha + 1, coluna + 1] == '1')
                        {
                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                            Console.WriteLine("Deseja arriscar? s/n");
                            escolha = Console.ReadLine();
                            if (escolha == "s")
                            {
                                campoMinado.Abrir(linha + 1, coluna + 1);
                            }
                        }
                        contaFechados = 0;
                    }
                    //----------------------------------------------------------------------------------------------
                    //Outras situações... em que duas casas além da analisada não é fechada
                    //----------------------------------------------------------------------------------------------
                    //se a posição atual está no canto superior esquerdo
                    if (linha == 0 && coluna == 0)
                    {
                        if (matriz[linha, coluna + 2] != '-' && matriz[linha + 1, coluna + 2] != '-' && matriz[linha + 2, coluna] != '-' && matriz[linha + 2, coluna + 2] != '-')
                        {//se duas casas do lado direito e embaixo existem casas abertas
                            if (matriz[linha, coluna + 1] == '1' & matriz[linha + 1, coluna + 1] == '1' & matriz[linha + 1, coluna] == '1' & matriz[linha + 1, coluna + 1] == '1')
                            {//tem pelo menos '1' um uma casa perto vizinha
                                {
                                    Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                    Console.WriteLine("Deseja arriscar? s/n");
                                    escolha = Console.ReadLine();
                                    if (escolha == "s")
                                    {
                                        campoMinado.Abrir(linha + 1, coluna + 1);
                                        status = campoMinado.JogoStatus; //Atualiza status do jogo constantemente

                                    }
                                    else
                                        Console.WriteLine("Certo, lembre-se que toda a vizinhança fechada diagonal, vertical e horizontal dos '1' vizinhos desta bomba pode ser aberta :D");

                                }

                            }
                        }
                        //se a posição atual está no canto inferior esquerdo*
                        if (linha == 9 && coluna == 0)
                        {
                            if (matriz[linha, coluna + 2] != '-' && matriz[linha - 1, coluna + 2] != '-' && matriz[linha - 2, coluna] != '-' && matriz[linha - 2, coluna + 2] != '-')
                            {//se duas casas do lado direito e em cima existem casas abertas
                                if (matriz[linha, coluna - 1] == '1' & matriz[linha - 1, coluna + 1] == '1' & matriz[linha - 1, coluna] == '1' & matriz[linha - 1, coluna + 1] == '1')
                                {//tem pelo menos '1' um uma casa perto vizinha

                                    Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                    Console.WriteLine("Deseja arriscar? s/n");
                                    escolha = Console.ReadLine();
                                    if (escolha == "s")
                                    {
                                        campoMinado.Abrir(linha + 1, coluna + 1);
                                    }
                                }
                            }
                            //se a posição atual está no canto superior direito*
                            if (linha == 0 && coluna == 8)
                            {
                                if (matriz[linha, coluna - 2] != '-' && matriz[linha + 1, coluna - 2] != '-' && matriz[linha + 2, coluna] != '-' && matriz[linha + 2, coluna - 2] != '-')
                                {//se duas casas do lado direito e em cima existem casas abertas
                                    if (matriz[linha, coluna - 1] == '1' & matriz[linha + 1, coluna - 1] == '1' & matriz[linha + 1, coluna] == '1' & matriz[linha + 1, coluna - 1] == '1')
                                    {//tem pelo menos '1' um uma casa perto vizinha

                                        Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                        Console.WriteLine("Deseja arriscar? s/n");
                                        escolha = Console.ReadLine();
                                        if (escolha == "s")
                                        {
                                            campoMinado.Abrir(linha + 1, coluna + 1);
                                            status = campoMinado.JogoStatus; //Atualiza status do jogo constantemente

                                        }
                                        else
                                            Console.WriteLine("Certo, lembre-se que toda a vizinhança fechada diagonal, vertical e horizontal dos '1' vizinhos desta bomba pode ser aberta :D");

                                    }
                                }

                                //se a posição atual está no canto inferior direito*
                                if (linha == 8 && coluna == 8)
                                {
                                    if (matriz[linha, coluna - 2] != '-' && matriz[linha - 1, coluna - 2] != '-' && matriz[linha - 2, coluna] != '-' && matriz[linha - 2, coluna - 2] != '-')
                                    {//se duas casas do lado direito e em cima existem casas abertas
                                        if (matriz[linha, coluna - 1] == '1' & matriz[linha - 1, coluna - 1] == '1' & matriz[linha - 1, coluna] == '1' & matriz[linha - 1, coluna - 1] == '1')
                                        {//tem pelo menos '1' um uma casa perto vizinha
                                            Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                            Console.WriteLine("Deseja arriscar? s/n");
                                            escolha = Console.ReadLine();
                                            if (escolha == "s")
                                            {
                                                campoMinado.Abrir(linha + 1, coluna + 1);
                                            }
                                        }
                                    }
                                    //se a posição atual está no canto superior fora os cantos esquerdos e direitos*
                                    if (linha == 1 && coluna > 2 && coluna < 8)
                                    {
                                        if (matriz[linha + 2, coluna + 1] != '-' && matriz[linha + 2, coluna] != '-' && matriz[linha + 2, coluna - 1] != '-')
                                        {//se duas casas a baixo estão abertas
                                            if (matriz[linha, coluna - 1] == '1' & matriz[linha + 1, coluna - 1] == '1' & matriz[linha, coluna + 1] == '1' & matriz[linha + 1, coluna] == '1' & matriz[linha + 1, coluna + 1] == '1')
                                            {//tem pelo menos '1' um uma casa perto vizinha

                                                Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                                Console.WriteLine("Deseja arriscar? s/n");
                                                escolha = Console.ReadLine();
                                                if (escolha == "s")
                                                {
                                                    campoMinado.Abrir(linha + 1, coluna + 1);
                                                    status = campoMinado.JogoStatus; //Atualiza status do jogo constantemente

                                                }
                                                else
                                                    Console.WriteLine("Certo, lembre-se que toda a vizinhança fechada diagonal, vertical e horizontal dos '1' vizinhos desta bomba pode ser aberta :D");

                                            }
                                        }
                                        //se a posição atual está no canto inferior fora os cantos esquerdos e direitos*
                                        if (linha == 9 && coluna > 1 && coluna < 9)
                                        {
                                            if (matriz[linha - 2, coluna + 1] != '-' && matriz[linha - 2, coluna] != '-' && matriz[linha - 2, coluna - 1] != '-')
                                            {//se duas casas acima estão abertas
                                                if (matriz[linha, coluna - 1] == '1' & matriz[linha - 1, coluna - 1] == '1' & matriz[linha, coluna + 1] == '1' & matriz[linha - 1, coluna] == '1' & matriz[linha - 1, coluna + 1] == '1')
                                                {//tem pelo menos '1' um uma casa perto vizinha

                                                    Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                                    Console.WriteLine("Deseja arriscar? s/n");
                                                    escolha = Console.ReadLine();
                                                    if (escolha == "s")
                                                    {
                                                        campoMinado.Abrir(linha + 1, coluna + 1);
                                                        status = campoMinado.JogoStatus; //Atualiza status do jogo constantemente

                                                    }
                                                    else
                                                        Console.WriteLine("Certo, lembre-se que toda a vizinhança fechada diagonal, vertical e horizontal dos '1' vizinhos desta bomba pode ser aberta :D");

                                                }
                                            }

                                            //se a posição atual está no canto esquerdo fora os cantos superiores e inferiores*
                                            if (linha > 2 && linha < 8 && coluna == 1)
                                            {
                                                if (matriz[linha - 1, coluna + 2] != '-' && matriz[linha, coluna + 2] != '-' && matriz[linha + 1, coluna + 2] != '-')
                                                {//se duas casas a direita estão abertas
                                                    if (matriz[linha, coluna + 1] == '1' & matriz[linha - 1, coluna + 1] == '1' & matriz[linha + 1, coluna + 1] == '1' & matriz[linha - 1, coluna] == '1' & matriz[linha + 1, coluna] == '1')
                                                    {//tem pelo menos '1' um uma casa perto vizinha
                                                        Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                                        Console.WriteLine("Deseja arriscar? s/n");
                                                        escolha = Console.ReadLine();
                                                        if (escolha == "s")
                                                        {
                                                            campoMinado.Abrir(linha + 1, coluna + 1);
                                                        }
                                                    }
                                                }
                                            }
                                            //se a posição atual está no canto direito fora os cantos superiores e inferiores*
                                            if (linha > 2 && linha < 8 && coluna == 9)
                                            {
                                                if (matriz[linha - 1, coluna - 2] != '-' && matriz[linha, coluna - 2] != '-' && matriz[linha + 1, coluna - 2] != '-')
                                                {//se duas casas a esquerda estão abertas
                                                    if (matriz[linha, coluna - 1] == '1' & matriz[linha - 1, coluna - 1] == '1' & matriz[linha + 1, coluna - 1] == '1' & matriz[linha - 1, coluna] == '1' & matriz[linha + 1, coluna] == '1')
                                                    {//tem pelo menos '1' um uma casa perto vizinha/
                                                        Console.WriteLine("Esta é uma bomba! Melhor tentar outra coordenada...");
                                                        Console.WriteLine("Deseja arriscar? s/n");
                                                        escolha = Console.ReadLine();
                                                        if (escolha == "s")
                                                        {
                                                            campoMinado.Abrir(linha + 1, coluna + 1);
                                                        }
                                                    }
                                                }
                                            }

                                        }


                                    }
                                }
                            }
                        }

                    }

                    status = campoMinado.JogoStatus;
                }
                else
                {
                    Console.WriteLine("A coordenada inserida já foi revelada, por favor, insira novamente.");
                }
            }
            
            Console.WriteLine("Game Over :( Essa era a coordenada de uma bomba.");

        }
    }
}

