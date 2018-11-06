using Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace _2_Controller_Attempt
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        public Player player, player1, player2, player3, player4;

        List<Player> playersList = new List<Player>();

        public World _world;

        PlayerIndex playerIndex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            new InputManager(this);
            player = new Player(this);

            _world = new World(new Vector2(0, 9.82f));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("SystemFont");

            player1 = new Player(this);
            player1.Name = "Player1";
            player1.Sprite = Content.Load<Texture2D>("Sprites/Mike_300X300");

            player2 = new Player(this);
            player2.Name = "Player2";
            player2.Sprite = Content.Load<Texture2D>("Sprites/Spike_300X300");

            player3 = new Player(this);
            player3.Name = "Player3";

            player4 = new Player(this);
            player4.Name = "Player4";

            playersList.Add(player1);
            playersList.Add(player2);
            playersList.Add(player3);
            playersList.Add(player4);

            foreach (Player player in playersList)
            {
                player.GetPlayerPosition(player);
                player.GetPlayerIndex(player);
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Player player in playersList)
            {
                GamePadState state = GamePad.GetState(player.index);

                if(state.IsConnected)
                {
                    player.Update(gameTime, player, _world);
                }
            }

            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Player player in playersList)
            {
                GamePadState state = GamePad.GetState(player.index);

                if (state.IsConnected)
                {
                        player.Draw(gameTime, font, spriteBatch, player);
                }       
            }            

            base.Draw(gameTime);
        }
    }
}
