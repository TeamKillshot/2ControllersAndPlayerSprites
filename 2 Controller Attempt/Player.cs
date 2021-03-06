﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace _2_Controller_Attempt
{
    public sealed class Player : GameComponent
    {
        #region Properties and Variables

        public PlayerIndex index;
        public Rectangle previousPosition;
        public Rectangle currentPosition;

        public Texture2D Sprite { get; set; }
        public string Name { get; set; }
        public bool IsConnected = false;
        public PlayerPhysics Body;
        private Body _circleBody;

        public List<Player> playerList = new List<Player>();

        Player player;

        private int speed = 15;
        #endregion

        public Player(Game _game)
            : base (_game)
        {
            GamePad.GetState(index);
            _game.Components.Add(this);

            Body = new PlayerPhysics(_game);
        }

        public void GetPlayerPosition(Player player)
        {            
            player.currentPosition = new Rectangle(125, 125, 300, 300);
            player.previousPosition = player.currentPosition;

        }

        public void GetPlayerIndex(Player player)
        {
            #region Check GameStates
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            GamePadState state2 = GamePad.GetState(PlayerIndex.Two);
            GamePadState state3 = GamePad.GetState(PlayerIndex.Three);
            GamePadState state4 = GamePad.GetState(PlayerIndex.Four);
            #endregion

            if (player.Name == "Player1" && state.IsConnected)
            {
                player.index = PlayerIndex.One;
                player.IsConnected = true;
            }
            else if(player.Name == "Player2" && state2.IsConnected)
            {
                player.index = PlayerIndex.Two;
                player.IsConnected = true;
            }
            else if(player.Name == "Players3" && !state.IsConnected)
            {
                player.index = PlayerIndex.Three;
                player.IsConnected = true;
            }
            else if (player.Name == "Player4" && !state.IsConnected)
            {
                player.index = PlayerIndex.Four;
                player.IsConnected = true;
            }
        }

        public void Update(GameTime gameTime, Player player, World _world)
        {
            #region Player1 Controller DPad
            if (player != null)
            {
                if (InputManager.IsButtonPressed(Buttons.DPadRight, player.index))
                {
                    player.currentPosition.X += speed;
                }
                if (InputManager.IsButtonPressed(Buttons.DPadLeft, player.index))
                {
                    player.currentPosition.X -= speed;
                }
                if (InputManager.IsButtonPressed(Buttons.DPadDown, player.index))
                {
                    player.currentPosition.Y += speed;
                }
                if (InputManager.IsButtonPressed(Buttons.DPadUp, player.index))
                {
                    player.currentPosition.Y -= speed;
                }
                if (InputManager.IsButtonPressed(Buttons.A, player.index))
                {
                    player.Body.GetPlayerCircle(player, _world, _circleBody);
                }

                #region Player1 Controller Joysticks
                if (player != null)
                {
                    Vector2 Joystick = InputManager.ThumbStickState();
                    Vector2 curPos = new Vector2(player.currentPosition.X, player.currentPosition.Y);


                }
                #endregion
            }
        }

            public void Draw(GameTime gameTime, SpriteFont font, SpriteBatch spritebatch, Player player)
        {
            spritebatch.Begin();
            //spritebatch.DrawString(font, "Player" + player.index.ToString(), currentPosition, Color.Red);
            if (player.Sprite != null)
            {
                 spritebatch.Draw(player.Sprite, player.currentPosition, Color.White);
            }
            spritebatch.End();
        }
    }
}
