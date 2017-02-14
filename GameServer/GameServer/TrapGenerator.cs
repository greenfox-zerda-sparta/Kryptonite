using System;

namespace GameServer {
  class TrapGenerator {
    //a mazeArrayt és a meretet majd bekerjuk persze a mazegeneratortol, addig is: :)
    private const int NUMBER_OF_ROWS = 20;
    private const int NUMBER_OF_COLOUMNS = 20;
    private const int WALL_ID = 1;
    private const int TRAP_ID = 2;
    private const int RAND_MINIMUM = 0;
    private const int RAND_MAXIMUM_FOR_ROWS = NUMBER_OF_ROWS;
    private const int RAND_MAXIMUM_FOR_COLOUMNS = NUMBER_OF_COLOUMNS;
    private const double PERCENIGE_FOR_NUMBER_OF_TRAPS = 0.05;
    private byte[,] mazeArray = new byte[NUMBER_OF_ROWS,NUMBER_OF_COLOUMNS];
    private Random ran = new Random();

    public TrapGenerator()
    {
      GenerateTraps(); 
    }

    //amig nincs meg Danitol a palya :)
    private void FillMazeArray()
    {
      for (int row = 0; row < NUMBER_OF_ROWS; row++)
      {
        for (int column = 0; column < NUMBER_OF_COLOUMNS; column++)
        {
          mazeArray[row, column] = Convert.ToByte(ran.Next(RAND_MINIMUM, 2));
        }
      }
    }

    private void GenerateTraps()
    {
      FillMazeArray();
      int number_of_traps = CountNumberOfTraps();
      int created_traps = 0;
      {
        do
        {
          int i = ran.Next(RAND_MINIMUM, RAND_MAXIMUM_FOR_ROWS);
          int j = ran.Next(RAND_MINIMUM, RAND_MAXIMUM_FOR_COLOUMNS);
          if (mazeArray[i, j] == WALL_ID)
          {
            mazeArray[i, j] = TRAP_ID;
            created_traps++;
          }
        } while (created_traps != number_of_traps);
      }
    }

    private int CountNumberOfTraps()
    {
      return (int)(NUMBER_OF_ROWS * NUMBER_OF_COLOUMNS * PERCENIGE_FOR_NUMBER_OF_TRAPS);
    }
  }
}
