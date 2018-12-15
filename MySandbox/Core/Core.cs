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

        public UI_Manager UIManager = new UI_Manager();

        public static Player Player { get; protected set; }
        public static Scene currentScene;

        public Core()
        {
            Input = new Input();
            IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            content = Content;
        }

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

        protected virtual void OnResize(object sender, EventArgs e)
        {
            Camera.main.SetPosition(Player.GetCentredPosition());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Console.WriteLine("Loading Content..");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentDefault.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if(currentScene != null)
                currentScene.Update(gameTime);
            if(Player != null)
                Player.Update(gameTime);
            UIManager.Update(gameTime);

            base.Update(gameTime);
            OnUpdate(gameTime);

            Input.LateUpdate();
        }

        protected virtual void OnUpdate(GameTime gameTime)
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin( samplerState: SamplerState.PointWrap,transformMatrix: Camera.main.Transform);
            if (currentScene != null)
            {
                currentScene.Draw();
                Player.Draw();
                UIManager.Draw();

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
