using System;
using System.Collections.Generic;
string[,] menu ={{"@","Начать игру"," "},
                {" ","Выбор уровня"," "},
                {" ","Выход"," "}};

string[,] matrix = {{" "," "," "," "," "},
                    {" ","|"," "," "," "},
                    {" "," ","@"," "," "},
                    {" "," "," "," "," "},
                    {" "," "," ","$"," "},
                    {" "," "," "," "," "}};

string[,] LvlMenu ={{" ","Первый"," "},
                    {" ","Второй"," "},
                    {" ","Третий"," "},
                    {" ","Четвертый"," "}};                    

Dictionary < int, string[,] > Lvls = new Dictionary<int, string[,]> {{1,
new string[,]{{" "," "," "," "," "},
              {" ","|"," "," "," "},
              {" "," ","@"," "," "},
              {" "," "," "," "," "},
              {" "," "," ","$"," "},
              {" "," "," "," "," "}}},{2,
new string[,]{{" "," "," "," "," "},
              {" ","|"," "," "," "},
              {" "," ","@"," "," "},
              {" "," "," ","|"," "},
              {" "," "," ","$"," "},
              {" "," "," "," "," "}}},{3,
new string[,]{{" "," "," ","|"," "},
              {" ","|"," ","|"," "},
              {" "," ","@"," "," "},
              {" "," "," "," "," "},
              {" "," ","|","$"," "},
              {" "," "," "," "," "}}},{4,
new string[,]{{" "," "," "," "," "},
              {" ","|"," ","|"," "},
              {" "," ","@"," "," "},
              {" "," "," ","|"," "},
              {" ","|"," ","$"," "},
              {" "," "," "," "," "}}}};

int coins = 0;
int MenuX = 0;
int MenuY = 0;

int SelectLvlGame()
{
    
    int indexMenu2 = 0;
    MatrixWrite(LvlMenu);
    ConsoleKeyInfo User_keyTab2 = Console.ReadKey();
    while(User_keyTab2.Key != ConsoleKey.Enter)
    {
        Console.Clear();
        LvlMenu[MenuY,MenuX] = " ";
        if(User_keyTab2.Key == ConsoleKey.W && indexMenu2 > 0)
        {
            indexMenu2--;
            MenuY--;
        }
        if(User_keyTab2.Key == ConsoleKey.S && indexMenu2 < 2)
        {
            indexMenu2++;
            MenuY++;
        }
        LvlMenu[MenuY,MenuX] = "@"; 
        MatrixWrite(LvlMenu);
        User_keyTab2 = Console.ReadKey();
    }
    return indexMenu2;
}



int SelectMenuPlayer()
{
    int indexMenu = 0;
    MatrixWrite(menu);
    ConsoleKeyInfo User_keyTab = Console.ReadKey();
    while(User_keyTab.Key != ConsoleKey.Enter)
    {
        Console.Clear();
        menu[MenuY,MenuX] = " ";
        if(User_keyTab.Key == ConsoleKey.W && indexMenu > 0)
        {
            indexMenu--;
            MenuY--;
        }
        if(User_keyTab.Key == ConsoleKey.S && indexMenu < 2)
        {
            indexMenu++;
            MenuY++;
        }
        menu[MenuY,MenuX] = "@"; 
        MatrixWrite(menu);
        User_keyTab =Console.ReadKey();
    }
    return indexMenu;
}

void MatrixWrite (string[,] array)
{
    for(int i = 0; i < array.GetLength(0); i++)
    {
        for(int j = 0; j < array.GetLength(1); j++)
        {
            Console.Write(array[i,j] + " ");
        }
        Console.WriteLine(" ");
    }
    Console.WriteLine("Кол-во очков " + coins);
}
MatrixWrite(matrix);

string[,] ItemFoodMatrix( int x, int y, string[,] array)
{
    while(matrix[y,x] == "$")
    {
        matrix[y,x] =" ";
        int matX = new Random().Next(0, array.GetLength(1));
        int matY = new Random().Next(0, array.GetLength(0));
        while(matrix[matY,matX] == "|")
        {
            matX = new Random().Next(0, array.GetLength(1));
            matY = new Random().Next(0, array.GetLength(0));
        }
        array[matY, matX] = "$";
        coins++;
    }
    return array;
}

bool Barrier(int x, int y)
{
    if(matrix[y,x] =="|") return false;
    return true;
}

int x = 2;
int y = 2;



while(true)
{
    switch(SelectMenuPlayer())
    {
        case 0:
            Console.Clear();
            Game();
            break;
        case 1:
            Console.Clear();
            matrix = Lvls[SelectLvlGame()];
            Game();
            break;
        case 2:
            Console.Clear();
            break;
        default:
            break;
    }
}



void Game()
{
    while(true)
    {
        Console.Clear();
        MatrixWrite(matrix);
        matrix[y,x] = " ";
        ConsoleKeyInfo User_keyTab = Console.ReadKey();
        if(User_keyTab.Key == ConsoleKey.W) if(Barrier(x,y-1)) y--;
        if(User_keyTab.Key == ConsoleKey.S) if(Barrier(x,y+1)) y++;
        if(User_keyTab.Key == ConsoleKey.A) if(Barrier(x-1,y)) x--;
        if(User_keyTab.Key == ConsoleKey.D) if(Barrier(x+1,y)) x++;
        matrix = ItemFoodMatrix(x,y,matrix);
        matrix[y,x] = "@";

        if(y >= matrix.GetLength(0)) y = matrix.GetLength(0)-1;
        if(y <= 0) y = 0;
        if(x >= matrix.GetLength(1)) x = matrix.GetLength(1)-1;
        if(x <=0) x = 0;
    }     
}