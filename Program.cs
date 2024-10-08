﻿using Raylib_cs;

namespace Pong;

class Pong
{
    // constants //
    const int windowLength = 800;   // defines window's horizontal size
    const int windowHeigth = 480;   // defines window's vertical size
    const int racketWidth = 25;     // defines racket's horizontal size
    const int racketHeight = 100;   // defines racket's vertical size 

    // variables 1 //
    static double leftRacketY = windowHeigth * 0.40; // is used to move left racket
    static double rightRacketY = windowHeigth * 0.40; // is used to move right racket
    static double ballX = windowLength / 2; // is used to move ball in horizontal way
    static double ballY = windowHeigth / 2; // is used to move ball in vertical way

    // variables 2 //
    static bool BallIsGoingUp = true;   // used to change the vertical movement
    static bool BallIsGoingRight = true;    // used to move the horizontal movement

    // variables 3 //
    static int leftPlayerScore = 0;
    static int rightPlayerScore = 0;
    static double leftRacketCollisionY = racketHeight % Math.Round(leftRacketY);
    static double rightRacketCollisionY = racketHeight % Math.Round(rightRacketY);


    // main method //
    public static void Main(string[] args)
    {
        Raylib.InitWindow(windowLength, windowHeigth, "Pong Clone using C# and Raylib");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            IsGameOver();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    // methods 1 - Drawing shapes //
    static void DrawLeftRacket()
    {
        MoveLeftRacketY();
        Raylib.DrawRectangle((int)(windowLength * 0.02), (int) leftRacketY, racketWidth, racketHeight, Color.DarkBlue);
    }

    static void DrawRightRacket()
    {
        MoveRightRacketY();
        Raylib.DrawRectangle((int)(windowLength * 0.95), (int) rightRacketY, racketWidth, racketHeight, Color.Red);
    }

    static void DrawBall()
    {
        MoveBallX(); 
        MoveBally();
        Raylib.DrawCircle((int) ballX, (int) ballY, 10, Color.Black);
    }

    static void DrawScore()
    {
        Raylib.DrawText($"{leftPlayerScore}", (windowLength / 2) - 20, 10, 20, Color.Black);
        Raylib.DrawText("|", windowLength / 2, 10, 20, Color.Black);
        Raylib.DrawText($"{rightPlayerScore}", (windowLength / 2) + 10, 10, 20, Color.Black);
    }

    // methods 2  - Moving Shapes//
    static void MoveLeftRacketY()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            RacketsUpperLimit();
            leftRacketY -= 0.1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            RacketsBottomLimit();
            leftRacketY += 0.1;
        }
    }

    static void MoveRightRacketY() 
    {
        if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            RacketsUpperLimit();
            rightRacketY -= 0.1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            RacketsBottomLimit();
            rightRacketY += 0.1;
        }
    }

    static void MoveBallX() 
    {
        if (BallIsGoingRight)
        {
            BallsXLimit();
            ballX += 0.02;
        }
        else
        {
            BallsXLimit();
            ballX -= 0.02;
        }
    }

    static void MoveBally()
    {
        BallsYLimit();
        if (BallIsGoingUp)
        {
            ballY -= 0.02;
        }
        else
        {
            ballY += 0.02;
        }
    }

    static void ResetBallPosition()
    {
        ballX = windowLength / 2;
        ballY = windowHeigth / 2;
    }

    // methods 3 - Shaping Limits //
    static void RacketsUpperLimit()
    {
        if (leftRacketY < 0)
        {
            leftRacketY++;
        }
        if (rightRacketY < 0)
        {
            rightRacketY++;
        }
    }

    static void RacketsBottomLimit()
    {
        if (leftRacketY > windowHeigth - racketHeight)
        {
            leftRacketY--;
        }
        if (rightRacketY > windowHeigth - racketHeight)
        {
            rightRacketY--;
        }
    }

    static void BallsYLimit()
    {
        if (ballY < 0 || ballY > 480)
        {
            BallIsGoingUp = !BallIsGoingUp;
        }
    }

    static void BallsXLimit()
    {
        if (ballX <= racketWidth)
        {
            rightPlayerScore++;
            ResetBallPosition();
        }
        else if (ballX >= windowLength - racketWidth)
        {
            leftPlayerScore++;
            ResetBallPosition();
        }
    }

    static void BallHittingRackets()
    {
        if (ballX <= windowLength * 0.05)
        {
            if (!(ballY <= leftRacketY || ballY >= leftRacketY + leftRacketCollisionY))
            {
                BallIsGoingRight = true;
            }
        }
        else if (ballX >= windowLength * 0.95)
        {
            if (!(ballY <= rightRacketY || ballY >= rightRacketY + rightRacketCollisionY))
            {
                BallIsGoingRight = false;
            }
        }
    }

    // Methods 4 - Shaping the Champion //
    static void IsGameOver() {
        if (leftPlayerScore < 10 && rightPlayerScore < 10)
        {
            DrawLeftRacket();
            DrawRightRacket();
            DrawBall();
            DrawScore();
            BallHittingRackets();
        }
        else
        {
            if (leftPlayerScore >= 10)
            {
                Raylib.DrawText("Player 2 Wins!", windowLength / 2 - 75, windowHeigth / 2, 20, Color.DarkBlue);
            }
            else if (rightPlayerScore >= 10)
            {
                Raylib.DrawText("Player 1 Wins!", windowLength / 2 - 75, windowHeigth / 2, 20, Color.Red);
            }
        }
    }
}