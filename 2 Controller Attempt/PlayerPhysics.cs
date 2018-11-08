using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        //private Vector2 _screenCenter;
        private Vector2 _circleOrigin;

        //private Body _circleBody;
        private Body _groundBody;

        #endregion

        public PlayerPhysics(Game _game)
            : base(_game)
        {
            //instantiate the camera
            _view = Matrix.Identity;
            _cameraPosition = Vector2.Zero;
            //_screenCenter = new Vector2(_game.GraphicsDevice.Viewport.Width
            //    / 2f, _game.GraphicsDevice.Viewport.Height / 2f);

            _game.Components.Add(this);
        }

        public void GetPlayerCircle(Player player, World _world, Body _circleBody)
        {
            GamePadState padState = GamePad.GetState(player.index);

            if (player.Sprite != null)
            {
                _circleOrigin = new Vector2(player.Sprite.Width / 2f, player.Sprite.Height / 2f);

                Vector2 circlePosition = new Vector2(player.currentPosition.X, player.currentPosition.Y);

                // Create the circle fixture
                _circleBody = _world.CreateCircle((2f), 1f, circlePosition, BodyType.Dynamic);

                // Give it some bounce and friction
                _circleBody.SetRestitution(0.3f);
                _circleBody.SetFriction(0.5f);

                _circleBody.ApplyLinearImpulse(new Vector2(0, -10));

                _circleBody.ApplyForce(padState.ThumbSticks.Left);
                _cameraPosition.X -= padState.ThumbSticks.Right.X;
                _cameraPosition.Y += padState.ThumbSticks.Right.Y;

                _view = Matrix.CreateTranslation(new Vector3(_cameraPosition - new Vector2(player.currentPosition.X, player.currentPosition.Y), 0f)) 
                    * Matrix.CreateTranslation(new Vector3(new Vector2(player.currentPosition.X, player.currentPosition.Y), 0f));

                //_view = Matrix.CreateTranslation(new Vector3(_cameraPosition - _screenCenter, 0f)) 
                //*Matrix.CreateTranslation(new Vector3(_screenCenter, 0f));
            }
        }
    }
}
