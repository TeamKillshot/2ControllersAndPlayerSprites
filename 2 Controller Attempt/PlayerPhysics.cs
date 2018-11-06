﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D;
using tainicom.Aether.Physics2D.Dynamics;

namespace _2_Controller_Attempt
{
    public class PlayerPhysics : GameComponent
    {
        #region Variables

        private Matrix _view;
        private Vector2 _cameraPosition;
        private Vector2 _screenCenter;
        private Vector2 _circleOrigin;

        private Body _circleBody;
        private Body _groundBody;

        #endregion

        public PlayerPhysics(Game _game)
            : base(_game)
        {
            //instantiate the camera
            _view = Matrix.Identity;
            _cameraPosition = Vector2.Zero;
            _screenCenter = new Vector2(_game.GraphicsDevice.Viewport.Width
                / 2f, _game.GraphicsDevice.Viewport.Height / 2f);
        }

        public void GetPlayerCircle(Player player, World _world)
        {
            _circleOrigin = new Vector2(player.Sprite.Width / 2f, player.Sprite.Height / 2f);

            Vector2 circlePosition = _screenCenter + new Vector2(0, -1.5f);

            // Create the circle fixture
            _circleBody = _world.CreateCircle((2f), 1f, circlePosition, BodyType.Dynamic);

            // Give it some bounce and friction
            _circleBody.SetRestitution(0.3f);
            _circleBody.SetFriction(0.5f);

            _circleBody.ApplyLinearImpulse(new Vector2(0, -10));

            _view = Matrix.CreateTranslation(new Vector3(_cameraPosition - _screenCenter, 0f)) 
                * Matrix.CreateTranslation(new Vector3(_screenCenter, 0f));
        }
    }
}