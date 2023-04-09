using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZInput;
using System.IO;
using System.Threading;

namespace game_project
{
    class Program
    {
        static int score = 0;
        static void Main(string[] args)
        {
            int bobX = 4;
            int bobY = 4;
            char[] bob = new char[] { 'B', 'O', 'B' };
            
            char[,] maze = new char[10, 11] {
             { '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%'}
             };
            
            

                // Ghost 1 (Horizontal) Information
                char previous1 = ' ';
                int ghost1X = 8;
                int ghost1Y = 8;
                string ghost1direction = "left";
                int count1 = 0;


                

                
                printMaze(maze);

                Console.SetCursorPosition(bobY, bobX);
                Console.Write("B");

                bool gameRunning = true;
                while (true)
                {
                    Thread.Sleep(90);
                    printScore();
                    if (Keyboard.IsKeyPressed(Key.UpArrow))
                    {
                        movebobUp(maze, ref bobX, ref bobY);
                    }
                    if (Keyboard.IsKeyPressed(Key.DownArrow))
                    {
                        movebobDown(maze, ref bobX, ref bobY);
                    }
                    if (Keyboard.IsKeyPressed(Key.LeftArrow))
                    {
                        movebobLeft(maze, ref bobX, ref bobY);
                    }
                    if (Keyboard.IsKeyPressed(Key.RightArrow))
                    {
                        movebobRight(maze, ref bobX, ref bobY);
                    }
                    count1++;

                int op = 0;
                    if (op == 0 )            // Slowest Movement
                    {
                        gameRunning = moveGhostInLine(ref ghost1direction, maze, ref ghost1X, ref ghost1Y, ref previous1);
                        if (gameRunning == false)
                        {
                            break;
                        }
                        count1 = 0;
                    }
                Console.ReadKey();
                }
            }

            static void printScore()
            {
                Console.SetCursorPosition(79, 12);
                Console.WriteLine("Score: " + score);
            }





            static bool moveGhostInLine(ref string direction, char[,] maze, ref int X, ref int Y, ref char previous)
            {
                if (maze[X, Y - 1] == 'B' || maze[X, Y + 1] == 'B' || maze[X + 1, Y] == 'B' || maze[X - 1, Y] == 'B')
                    if (direction == "left" && (maze[X, Y - 1] == ' ' || maze[X, Y - 1] == '.'))
                    {
                        maze[X, Y] = previous;
                        Console.SetCursorPosition(Y, X);
                        Console.Write(previous);

                        Y = Y - 1;

                        previous = maze[X, Y];
                        Console.SetCursorPosition(Y, X);
                        Console.Write("S");
                        if (maze[X, Y - 1] == '|')
                        {
                            direction = "right";
                        }
                    }
                    else if (direction == "right" && (maze[X, Y + 1] == ' ' || maze[X, Y + 1] == '.'))
                    {
                        maze[X, Y] = previous;
                        Console.SetCursorPosition(Y, X);
                        Console.Write(previous);

                        Y = Y + 1;

                        previous = maze[X, Y];
                        Console.SetCursorPosition(Y, X);
                        Console.Write("S");
                        if (maze[X, Y + 1] == '|')
                        {
                            direction = "left";
                        }
                    }
                    else if (direction == "up" && (maze[X - 1, Y] == ' ' || maze[X - 1, Y] == '.'))
                    {
                        maze[X, Y] = previous;
                        Console.SetCursorPosition(Y, X);
                        Console.Write(previous);

                        X = X - 1;

                        previous = maze[X, Y];
                        Console.SetCursorPosition(Y, X);
                        Console.Write("S");
                        if (maze[X - 1, Y] == '%' || maze[X - 1, Y] == '#')
                        {
                            direction = "down";
                        }
                    }
                    else if (direction == "down" && (maze[X + 1, Y] == ' ' || maze[X + 1, Y] == '.'))
                    {
                        maze[X, Y] = previous;
                        Console.SetCursorPosition(Y, X);
                        Console.Write(previous);

                        X = X + 1;

                        previous = maze[X, Y];
                        Console.SetCursorPosition(Y, X);
                        Console.Write("S");
                        if (maze[X + 1, Y] == '%' || maze[X + 1, Y] == '#')
                        {
                            direction = "up";
                        }
                    }
                return true;
            }





            static void calculateScore()
            {
                score = score + 1;
            }

            static void movebobUp(char[,] maze, ref int bobX, ref int bobY)
            {
                if (maze[bobX - 1, bobY] == ' ' || maze[bobX - 1, bobY] == '.')
                {
                    maze[bobX, bobY] = ' ';
                    Console.SetCursorPosition(bobY, bobX);
                    Console.Write(" ");
                    bobX = bobX - 1;
                    if (maze[bobX, bobY] == '.')
                    {
                        calculateScore();
                    }
                    Console.SetCursorPosition(bobY, bobX);
                    maze[bobX, bobY] = 'B';
                    Console.Write("B");

                }
            }
            static void movebobDown(char[,] maze, ref int bobX, ref int bobY)
            {
                if (maze[bobX + 1, bobY] == ' ' || maze[bobX + 1, bobY] == '.')
                {
                    maze[bobX, bobY] = ' ';
                    Console.SetCursorPosition(bobY, bobX);
                    Console.Write(" ");
                    bobX = bobX + 1;
                    Console.SetCursorPosition(bobY, bobX);
                    if (maze[bobX, bobY] == '.')
                    {
                        calculateScore();
                    }
                    maze[bobX, bobY] = 'B';
                    Console.Write("B");

                }
            }

            static void movebobLeft(char[,] maze, ref int bobX, ref int bobY)
            {
                if (maze[bobX, bobY - 1] == ' ' || maze[bobX, bobY - 1] == '.')
                {
                    maze[bobX, bobY] = ' ';
                    Console.SetCursorPosition(bobY, bobX);
                    Console.Write(" ");
                    bobY = bobY - 1;
                    if (maze[bobX, bobY] == '.')
                    {
                        calculateScore();
                    }
                    Console.SetCursorPosition(bobY, bobX);
                    maze[bobX, bobY] = 'B';
                    Console.Write("B");

                }
            }

            static void movebobRight(char[,] maze, ref int bobX, ref int bobY)
            {
                if (maze[bobX, bobY + 1] == ' ' || maze[bobX, bobY + 1] == '.')
                {
                    maze[bobX, bobY] = ' ';
                    Console.SetCursorPosition(bobY, bobX);
                    Console.Write(" ");
                    bobY = bobY + 1;
                    if (maze[bobX, bobY] == '.')
                    {
                        calculateScore();
                    }
                    Console.SetCursorPosition(bobY, bobX);
                    maze[bobX, bobY] = 'B';
                    Console.Write("B");

                }
            }

            static void printMaze(char[,] maze)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    for (int y = 0; y < maze.GetLength(1); y++)
                    {
                        Console.Write(maze[x, y]);
                    }
                    Console.WriteLine();
                }
            }

           
        }
    }

