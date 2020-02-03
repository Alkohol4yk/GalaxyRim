using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyRim.Models.Particles
{
    /// <summary>
    /// Інтерфейс об'єкту частинки.
    /// </summary>
    public interface IParticle:IPhysicalObject,IDrawableObject
    {
        /// <summary>
        /// Швидкість зміни прозорості частинки.
        /// </summary>
        Vector4 AlphaVel { get; set; }          
        /// <summary>
        /// Час життя частинки (в тіках гри).
        /// </summary>
        uint TTL { get; set; }
        /// <summary>
        /// Функція скидання частинки / повернення до ParticlePool.
        /// </summary>
        void ResetState();
    }
}
