using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace GalaxyRim.Controllers
{
    public class Container
    {
        /*static public ParticlePool SparkPool = new ParticlePool(new SparkCreator());
        static public ParticlePool SmokePool = new ParticlePool(new SmokeCreator());
        static public ParticlePool DerbisPool = new ParticlePool(new ScrapCreator());
        static public ParticlePool TrashPool = new ParticlePool(new TrashCreator());*/


        static public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        static public Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        static public Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        static public Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
        static public Dictionary<string, Song> songs = new Dictionary<string, Song>();
        static public Dictionary<string, Model> model = new Dictionary<string, Model>();

        private static System.Threading.ThreadLocal<ContentManager> content = new System.Threading.ThreadLocal<ContentManager>();

        private static GraphicsDevice graphicsDevice;

        public static GraphicsDevice GraphicsDevice { get => graphicsDevice; set => graphicsDevice = value; }

        static IServiceProvider serv;
        public static IServiceProvider Set { set => serv = value; }

        static public ContentManager Content { get => content.Value; set => content.Value = value; }

        //static public T Load<T>(string path) 
        //{
        //    if(typeof(T)==typeof(Texture2D))
        //            return content.Load<T>(path);
                    
        //    return null;
        //}

        static public Texture2D LoadTexture(string path)
        {
            //Content.Load<int>("");
            if (!textures.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                textures.Add(path, Content.Load<Texture2D>(path));
            }
            return textures[path];
        }
        static public SpriteFont LoadFont(string path)
        {
            if (!fonts.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                fonts.Add(path, Content.Load<SpriteFont>(path));
            }
            return fonts[path];
        }
        static public SoundEffect LoadSound(string path)
        {
            if (!sounds.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                sounds.Add(path, Content.Load<SoundEffect>(path));
            }
            return sounds[path];
        }
        static public Effect LoadEffect(string path)
        {
            if (!effects.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                effects.Add(path, Content.Load<Effect>(path));
            }
            return effects[path];
        }
        static public Song LoadSong(string path)
        {
            if (!songs.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                songs.Add(path, Content.Load<Song>(path));
            }
            return songs[path];
        }
        static public Model LoadModel(string path)
        {
            if (!model.ContainsKey(path))
            {
                if (!content.IsValueCreated)
                    content.Value = new ContentManager(serv);
                model.Add(path, Content.Load<Model>(path));
            }
            return model[path];
        }

        private class SparkCreator : IPoolObjectCreator<Models.Particles.Particle>
        {
            public Models.Particles.Particle Create()
            {
                return new Models.Particles.Particle(Container.LoadTexture("Content\\spark"), new Vector2(0),
                    new Vector2(0), 0, 0, new Vector4(1), 1, 0, 0.01f, 0, null);
            }
        }
        private class SmokeCreator : IPoolObjectCreator<Models.Particles.Particle>
        {
            public Models.Particles.Particle Create()
            {
                return new Models.Particles.Particle(Container.LoadTexture("Content\\smoke"), new Vector2(0),
                    new Vector2(0), 0, 0, new Vector4(1), 1, 0, 0.01f, 0, null)
                { Origin = Vector2.Zero };
            }
        }
        private class ScrapCreator : IPoolObjectCreator<Models.Particles.Particle>
        {
            public Models.Particles.Particle Create()
            {
                return new Models.Particles.Particle(Container.LoadTexture("Content\\scrap"), new Vector2(0),
                    new Vector2(0), 0, 0, new Vector4(1), 1, 0, 0.01f, 0, null)
                { Origin = Vector2.Zero };
            }
        }
        private class TrashCreator : IPoolObjectCreator<Models.Particles.Particle>
        {
            public Models.Particles.Particle Create()
            {
                return new Models.Particles.Particle(Container.LoadTexture("Content\\trash"), new Vector2(0),
                    new Vector2(0), 0, 0, new Vector4(1), 1, 0, 0.01f, 0, null)
                { Origin = Vector2.Zero };
            }
        }
    }
}
