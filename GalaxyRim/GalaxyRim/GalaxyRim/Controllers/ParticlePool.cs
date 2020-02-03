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
    public class ParticlePool
    {
        private readonly System.Collections.Concurrent.ConcurrentBag<Models.Particles.Particle> pool =
            new System.Collections.Concurrent.ConcurrentBag<Models.Particles.Particle>();

        private readonly IPoolObjectCreator<Models.Particles.Particle> creator;

        public int Count => this.pool.Count;

        public ParticlePool(IPoolObjectCreator<Models.Particles.Particle> creator)
        {
            if (creator == null) throw new ArgumentNullException("Creator can`t be null");
            this.creator = creator;

        }

        public Models.Particles.Particle GetParticle()
        {
            Models.Particles.Particle m;
            if (this.pool.TryTake(out m))
                return m;
            return this.creator.Create();
        }

        public void ReturnParticle(ref Models.Particles.Particle particle)
        {
            particle.ResetState();
            this.pool.Add(particle);
            particle = null;
        }

    }
    public interface IPoolObjectCreator<T>
    {
        T Create();
    }
    public class DefaultObjectCreator<T> : IPoolObjectCreator<T> where T : class, new()
    {
        T IPoolObjectCreator<T>.Create()
        {
            return new T();
        }
    }
}
