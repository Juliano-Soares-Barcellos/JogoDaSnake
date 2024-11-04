using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    internal class Game
    {
        public Keys Direcao { get; set; }

        public Keys Tecla { get; set; }

        private Timer Frame { get; set; }
        private Label LbPontuacao { get; set; }
        private Panel PnTela { get; set; }

        private int pontos = 0;

        private Food food;
        private Snake snake;

        private Bitmap offScreenBitmap;
        private Graphics bitmapGraph;
        private Graphics screenGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            PnTela = panel;
            Frame = timer;
            LbPontuacao = label;
            offScreenBitmap = new Bitmap(428, 428);
            snake = new Snake();
            food = new Food();
            Direcao = Keys.Left;
            Tecla = Direcao;

        }

        public void InicioDeJogo()
        {
            snake.Reset();
            food.CreateFood();
            Direcao = Keys.Left;
            bitmapGraph = Graphics.FromImage(offScreenBitmap);
            screenGraph = PnTela.CreateGraphics();
            Frame.Enabled = true;
        }
        public void Tick()
        {
            // Atualiza a direção da cobra conforme a tecla pressionada
            if (((Direcao == Keys.Left) && (Tecla != Keys.Right)) ||
                ((Direcao == Keys.Right) && (Tecla != Keys.Left)) ||
                ((Direcao == Keys.Up) && (Tecla != Keys.Down)) ||
                ((Direcao == Keys.Down) && (Tecla != Keys.Up)))
            {
                Direcao = Tecla;
            }

            // Move a cobra
            switch (Direcao)
            {
                case Keys.Left:
                    snake.Esquerda();
                    break;
                case Keys.Right:
                    snake.Direita();
                    break;
                case Keys.Up:
                    snake.up();
                    break;
                case Keys.Down:
                    snake.Abaxo();
                    break;
            }

            if (snake.Location[0].X < 0 || snake.Location[0].X >= 28 ||
                snake.Location[0].Y < 0 || snake.Location[0].Y >= 28)
            {
                gameover();
                return;
            }


            bitmapGraph.Clear(Color.White);
            bitmapGraph.DrawImage(Properties.Resources.Maça, (food.Location.X * 15), (food.Location.Y * 15), 15, 15);

            bool GameOver = false;

            // Desenha cada segmento da cobra
            for (int i = 0; i < snake.Tamanho; i++)
            {
                if (i == 0)
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")),
                        (snake.Location[i].X * 15), (snake.Location[i].Y * 15), 15, 15); // Cabeça da cobra
                }
                else
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4F4F4F")),
                        (snake.Location[i].X * 15), (snake.Location[i].Y * 15), 15, 15); // Corpo da cobra
                }

                // Verifica se a cobra colidiu com ela mesma
                if ((snake.Location[i] == snake.Location[0]) && (i > 0))
                {
                    GameOver = true;
                }
            }

            // Desenha o conteúdo do buffer na tela
            screenGraph.DrawImage(offScreenBitmap, 0, 0);

            // Verifica se a cobra comeu a comida
            checkCollision();

            // Verifica se o jogo acabou
            if (GameOver)
            {
                gameover();
            }
        }
    

    public void gameover()
    {
        Frame.Enabled = false;
        bitmapGraph.Dispose();
        screenGraph.Dispose();
        LbPontuacao.Text = "PONTOS: " + pontos;
        MessageBox.Show("FIM DE JOGO");

    }
    public void checkCollision()
    {
        if (snake.Location[0].X == food.Location.X && snake.Location[0].Y == food.Location.Y)
        {
            snake.comer();
            food.CreateFood();
            pontos += 10;
            LbPontuacao.Text = "PONTOS: " + pontos;
        }
    }

}
}