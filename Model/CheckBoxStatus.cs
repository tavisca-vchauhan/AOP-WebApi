using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicToeApi.Model
{
    public class CheckBoxStatus
    {
        static int[,] board = new int[3,3];
        public string CheckBox(int [] box)
        {
            int index3 = 0,count=0;
            for (int index =0; index<3;index++)
            {
                for(int index2=0;index2<3;index2++)
                {
                    if(box[index3]!=0)
                    {
                        count++;
                    }
                    board[index,index2]= box[index3];
                    index3++;
                }
            }
            if(count==9)
            {
                return "DRAW !";
            }
            if(RowCheck(box))
            {
                return "Game Over : ";
            }
            else if (ColumnCheck(box))
            {
                return "Game Over : ";
            }
            else if (DiagonalCheck(box))
            {
                return "Game Over : ";
            }

            return "In Progress...";
        }


        bool RowCheck(int[] box)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i,0] == board[i,1] &&
                    board[i,1] == board[i,2] &&
                    board[i,0] !=0)
                    return (true);
            }
            return (false);
        }


        bool ColumnCheck(int[] box)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[0,i] == board[1,i] &&
                    board[1,i] == board[2,i] &&
                    board[0,i] !=0)
                    return (true);
            }
            return (false);
        }


        bool DiagonalCheck(int[] box)
        {
            if (board[0,0] == board[1,1] &&
                board[1,1] == board[2,2] &&
                board[0,0] != 0)
                return (true);

            if (board[0,2] == board[1,1] &&
                board[1,1] == board[2,0] &&
                 board[0,2] !=0)
                return (true);

            return (false);
        }
    }
}
