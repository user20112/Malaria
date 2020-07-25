using Malaria2.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Malaria2
{
    internal class Malaria2 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch g;
        private SpriteSheet SpriteSheet;
        private EntitySpriteSheet EntitySheet;
        private int ScreenHeight;
        private int ScreenWidth;
        private World World;

        public Malaria2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        private void OnResize(object sender, EventArgs e)
        {
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
            g.Begin(SpriteSortMode.Immediate);
            World.Draw(player.Posx, player.Posy, ScreenHeight, ScreenWidth, g, SpriteSheet, EntitySheet);
            g.End();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            World.Update(player.Posx, player.Posy, gameTime, ScreenHeight, ScreenWidth);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        private Player player;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            g = new SpriteBatch(GraphicsDevice);
            SpriteSheet = new SpriteSheet(Content.Load<Texture2D>("SpriteSheet"), 16, 16);
            EntitySheet = new EntitySpriteSheet(Content.Load<Texture2D>("EntitySheet"), 2, 16);
            World = new World(1000, 1000);
            player = new Player(0);
            player.SetLocation(World, 300, 600);
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }
    }
}