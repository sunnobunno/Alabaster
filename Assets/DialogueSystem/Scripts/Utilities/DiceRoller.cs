using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alabaster.DialogueSystem.Utilities
{
    public class DiceRoller
    {
        public static Random RandomRoller = new Random();
        
        public static int[] Roll2D6()
        {
            var diceResult = new int[2];
            var dice1Roll = RandomRoller.Next(1, 6);
            var dice2Roll = RandomRoller.Next(1, 6);
            diceResult[0] = dice1Roll;
            diceResult[1] = dice2Roll;

            return diceResult;
        }


    }
}
