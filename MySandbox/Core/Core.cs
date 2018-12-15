using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MySandbox.Core.UI;
using System;

namespace MySandbox.Core
{
    public class Core : Game
    {
        public static Input Input;

        public static SpriteBatch spriteBatch { get; private set; }
        public static GraphicsDeviceManager graphics { get; private set; }
        public static ContentManager content { get; private set; }

        public static UI_Manager UIManager { get; set; } = new UI_Manager();
        public static Player Player { get; protected set; }
        public static Scene CurrentScene { get; set; }

        /// <summary>
        /// Creating core
        /// </summary>
        public Core()
        {
            Input = new Input();
            IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            content = Content;
        }
        /// <summary>
        /// Initialization
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Initialization..");
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }
        /// <summary>
        /// On resize
        /// </summary>
        protected virtual void OnResize(object sender, EventArgs e)
        {
            Camera.main.SetPosition(Player.GetCentredPosition());
        }
        /// <summary>
        /// Load content
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            Console.WriteLine("Loading Content..");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentDefault.LoadContent();
        }
        /// <summary>
        /// Update scene,player,ui
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if(CurrentScene != null)
                CurrentScene.Update(gameTime);
            if(Player != null)
                Player.Update(gameTime);
            UIManager.Update(gameTime);

            base.Update(gameTime);
            OnUpdate(gameTime);

            Input.LateUpdate();
        }
        /// <summary>
        /// OnUpdate
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected virtual void OnUpdate(GameTime gameTime)
        {

        }
        /// <summary>
        /// Draw scene,player,ui
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin( samplerState: SamplerState.PointWrap,transformMatrix: Camera.main.Transform);
            if (CurrentScene != null)
            {
                CurrentScene.Draw();
                Player.Draw();
                UIManager.Draw();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
