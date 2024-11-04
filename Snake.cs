using System.Drawing;

namespace Snake
{
    internal class Snake
    {
        public int Tamanho { get; private set; }

        public Point[] Location { get; private set; }

        public Snake()
        {
            Location = new Point[28 * 28];
            Reset();
        }

        public void Reset()
        {
            Tamanho = 3;
            for (int i = 0; i < Tamanho; i++)
            {
                Location[i].X = 12;
                Location[i].Y = 12;
            }
        }


        public void Follow()
        {
            for (int i = Tamanho - 1; i > 0; i--)
            {
                Location[i] = Location[i - 1];

            }
        }

        public void up()
        {
            Follow();
            Location[0].Y--;
            if (Location[0].Y < 0)
            {
                Location[0].Y += 28; // Retorna ao limite inferior ao ultrapassar o topo
            }
        }

        public void Abaxo()
        {
            Follow();
            Location[0].Y++;
            if (Location[0].Y >= 28)
            {
                Location[0].Y -= 28; // Retorna ao limite superior ao ultrapassar o fundo
            }
        }


        public void Esquerda()
        {
            Follow();
            Location[0].X--;
            if (Location[0].X < 0)
            {
                Location[0].X += 28;
            }
        }

        public void Direita()
        {
            Follow();
            Location[0].X++;
            if (Location[0].X > 27)
            {
                Location[0].X -= 28;
            }
        }

        public void comer()
        {
            Tamanho++;
        }

    }
}
