using System;

namespace JogoLaboratorioPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variaveis
            int energia = 100;
            int pontos = 0;
            int fase = 1;
            bool jogando = true;
            string resposta;

            //Introdução
            Console.WriteLine("FUGA DO LABORATÓRIO DE INFORMÁTIl" +
                "CA");
            Console.WriteLine("Você pegou no sono estudando e o laboratório foi trancado.");
            Console.WriteLine("Dica: Digite 'sair' a qualquer momento para desistir.\n");

            //Iniciar o jogo
            while (jogando)
            {
                // Estabelecendo a condicional inicial que falará se ele perdeu ou não 
                if (energia <= 0)
                {
                    //colocando uma cor no console para enfatizar a perda
                    Console.ForegroundColor = ConsoleColor.Red;
                    //texto de perda
                    Console.WriteLine("[GAME OVER] Sua energia acabou. Você terá que esperar o segurança abrir de manhã.");
                    Console.ResetColor();
                    break;
                }

                // Verifica se o jogador venceu (passou da fase 3)
                if (fase > 3)
                {
                    //colocando cor verde para enfatizar a vitoria
                    Console.ForegroundColor = ConsoleColor.Green;
                    //texto de vitoria
                    Console.WriteLine("[VITÓRIA] Parabéns! Você destrancou a porta e conseguiu sair do laboratório!");
                    Console.WriteLine($"Pontuação Final: {pontos} pontos.");
                    Console.ResetColor();
                    break;
                }
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"Status: Energia: {energia} | Pontos: {pontos} | Fase: {fase}");
                Console.WriteLine("------------------------------------------");

                // condicional da fase 1
                if (fase == 1)
                {
                    Console.WriteLine("FASE 1: para avançar, responda:");
                    Console.WriteLine("Qual o comando em C# usado para exibir mensagens no console?");
                    Console.Write("Resposta: ");
                    //fazendo o codigo ler e formatar (ToLower) ele para evitar de erros desnecessários
                    resposta = Console.ReadLine().ToLower();
                    //estabelecendo a função de sair
                    if (resposta == "sair") { jogando = false; }
                    //resposta certa
                    else if (resposta == "console.writeline" || resposta == "console.writeline()")
                    {
                        Console.WriteLine("Correto!");
                        pontos += 10;
                        fase = 2;
                    }
                    else
                    {
                        Console.WriteLine("Erro!");
                        energia -= 30;
                    }
                }
                else if (fase == 2)
                {
                    Console.WriteLine("FASE 2: resolva a questão matematica:");
                    Console.WriteLine("Em um calculo de uma equação de segundo grau, não existirá raizes reais se?");
                    Console.Write("Delta for: ");
                    resposta = Console.ReadLine().ToLower();

                    if (resposta == "sair") { jogando = false; }
                    else if (resposta == "negativo")
                    {
                        Console.WriteLine("Correto!");
                        pontos += 15;
                        fase = 3;
                    }
                    else
                    {
                        Console.WriteLine("Erro!");
                        energia -= 40;
                    }
                }
                else if (fase == 3)
                {
                    Console.WriteLine("FASE 3: Último desafio!");
                    Console.WriteLine("Qual estrutura de repetição executa um bloco 'enquanto' uma condição for verdadeira?");
                    Console.Write("Resposta: ");
                    resposta = Console.ReadLine().ToLower();

                    if (resposta == "sair") { jogando = false; }
                    else if (resposta == "while")
                    {
                        Console.WriteLine("Correto! A porta se abriu.");
                        pontos += 20;
                        fase = 4; // Isso fará o loop verificar a vitória na próxima volta
                    }
                    else
                    {
                        Console.WriteLine("Erro! A porta travou mais forte.");
                        energia -= 50;
                    }
                }
            }

            Console.WriteLine("Obrigado por jogar! Pressione qualquer tecla para encerrar.");
            Console.ReadKey();
        }
    }
}